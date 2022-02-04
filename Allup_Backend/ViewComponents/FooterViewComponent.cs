using System;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Allup_Backend.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly Context _context;
        public FooterViewComponent(Context context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            Contact contact = _context.Contacts.FirstOrDefault();
            return View(await Task.FromResult(contact));

        }
    }
}
