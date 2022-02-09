using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class BlogController : Controller
    {

        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Detail(int? id)
        {
            var blog = await _context.Blogs.Include(x => x.BlogImage).FirstOrDefaultAsync(b => b.Id == id);
            var tags = await _context.ProductTags.Where(p => p.ProductId == blog.ProductId).Select(t => t.Tag).ToListAsync();
            var user = await _userManager.FindByIdAsync(blog.UserId);
            List<CommentBlog> comments = await _context.CommentBlogs.Where(c => c.BlogId == id).Include(x => x.User).ToListAsync();
            var photos = _context.BlogImages.ToList();
            var recentBlogs = _context.Blogs.Take(4).ToList();

            var product = _context.ProductTags.Include(p => p.Tag).ToList();
            var relatedPosts = _context.Blogs.Include(b => b.BlogImage).Include(b => b.Product).ThenInclude(p => p.ProductTags).ThenInclude(r => r.Tag).ToList();

            ViewBag.relatedPosts = relatedPosts.Where(r => r.Product.ProductTags[0].TagId == product[0].TagId);
            ViewBag.RecentBlogs = recentBlogs;
            ViewBag.photos = photos;
            ViewBag.user = user.FullName;
            ViewBag.tags = tags;
            ViewBag.comment = comments;

            return View(blog);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var blog = _context.Blogs.Include(bu => bu.User).ToList();

            var recentBlogs = _context.Blogs.Take(4).ToList();
            ViewBag.RecentBlogs = recentBlogs;

            var photos = _context.BlogImages.ToList();
            ViewBag.photos = photos;


            return View(blog);
           
        }

    }
}
