using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Allup_Backend.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ContactController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _env;

        public ContactController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
           List<Contact> contacts = _context.Contacts.ToList();
           return View(contacts);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        //GET Contact Create
        public ActionResult Create()
        {
            return View();
        }

        //Post Contact Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            Contact addNewContact = new Contact
            {
                Address = contact.Address,
                Description = contact.Description,
                PhoneNumber = contact.PhoneNumber,
                MapUrl = contact.MapUrl,
                EmailAddress = contact.EmailAddress,
                OpenCloseClocks = contact.OpenCloseClocks
            };

            await _context.AddAsync(addNewContact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET Contact Update
        public async Task<ActionResult>Update(int? id)
        {
            if (id == null) return NotFound();
            Contact dbContact = await _context.Contacts.FindAsync(id);
            if (dbContact == null) return NotFound();
            return View(dbContact);
        }

        //Post Contact Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Contact contact)
        {
            Contact dbContact = _context.Contacts.FirstOrDefault(c => c.Id==contact.Id);
            dbContact.Address = contact.Address;
            dbContact.Description = contact.Description;
            dbContact.EmailAddress = contact.EmailAddress;
            dbContact.PhoneNumber = contact.PhoneNumber;
            dbContact.MapUrl = contact.MapUrl;
            dbContact.OpenCloseClocks = contact.OpenCloseClocks;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Contac");

        }

        //GET Contact Delete
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Contact dbContact = await _context.Contacts.FindAsync(id);
            if (dbContact == null) return NotFound();
            return View(dbContact);
        }

        //Post Contact Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Contact dbContact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(dbContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}