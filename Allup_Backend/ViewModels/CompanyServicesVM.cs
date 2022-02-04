using System;
using System.Collections.Generic;
using Allup_Backend.Models;

namespace Allup_Backend.ViewModels
{
    public class CompanyServicesVM
    {
        public List<CompanySlider> CompanySliders { get; set; }

        public List<ServicesSlider> ServicesSliders { get; set; }
    }
}
