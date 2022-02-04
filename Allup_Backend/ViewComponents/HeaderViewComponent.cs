using System;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Allup_Backend.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserName = user.FullName;
            };

            Bio bio = _context.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
            
        }

    }
}
