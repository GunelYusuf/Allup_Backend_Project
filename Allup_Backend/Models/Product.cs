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

        [Required]
        public string ProductCode { get; set; }

        public double  Tax { get; set; }

        public bool Availability { get; set; }
        [Required]
        public int Quantity { get; set; }

        public bool IsFeatured { get; set; }

        [NotMapped]
        [Required]

        public IFormFile[] Photos { get; set; }

        public int CampaignId { get; set; }

        public Campaign Campaign { get; set; }

        public Brand Brand { get; set; }

        public int BrandId { get; set; }

        public IEnumerable<CommentProduct> CommentProducts { get; set; }

        public List<ProductColor> ProductColors { get; set; }

        public List<ProductTag> ProductTags{ get; set; }

        public List<ProductImage> Images { get; set; }

        public int Count { get; set; }

    }
}
