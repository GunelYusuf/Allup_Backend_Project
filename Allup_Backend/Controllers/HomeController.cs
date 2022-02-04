using System.Collections.Generic;
using System.Linq;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Allup_Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }


        public  IActionResult Index()
        {
           return View();
        }
    }
}
