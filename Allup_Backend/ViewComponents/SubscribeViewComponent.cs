using System;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Allup_Backend.ViewComponents
{
    public class SubscribeViewComponent:ViewComponent
    {

        private readonly Context _context;

        public SubscribeViewComponent(Context context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            Subscribe subscribe = _context.Subscribes.FirstOrDefault();
            return View(await Task.FromResult(subscribe));

        }
    }
}
