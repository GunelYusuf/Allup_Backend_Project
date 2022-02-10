using System.Collections.Generic;
using System.Linq;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Allup_Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public  IActionResult Index()
        {
            HttpContext.Session.SetString("AllUp", "E-commerce");

            List<AuthorSlider> authorSliders = _context.AuthorSliders.ToList();
            List<Category> categories = _context.Categories.Where(c => c.IsFeatured == true).ToList();
            List<HomeSlider> homeSliders = _context.HomeSliders.Include(x => x.Product).ToList();
            List<Product> products = _context.Products.Where(pr => pr.IsDeleted == false).Include(pro => pro.Images).ToList();

            HomeVM homeVM = new HomeVM();
            homeVM.AuthorSliders = authorSliders;
            homeVM.categories = categories;
            homeVM.HomeSliders = homeSliders;
            homeVM.Products = products;
            ViewBag.FeatCategories = categories.Where(c => c.IsFeatured == true);

            return View(homeVM);
        }

        public IActionResult GetSession()
        {
            string session = HttpContext.Session.GetString("Software");
            return Content(session);
        }
    }
}
