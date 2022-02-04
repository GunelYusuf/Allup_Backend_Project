using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Allup_Backend.Extension;
using System.IO;

namespace Allup_Backend.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CompanySliderController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public CompanySliderController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            List<CompanySlider> companySliders = _context.CompanySliders.ToList();
            return View(companySliders);
        }

        //GET CompanySlider Create
        public IActionResult Create()
        {

            return View();
        }


        //Post CompanySlider Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Create(CompanySlider company)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Please, don't empty");
            }
            if (!company.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "just image");
                return View();
            }
            if (company.Photo.IsCorrectSize(300))
            {
                ModelState.AddModelError("Photo", "Enter the size correctly");
                return View();
            }

            CompanySlider newCompany = new CompanySlider();

            string fileName = await company.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/brand/");
            newCompany.ImageUrl = fileName;

            await _context.CompanySliders.AddAsync(newCompany);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET CompanySlider Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            CompanySlider dbCompany = await _context.CompanySliders.FindAsync(id);
            if (dbCompany == null) return NotFound();

            return View(dbCompany);

        }
        //Post CompanySlider Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteCompany(int? id)
        {
            if (id == null) return NotFound();
            CompanySlider dbCompany = await _context.CompanySliders.FindAsync(id);
            if (dbCompany == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "assets/images/brand/", dbCompany.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.CompanySliders.Remove(dbCompany);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //GET CompanySlider Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            CompanySlider dbCompany = await _context.CompanySliders.FindAsync(id);
            if (dbCompany == null) return NotFound();
            return View(dbCompany);
        }

        //Post CompanySlider Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,CompanySlider company)
        {
            if (id == null) return NotFound();

            if (company.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Please, don't empty");
                }

                if (!company.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "just image");
                    return View();
                }
                if (company.Photo.IsCorrectSize(400))
                {
                    ModelState.AddModelError("Photo", "Enter the size correctly");
                    return View();
                }
                CompanySlider dbCompany = await _context.CompanySliders.FindAsync(id);
                string path = Path.Combine(_env.WebRootPath, "assets/images/brand/", dbCompany.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string fileName = await company.Photo.SaveImageAsync(_env.WebRootPath, "assets/images/brand/");
                dbCompany.ImageUrl = fileName;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}