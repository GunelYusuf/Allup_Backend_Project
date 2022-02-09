using System;
namespace Allup_Backend.Models
{
    public class ProductRelated
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }
        
    }
}
