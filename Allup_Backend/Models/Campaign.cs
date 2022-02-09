using System;
using System.Collections.Generic;

namespace Allup_Backend.Models
{
    public class Campaign
    {
        public int Id { get; set; }

        public int Discount { get; set; }

        public List<Product> Products { get; set; }
    }
}
