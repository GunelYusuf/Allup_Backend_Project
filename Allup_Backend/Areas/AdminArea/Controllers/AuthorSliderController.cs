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
    public class AuthorSliderController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public AuthorSliderController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<AuthorSlider> author = _context.AuthorSliders.ToList();
            return View(author);
        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            AuthorSlider dbAuthor = await _context.AuthorSliders.FindAsync(id);
            if (dbAuthor == null) return NotFound();
            return View(dbAuthor);
        }

        //Get Create
        public IActionResult Create()
        {
            return View();
        }


        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorSlider article)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.AuthorSliders.Any(a => a.AuthorName.ToLower().Trim() == article.AuthorName.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("AuthorName", "The author with this description already exists");
                View();
            }

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Please, don't empty");
            }
            if (!article.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "just image");
                return View();
            }
            if (article.Photo.IsCorrectSize(300))
            {
                ModelState.AddModelError("Photo", "Enter the size correctly");
                return View();
            }

            AuthorSlider newAuthorSLider = new AuthorSlider();

            string fileName = await article.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/author");

            newAuthorSLider.AuthorName = article.AuthorName;
            newAuthorSLider.Description = article.Description;
            newAuthorSLider.Email = article.Email;
            newAuthorSLider.ImageUrl = fileName;


            await _context.AuthorSliders.AddAsync(newAuthorSLider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        //Get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            AuthorSlider dbAuthor = await _context.AuthorSliders.FindAsync(id);
            if (dbAuthor == null) return NotFound();
            return View(dbAuthor);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteAbout(int? id)
        {
            AuthorSlider dbAuthor = await _context.AuthorSliders.FindAsync(id);
            if (dbAuthor == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "assets/images/author", dbAuthor.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.AuthorSliders.Remove(dbAuthor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Get Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            AuthorSlider dbAuthor = await _context.AuthorSliders.FindAsync(id);
            if (dbAuthor == null) return NotFound();
            return View(dbAuthor);
        }

        //Post Update

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, AuthorSlider article)
        {
            if (id == null) return NotFound();

            bool isExist = _context.AuthorSliders.Any(a => a.AuthorName.ToLower().Trim() == article.AuthorName.ToLower().Trim());

            AuthorSlider isExistAuthor = _context.AuthorSliders.FirstOrDefault(a => a.Id == article.Id);

            if (isExist && !(isExistAuthor.AuthorName.ToLower() == article.AuthorName.ToLower().Trim()))
            {
                ModelState.AddModelError("Author", "The author with this description already exists");
                View();
            };



            if (article.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Please, don't empty");
                }

                if (!article.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "just image");
                    return View();
                }
                if (article.Photo.IsCorrectSize(400))
                {
                    ModelState.AddModelError("Photo", "Enter the size correctly");
                    return View();
                }
                AuthorSlider dbAuthor = await _context.AuthorSliders.FindAsync(id);
                
                string path = Path.Combine(_env.WebRootPath, "assets/images/author", dbAuthor.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string fileName = await article.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/author");


                dbAuthor.AuthorName = article.AuthorName;
                dbAuthor.Description = article.Description;
                dbAuthor.Email = article.Email;
                dbAuthor.ImageUrl = fileName;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}