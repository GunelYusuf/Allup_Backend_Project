using System.Linq;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class ContactController : Controller
    {

        private readonly Context _context;

        public ContactController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Contact contact = _context.Contacts.FirstOrDefault();


            return View(contact);
        }
    }
}
