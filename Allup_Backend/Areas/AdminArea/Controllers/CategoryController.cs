using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Extension;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup_Backend.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public CategoryController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.Include(cb => cb.CategoryBrands).ThenInclude(b => b.Brand).ToList();

            return View(categories);
          
        }

        //Detail
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            return View(dbCategory);
        }

        public async Task<IActionResult> Active(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            dbCategory.IsDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

       
        // Get Create
        public IActionResult Create()
        {
            List<Category> Parent = _context.Categories.Where(p => p.IsMain == true && p.IsDeleted == false).ToList();
            ViewBag.Category = Parent;
            return View();
        }

        //Post Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            bool isExist = _context.Categories.Any(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "The category with this name already exists");
                return RedirectToAction("Index");

            }
            if (!category.IsMain)
            {
                Category children = new Category();
                List<Category> db = _context.Categories.Where(c => c.Id == category.Parent.Id).ToList();


                children.Parent = db.FirstOrDefault();
                children.Name = category.Name;

                if (children.Parent == null)
                {

                    return NotFound();

                }
                await _context.Categories.AddAsync(children);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Please, don't empty");
            }
            if (!category.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "just image");
                return View();
            }
            if (category.Photo.IsCorrectSize(300))
            {
                ModelState.AddModelError("Photo", "Enter the size correctly");
                return View();
            }

            string fileName = await category.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/");
            Category Parent = new Category
            {
                Name = category.Name,
                IsMain = category.IsMain,
                IsFeatured = category.IsFeatured,
                ImageUrl = fileName,
            };

            await _context.Categories.AddAsync(Parent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        //Get Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();

            List<Category> Parent = _context.Categories.Where(x => x.IsMain == true && x.IsDeleted == false).ToList();
            ViewBag.Category =Parent;

            return View(dbCategory);
        }

        //Post Update
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            bool isExist = _context.Categories.Any(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            Category newCategory = await _context.Categories.FindAsync(id);

          
            if (isExist && !(newCategory.Name.ToLower() == category.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "The category with this name already exists");
                await _context.SaveChangesAsync();
                View();
            }

            if (!category.IsMain)
            {
                newCategory.Parent = category.Parent;
                newCategory.Name = category.Name;
                return RedirectToAction(nameof(Index));

            }
            if (category.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Please, don't empty");
                }
                if (!category.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "just image");
                    return View();
                }
                if (category.Photo.IsCorrectSize(300))
                {
                    ModelState.AddModelError("Photo", "Enter the size correctly");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath, "assets/images/", newCategory.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string fileName = await category.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/");
                newCategory.IsFeatured = category.IsFeatured;
                newCategory.Name = category.Name;
                newCategory.ImageUrl = fileName;

            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        // Get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            return View(dbCategory);
        }

        // Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Category category)
        {
            if (id == null) return NotFound();
            Category dbCategory = await _context.Categories.FindAsync(id);
            if (dbCategory == null) return NotFound();
            if (dbCategory.IsMain)
            {
                bool isChildren = _context.Categories.Any(c => c.Parent.Id == category.Id);
                if (isChildren)
                {
                    dbCategory.IsDeleted = true;
                }
                else
                {
                    string path = Path.Combine(_env.WebRootPath, "assets/images/", dbCategory.ImageUrl);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    _context.Categories.Remove(dbCategory);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}