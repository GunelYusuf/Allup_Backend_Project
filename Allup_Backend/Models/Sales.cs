using System;
using System.Collections.Generic;

namespace Allup_Backend.Models
{
    public class Sales
    {

        public int Id { get; set; }

        public double Total { get; set; }

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public DateTime SaleDate { get; set; }

        public List<ProductSales> ProductSales { get; set; }
    }
}
