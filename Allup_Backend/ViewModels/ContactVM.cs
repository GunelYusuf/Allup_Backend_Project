using System;
using System.Collections.Generic;
using Allup_Backend.Models;

namespace Allup_Backend.ViewModels
{
    public class ContactVM
    {
        public AppUser User{ get; set; }

        public Contact Contact { get; set; }

        public BillingAddress BillingAddresses { get; set; }

    }
}
