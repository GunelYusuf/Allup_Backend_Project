using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Allup_Backend.Models
{
    public class About
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(260), MinLength(5)]

        public string ImageUrl { get; set; }

        [NotMapped]
        [Required]

        public IFormFile Photo { get; set; }
    }
}
