using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Allup_Backend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class ContactController : Controller
    {

        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            ContactVM contactVM = new ContactVM();
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
              userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
              contactVM.User = await _userManager.FindByIdAsync(userId);
            }
        
            Contact contact = _context.Contacts.FirstOrDefault();
            contactVM.Contact = contact;
            return View(contactVM);

        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<ActionResult> Create(BillingAddress message)
        {
            if (User.Identity.IsAuthenticated)
            {
               var dataComment = new BillingAddress();
             
               dataComment.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
               dataComment.Subject = message.Subject;
               dataComment.Text = message.Text;
               await _context.BillingAddresses.AddAsync(dataComment);
               _context.SaveChanges();

                
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }

            return RedirectToAction(nameof(Index));

        }


    }
}
