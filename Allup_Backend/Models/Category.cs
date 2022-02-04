using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Allup_Backend.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "don't leave empty")]
        public string Name { get; set; }

        public bool IsMain { get; set; }

        public Category Parent { get; set; }

        public List<Category> Children { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile CategoryImage { get; set; }

        public ICollection<CategoryBrand> CategoryBrands { get; set; }


    }
}
