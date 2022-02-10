using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Allup_Backend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            ViewBag.ProductCount = 0;
            string basketCookie = Request.Cookies["basketCookie"];
            if (basketCookie != null)
            {
                List<BasketProduct> basketProductList = JsonConvert.DeserializeObject<List<BasketProduct>>(basketCookie);
                ViewBag.ProductCount = basketProductList.Count;

                //if total product number
                //foreach (var item in basketProductList)
                //{
                //    total += item.Count;
                //}
                //ViewBag.ProductCount = total;
            }

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
