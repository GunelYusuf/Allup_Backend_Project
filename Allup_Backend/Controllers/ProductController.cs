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
            List<Product> products = _context.Products.Include(p => p.BrandId).Take(8).ToList();
            return View(products);
        }


        public IActionResult Detail(int id)
        {
            IEnumerable<CommentProduct> comments = _context.CommentProducts.Where(c => c.ProductId == id);

            Product product = _context.Products.Include(b => b.BrandId).Include(i => i.Images).ThenInclude(pi => pi.ImageUrl).FirstOrDefault(p => p.Id == id);
            ViewBag.ProductID = product.Id;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.UserID = userId;
            Product products = new Product
            {
                Name = product.Name,
                Price = product.Price,
                BrandId = product.BrandId,
                CommentProducts = comments,
            };
            return View(products);

        }
    }
}
