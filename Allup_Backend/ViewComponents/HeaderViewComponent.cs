using System;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Allup_Backend.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly Context _context;

        public HeaderViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Bio bio = _context.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }

    }
}
