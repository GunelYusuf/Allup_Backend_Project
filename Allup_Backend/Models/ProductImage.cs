using System;
namespace Allup_Backend.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public bool IsMain { get; set; } 
    }
}
