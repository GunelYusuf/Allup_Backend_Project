using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Allup_Backend.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "can not be empty")]
        public string Name { get; set; }

        public List<CategoryBrand> CategoryBrands{ get; set; }

        public List<Product> Products { get; set; }
    }
}
