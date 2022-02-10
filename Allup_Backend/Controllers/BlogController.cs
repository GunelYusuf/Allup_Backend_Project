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
           
            ViewBag.user = user.FullName;
            ViewBag.tags = tags;
            return View(blog);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var blog = _context.Blogs.Include(bu => bu.User).ToList();
            var photos = _context.BlogImages.ToList();

            ViewBag.photos = photos;


            return View(blog);
           
        }

    }
}
