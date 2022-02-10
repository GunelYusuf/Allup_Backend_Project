using System;
using System.Collections.Generic;
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
    public class HomeSliderController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public HomeSliderController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            List<HomeSlider> homeSliders = _context.HomeSliders.Include(s => s.Product).ToList();
            return View(homeSliders);
        }


        //Get Create
        public async Task<IActionResult> Create()
        {
            List<Product> products = await  _context.Products.Include(p => p.Images).ToListAsync();
            ViewBag.ProductList = products;
            return View();
        }


        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeSlider homeSlider)
        {

            bool isExist = _context.Products.Any(x => x.Id == homeSlider.ProductId);

            if (!isExist)
            {
                return RedirectToAction(nameof(Index));
            }

            if (homeSlider.Title.Length < 10 || homeSlider.Description.Length < 20)
            {
                return View();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Don't empty");
                return View();
            }

            if (!homeSlider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "just image");
                return View();
            }
            if (homeSlider.Photo.IsCorrectSize(300))
            {
                ModelState.AddModelError("Photo", "Enter the size correctly");
                return View();
            }

            HomeSlider newSlider = new HomeSlider();
            newSlider.Title =homeSlider.Title;
            newSlider.Description = homeSlider.Description;
            newSlider.ProductId = homeSlider.ProductId;
            string fileName = await homeSlider.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/product/");
            newSlider.ImageUrl = fileName;

            await _context.HomeSliders.AddAsync(newSlider);
            await _context.SaveChangesAsync();


            var products = await _context.Products
             .Include(x => x.Images).ToListAsync();

            ViewBag.ProductList = products;

            return RedirectToAction(nameof(Index));

           
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            HomeSlider homeSlider = _context.HomeSliders.Include(x => x.Product).FirstOrDefault(x => x.Id == id);
            var products = await _context.Products.Include(hs => hs.Images).ToListAsync();

            ViewBag.ProductList = products;
            return View(homeSlider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(HomeSlider homeSlider)
        {
            if (homeSlider.Title.Length < 10 || homeSlider.Description.Length < 20)
            {
                return View();
            }

            bool isExist = _context.Products.Any(x => x.Id == homeSlider.ProductId);

            if (!isExist)
            {
                return View();
            }

            HomeSlider newHomeSlider = await _context.HomeSliders.FirstOrDefaultAsync(x => x.Id == homeSlider.Id);

            if (newHomeSlider == null) return View();


            newHomeSlider.Title = homeSlider.Title;
            newHomeSlider.Description = homeSlider.Description;
            newHomeSlider.ProductId = homeSlider.ProductId;

            if (homeSlider.Photo != null)
            {
                if (!homeSlider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "just image");
                    return View();
                }
                if (homeSlider.Photo.IsCorrectSize(300))
                {
                    ModelState.AddModelError("Photo", "Enter the size correctly");
                    return View();
                }
                string fileName = await homeSlider.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/product/");
                newHomeSlider.ImageUrl = fileName;
            }

            await _context.SaveChangesAsync();


            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            HomeSlider homeSlider = _context.HomeSliders.Include(x => x.Product).FirstOrDefault(x => x.Id == id);
            var products = await _context.Products
             .Include(x => x.Images).ToListAsync();

            ViewBag.ProductList = products;
            return View(homeSlider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(HomeSlider homeSlider)
        {
            HomeSlider removeSlider = _context.HomeSliders.Include(x => x.Product).FirstOrDefault(x => x.Id == homeSlider.Id);
            if (removeSlider == null) return NotFound();

            _context.HomeSliders.Remove(removeSlider);
            await _context.SaveChangesAsync();
            return View();
        }
    }
}
