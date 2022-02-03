using System;
using System.Collections.Generic;

namespace Allup_Backend.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CategoryBrand> CategoryBrands{ get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
