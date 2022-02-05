using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class AboutController : Controller
    {

        private readonly Context _context;
       

        public AboutController(Context context)
        {
            _context = context;
           
        }
        public  IActionResult Index()
        {
             About about = _context.Abouts.FirstOrDefault();
             return View(about);
        }
    }
}
