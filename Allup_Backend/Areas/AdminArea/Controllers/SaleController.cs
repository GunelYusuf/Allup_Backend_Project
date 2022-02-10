using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup_Backend.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SaleController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly Context _context;

        public SaleController(UserManager<AppUser> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            List<Sales> sales = _context.Sales.Include(p => p.ProductSales).ThenInclude(p => p.Product).Include(u => u.AppUser).ToList();
            return View(sales);
        }

        public async Task<IActionResult> Detail(int? Id)
        {
            Sales sales = await _context.Sales.Include(p => p.ProductSales).ThenInclude(p => p.Product).Include(u => u.AppUser).FirstOrDefaultAsync();
            return View(sales);
        }
    }
}