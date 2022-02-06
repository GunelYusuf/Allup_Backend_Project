using System.Collections.Generic;
using System.Linq;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Allup_Backend.ViewModels;
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
            List<AuthorSlider> authorSliders = _context.AuthorSliders.ToList();

            HomeVM homeVM = new HomeVM();
            homeVM.AuthorSliders = authorSliders;

            return View(homeVM);
        }
    }
}
