using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Allup_Backend.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public double  Tax { get; set; }

        public bool Availability { get; set; }

        public int Quantity { get; set; }

        public bool IsFeatured { get; set; }

        public ICollection<ProductImage> Images { get; set; }

        public Brand Brand { get; set; }

        public int BrandId { get; set; }

        public IEnumerable<CommentProduct> CommentProducts { get; set; }
    }
}
