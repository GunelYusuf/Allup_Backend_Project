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

        public IActionResult Details(int id)
        {
            return View();
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
        public async Task<ActionResult> Create(Brand brand, int[] subcategory)
        {
            bool isExist = _context.Brands.Any(b => b.Name.ToLower().Trim() == brand.Name.ToLower().Trim());
        
            if (isExist)
            {
                ModelState.AddModelError("Name", "The category with this name already exists");
                return RedirectToAction(nameof(Index));

            }

            Brand newBrand = new Brand();
            newBrand.Name = brand.Name;
            await _context.Brands.AddAsync(newBrand);
            await _context.SaveChangesAsync();

            if (subcategory != null)
            {
                foreach (var item in subcategory)
                {
                    CategoryBrand categoryBrands = new CategoryBrand();
                    categoryBrands.BrandId = newBrand.Id;
                    categoryBrands.CategoryId = item;
                    await _context.AddAsync(categoryBrands);
                    await _context.SaveChangesAsync();
                }

            }
            return Ok();
        }

        //Get Update
        public async Task<IActionResult> Update(int? id)
        {
            Brand brand = await _context.Brands.Include(b => b.CategoryBrands).ThenInclude(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
            List<CategoryBrand> SubCategory = await _context.CategoryBrands.Include(c => c.Category).Where(x => x.BrandId == brand.Id).ToListAsync();

            List<Category> AllCategory = await _context.Categories.Include(c => c.CategoryBrands).ThenInclude(c => c.Brand).Where(c => c.IsMain == false).ToListAsync();
            foreach (var item in SubCategory)
            {
                AllCategory.Remove(item.Category);

            }
            ViewBag.checkCategory = SubCategory;
            ViewBag.noneCheck = AllCategory;
            return View(brand);
        }
        //Get Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int? id, Brand brand, int[] subcategory)
        {
            bool isExist = _context.Brands.Any(b => b.Name.ToLower().Trim() == brand.Name.ToLower().Trim());
            Brand newBrand = await _context.Brands.FindAsync(id);

            if (isExist && !(newBrand.Name.ToLower() == brand.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "The category with this name already exists");
                return RedirectToAction(nameof(Index));
            }
            var allSubCategory = _context.Categories.Where(c => c.IsMain == false).ToList();
            var checkedCategory = _context.CategoryBrands.Where(c => c.BrandId == newBrand.Id).ToList();

            List<int> allCategoryList = new List<int>();
            List<int> checkedCategoryList = new List<int>();

            foreach (var item in allSubCategory)
            {
                allCategoryList.Add(item.Id);
            }

            foreach (var item in checkedCategory)
            {
                checkedCategoryList.Add(item.CategoryId);
            }

            var addedCategory = subcategory.Except(checkedCategoryList);
            var removedCategory = checkedCategoryList.Except(subcategory);

            newBrand.Name = brand.Name;

            foreach (var item in removedCategory)
            {
                CategoryBrand categoryBrand = await _context.CategoryBrands.Where(c => c.CategoryId == item && c.BrandId == newBrand.Id).FirstOrDefaultAsync();
                _context.CategoryBrands.Remove(categoryBrand);
                await _context.SaveChangesAsync();
            }
            if (addedCategory != null)
            {
                foreach (var item in addedCategory)
                {
                    Category category = _context.Categories.Find(item);
                    CategoryBrand categoryBrand = new CategoryBrand();
                    categoryBrand.BrandId = newBrand.Id;
                    categoryBrand.CategoryId = category.Id;
                    await _context.CategoryBrands.AddAsync(categoryBrand);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return NotFound();



        }


    }
}