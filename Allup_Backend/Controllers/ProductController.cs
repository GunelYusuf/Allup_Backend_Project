using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class ProductController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
                List<Product> product = _context.Products.Include(p=>p.Images)
                .Include(b => b.Brand)
                .Include(c => c.Campaign)
                .Include(pc => pc.ProductColors)
                .Include(pt => pt.ProductTags).ToList();
             

            return View(product);
        }


        public IActionResult Detail(int id)
        {
            IEnumerable<CommentProduct> comments = _context.CommentProducts.Where(c => c.ProductId == id);

            Product product = _context.Products.Include(b => b.Brand).Include(i => i.Images).Include(p => p.Campaign).Include(p => p.ProductColors).Include(p => p.ProductTags).FirstOrDefault(p => p.Id == id);

            ViewBag.tags = _context.ProductTags.Include(p => p.Tag).Where(p => p.ProductId == id).ToList();

            return View(product);


        }
    }
}
