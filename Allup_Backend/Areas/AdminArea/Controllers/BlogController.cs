using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Extension;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup_Backend.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BlogController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public BlogController(Context context, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.Include(p => p.Product).ToList();
            return View(blogs);
        }

        public  async Task<IActionResult> Detail(int? id)
        {
            Blog blogs = await _context.Blogs.Include(p => p.Product).Include(u => u.UserId).FirstOrDefaultAsync();
            return View(blogs);
        }

        //Get Create
        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }

        //Post Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            //ViewBag.Tags = _context.Tags.ToList();
            bool isExist = _context.Blogs.Any(b => b.Title.ToLower().Trim() == blog.Title.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Title", "The blog with this title already exists");
                return RedirectToAction("Index");
            }
            Product dbProduct = await _context.Products.FindAsync(blog.ProductId);

            if (dbProduct==null) return NotFound();
            
            if (blog.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Please, don't empty");
                }
                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "just image");
                    return View();
                }
                if (blog.Photo.IsCorrectSize(300))
                {
                    ModelState.AddModelError("Photo", "Enter the size correctly");
                    return View();
                }
                Blog newBlog = new Blog();
               
                string fileName = await blog.Photo.SaveImageAsync(_env.WebRootPath, "images/");

                newBlog.Title = blog.Title;
                newBlog.Description = blog.Description;
                newBlog.Date = blog.Date;
                newBlog.CommentBlogs = blog.CommentBlogs;
                newBlog.ProductId = blog.ProductId;
                newBlog.ImageUrl = fileName;
                await _context.Blogs.AddAsync(newBlog);
                await _context.SaveChangesAsync();
            
            }
            
            var tagProduct = _context.ProductTags.Where(p => p.ProductId == blog.ProductId).Select(t => t.Tag).ToList();
            if ( tagProduct != null)
            {
                foreach (var item in tagProduct)
                {
                    ProductTag productTag = new ProductTag()
                    {
                        ProductId =dbProduct.Id,
                        TagId = item.Id
                       
                    };
                    await _context.ProductTags.AddAsync(productTag);
                    await _context.SaveChangesAsync();
                }
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}