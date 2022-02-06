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
    public class AboutController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public AboutController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }



        public IActionResult Index()
        {
            List<About> about = _context.Abouts.ToList();
            return View(about);
        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            About dbAbout = await _context.Abouts.FindAsync(id);
            if (dbAbout == null) return NotFound();
            return View(dbAbout);
        }

       
        //Get Create
        public IActionResult Create()
        {
            return View();
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.Abouts.Any(t => t.Title.ToLower().Trim() == about.Title.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Title", "The service with this title already exists");
                View();
            }

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Please, don't empty");
            }
            if (!about.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "just image");
                return View();
            }
            if (about.Photo.IsCorrectSize(300))
            {
                ModelState.AddModelError("Photo", "Enter the size correctly");
                return View();
            }

            About newAbout = new About();

            string fileName = await about.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/");

            newAbout.Title = about.Title;
            newAbout.Description = about.Description;
            newAbout.ImageUrl = fileName;


            await _context.Abouts.AddAsync(newAbout);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        //Get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            About dbAbout = await _context.Abouts.FindAsync(id);
            if (dbAbout == null) return NotFound();
            return View(dbAbout);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteAbout(int? id)
        {
            About dbAbout = await _context.Abouts.FindAsync(id);
            if (dbAbout == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "assets/images/", dbAbout.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Abouts.Remove(dbAbout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Get Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            About dbAbout = await _context.Abouts.FindAsync(id);
            if (dbAbout == null) return NotFound();
            return View(dbAbout);
        }

        //Post Update

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,About about)
        {
            if (id == null) return NotFound();

            bool isExist = _context.Abouts.Any(t => t.Title.ToLower().Trim() == about.Title.ToLower().Trim());
            
            About isExistAbout = _context.Abouts.FirstOrDefault(a => a.Id == about.Id);

            if (isExist && !(isExistAbout.Title.ToLower() == about.Title.ToLower().Trim()))
            {
                ModelState.AddModelError("Title", "The service with this title already exists");
                View();
            };



            if (about.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Please, don't empty");
                }

                if (!about.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "just image");
                    return View();
                }
                if (about.Photo.IsCorrectSize(400))
                {
                    ModelState.AddModelError("Photo", "Enter the size correctly");
                    return View();
                }
                About dbAbout = await _context.Abouts.FindAsync(id);
                string path = Path.Combine(_env.WebRootPath, "assets/images/", about.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string fileName = await about.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/");


                dbAbout.ImageUrl = fileName;
                dbAbout.Title = about.Title;
                dbAbout.Description = about.Description;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}