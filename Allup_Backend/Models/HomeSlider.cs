using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Allup_Backend.Models
{
    public class HomeSlider
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        [Required]

        public IFormFile Photo { get; set; }

        public Product Product { get; set; }

        public int ProductId  { get; set; }
    }
}
