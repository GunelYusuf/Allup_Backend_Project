using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Extension;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Allup_Backend.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(Context context, UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }
        
        public IActionResult Index()
        {
            List<Blog> blog = _context.Blogs.ToList();
            return View(blog);
        }

      
        public IActionResult Details(int id)
        {
            return View();
        }

        
        public IActionResult Create()
        {
            var products = new SelectList(_context.Products.OrderBy(l => l.Price)
            .ToDictionary(us => us.Id, us => us.Name), "Key", "Value");
            ViewBag.ProductId = products;

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, string videourl)
        {
            if (blog.Photos == null && videourl == null) return NotFound();
            Blog newBlog = new Blog()
            {
                Title = blog.Title,
                Description = blog.Description,
                ProductId = blog.ProductId,
                Date = blog.Date,
                UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
            };
            await _context.Blogs.AddAsync(newBlog);
            await _context.SaveChangesAsync();


            if (videourl != null)
            {
                BlogImage blogImage = new BlogImage();
                blogImage.VideoUrl = videourl;
                blogImage.BlogId = newBlog.Id;
                await _context.BlogImages.AddAsync(blogImage);
                await _context.SaveChangesAsync();
            }

            if (blog.Photos != null)
            {
                if (ModelState["Photos"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Don't empty");
                }

                foreach (IFormFile photo in blog.Photos)
                {
                    if (!photo.IsImage())
                    {
                        ModelState.AddModelError("Photo", "just image");
                        return RedirectToAction("Index");
                    }
                    if (photo.IsCorrectSize(300))
                    {
                        ModelState.AddModelError("Photo", "please enter photo under 300kb");
                        return RedirectToAction("Index");
                    }
                    BlogImage blogImage = new BlogImage();

                    string fileName = await photo.SaveImageAsync(_env.WebRootPath, "assets/images/");

                    blogImage.ImageUrl = fileName;
                    blogImage.BlogId = newBlog.Id;
                    await _context.BlogImages.AddAsync(blogImage);
                    await _context.SaveChangesAsync();
                }

            }
            return RedirectToAction(nameof(Index));
        }


      





    }


}
