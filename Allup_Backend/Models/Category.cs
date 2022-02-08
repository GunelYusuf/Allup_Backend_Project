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

        public bool IsFeatured { get; set; }

        public bool IsDeleted { get; set; }

        public Category Parent { get; set; }

        public List<Category> Children { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public List<CategoryBrand> CategoryBrands { get; set; }


    }
}
