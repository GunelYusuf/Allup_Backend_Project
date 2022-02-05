using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup_Backend.Areas.AdminArea.Controllers
{

    [Area("AdminArea")]
    public class MessageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly Context _context;
        private readonly IWebHostEnvironment _env;

        public MessageController(Context context, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<BillingAddress> billingAddresses = _context.BillingAddresses.Include(m=>m.User).ToList();

            return View(billingAddresses);
        }


        public async Task<IActionResult>  Detail(int? id)
        {
            List<BillingAddress> billingAddresses = _context.BillingAddresses.Include(m => m.User).ToList();
            var messages = billingAddresses.Find(u => u.Id == id);

            AppUser user = await _userManager.FindByIdAsync(messages.UserId);
            messages.User = user;

            return View(messages);

        }
        //Get
        public async Task<IActionResult> Delete(int? id)
        {

            List<BillingAddress> billingAddresses = _context.BillingAddresses.Include(m => m.User).ToList();
            var messages = billingAddresses.Find(u => u.Id == id);

            AppUser user = await _userManager.FindByIdAsync(messages.UserId);
            messages.User = user;
            return View(messages);

        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            BillingAddress message = await _context.BillingAddresses.FindAsync(id);
            _context.BillingAddresses.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }




    }
}