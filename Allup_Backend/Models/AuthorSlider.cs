using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Allup_Backend.Models
{
    public class AuthorSlider
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string AuthorName { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }


        [NotMapped]
        [Required]

        public IFormFile Photo { get; set; }
    }
}
