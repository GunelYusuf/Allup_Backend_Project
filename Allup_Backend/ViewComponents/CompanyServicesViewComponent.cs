using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Allup_Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Allup_Backend.ViewComponents
{
    public class CompanyServicesViewComponent:ViewComponent
    {

        private readonly Context _context;
        public CompanyServicesViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CompanySlider> companySlider = _context.CompanySliders.ToList();
            List<ServicesSlider> servicesSliders = _context.ServicesSliders.ToList();

            CompanyServicesVM companyServicesVM = new CompanyServicesVM();
            companyServicesVM.CompanySliders = companySlider;
            companyServicesVM.ServicesSliders = servicesSliders;


            return View(await Task.FromResult(companyServicesVM));

        }
    }
}
