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

namespace Allup_Backend.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ServicesSliderController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public ServicesSliderController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            List<ServicesSlider> servicesSliders = _context.ServicesSliders.ToList();
            return View(servicesSliders);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            ServicesSlider dbService = await _context.ServicesSliders.FindAsync(id);
            if (dbService == null) return NotFound();
            return View(dbService);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicesSlider services)
        {
            bool isExistService = _context.ServicesSliders.Any(s => s.Title.ToLower().Trim() == services.Title.ToLower().Trim());
            if (isExistService)
            {
                ModelState.AddModelError("Title", "The service with this title already exists");
                View();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Please, don't empty");
            }
            if (!services.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "just image");
                return View();
            }
            if (services.Photo.IsCorrectSize(300))
            {
                ModelState.AddModelError("Photo", "Enter the size correctly");
                return View();
            }

            ServicesSlider newService = new ServicesSlider();

            string fileName = await services.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/banner-icon/");
            newService.ImageUrl = fileName;
            newService.Description = services.Description;
            newService.Title = services.Title;
           

            await _context.ServicesSliders.AddAsync(newService);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            ServicesSlider dbServices = await _context.ServicesSliders.FindAsync(id);
            if (dbServices == null) return NotFound();

            return View(dbServices);

        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteServices(int? id)
        {
            if (id == null) return NotFound();
            ServicesSlider dbServices = await _context.ServicesSliders.FindAsync(id);
            if (dbServices == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "assets/images/banner-icon/", dbServices.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.ServicesSliders.Remove(dbServices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            ServicesSlider dbServices = await _context.ServicesSliders.FindAsync(id);
            if (dbServices == null) return NotFound();
            return View(dbServices);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ServicesSlider services)
        {
            if (id == null) return NotFound();

            if (!ModelState.IsValid) return View();

            bool isExist = _context.ServicesSliders.Any(s => s.Title.ToLower().Trim() == services.Title.ToLower().Trim());

            ServicesSlider isExistService = _context.ServicesSliders.FirstOrDefault(s => s.Id == services.Id);

            if (isExist && !(isExistService.Title.ToLower() == services.Title.ToLower().Trim()))
            {
                ModelState.AddModelError("Title", "The service with this title already exists");
                View();
            };



            if (services.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Please, don't empty");
                }

                if (!services.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "just image");
                    return View();
                }
                if (services.Photo.IsCorrectSize(400))
                {
                    ModelState.AddModelError("Photo", "Enter the size correctly");
                    return View();
                }
                ServicesSlider dbServices = await _context.ServicesSliders.FindAsync(id);
                string path = Path.Combine(_env.WebRootPath, "assets/images/banner-icon/", dbServices.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string fileName = await services.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/banner-icon/");


                dbServices.ImageUrl = fileName;
                dbServices.Title = services.Title;
                dbServices.Description = services.Description;
                await _context.SaveChangesAsync();
            }
           return RedirectToAction(nameof(Index));
        }
    }

}