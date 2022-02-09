using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup_Backend.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BrandController : Controller
    {
        private readonly Context _context;

        public BrandController(Context context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<Brand> brands = _context.Brands.Include(b => b.CategoryBrands).ThenInclude(c => c.Category).ToList();
            return View(brands);
        }


        //Get Create
        public IActionResult Create()
        {
            ViewBag.IsMainCategory = _context.Categories.Where(c => c.IsMain == true).ToList();
            ViewBag.SubCategory = _context.Categories.Where(c => c.IsMain == false).ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand, int[] subcategories)
        {
            bool isExist = _context.Brands.Any(b => b.Name.ToLower().Trim() == brand.Name.ToLower().Trim());
        
            if (isExist)
            {
                ModelState.AddModelError("Name", "The brand with this name already exists");
                return RedirectToAction(nameof(Index));

            }

            if (subcategories != null)
            {

                Brand newBrand = new Brand();
                newBrand.Name = brand.Name;
                await _context.Brands.AddAsync(newBrand);
                await _context.SaveChangesAsync();

                foreach (var subcategory in subcategories)
                {
                    CategoryBrand categoryBrands = new CategoryBrand();
                    categoryBrands.BrandId = newBrand.Id;
                    categoryBrands.CategoryId = subcategory;
                    await _context.AddAsync(categoryBrands);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        //Get Update
        public async Task<IActionResult> Update(int? id)
        {
            Brand brand = await _context.Brands.Include(b => b.CategoryBrands).ThenInclude(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
            List<CategoryBrand> SubCategory = await _context.CategoryBrands.Include(c => c.Category).Where(x => x.BrandId == brand.Id).ToListAsync();

            List<Category> allCategory = await _context.Categories.Include(c => c.CategoryBrands).ThenInclude(c => c.Brand).Where(c => c.IsMain == false).ToListAsync();
            foreach (var item in SubCategory)
            {
                allCategory.Remove(item.Category);

            }
            ViewBag.checkCategory = SubCategory;
            ViewBag.noneCheck = allCategory;
            return View(brand);
        }

        //Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Brand brand, List<int> subcategory)
        {
            bool isExist = _context.Brands.Any(c => c.Name.ToLower() == brand.Name.ToLower().Trim());
            Brand newBrand = await _context.Brands.FindAsync(id);

            if (isExist && !(newBrand.Name.ToLower() == brand.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", $"{newBrand} brand already exists");
                return RedirectToAction("Edit");
            }

            if (subcategory.Count() == 0)
            {
                ModelState.AddModelError("Name", "Must choose at least one category");
                return RedirectToAction("Edit");
            }

            List<int> checkCategory = _context.CategoryBrands.Where(c => c.BrandId == newBrand.Id).Select(i => i.CategoryId).ToList();

            List<int> addCategory = subcategory.Except(checkCategory).ToList();
            List<int> removedCategory = checkCategory.Except(subcategory).ToList();

            int addCategoryLength = addCategory.Count();
            int removedCategoryLength = removedCategory.Count();
            int FullLength = addCategoryLength + removedCategoryLength;

            newBrand.Name = brand.Name;

            for (int i = 1; i <= FullLength; i++)
            {
                if (addCategoryLength >= i)
                {
                    CategoryBrand categoryBrand = new CategoryBrand();
                    categoryBrand.BrandId = newBrand.Id;
                    categoryBrand.CategoryId = addCategory[i - 1];
                    await _context.CategoryBrands.AddAsync(categoryBrand);
                    await _context.SaveChangesAsync();
                }

                if (removedCategoryLength >= i)
                {
                    CategoryBrand categoryBrand = await _context.CategoryBrands.FirstOrDefaultAsync(c => c.CategoryId == removedCategory[i - 1] && c.BrandId == newBrand.Id);
                    _context.CategoryBrands.Remove(categoryBrand);
                    await _context.SaveChangesAsync();
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET Delete
        public async Task<IActionResult> Delete(int id)
        {
            Brand _brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (_brand == null) return RedirectToAction("Delete");
            return View(_brand);
        }

        // POST delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Brand brand)
        {
            Brand _brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == brand.Id);
            if (brand == null) return RedirectToAction("Delete");


            List<CategoryBrand> categoryBrand = await _context.CategoryBrands.ToListAsync();
            foreach (var item in categoryBrand)
            {
                CategoryBrand deletedBrand = await _context.CategoryBrands.FirstOrDefaultAsync(c => c.BrandId == brand.Id);
                if (deletedBrand != null)
                {
                    _context.CategoryBrands.Remove(deletedBrand);
                    await _context.SaveChangesAsync();
                }
            }
            _context.Brands.Remove(_brand);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            return View();
        }



    }
}