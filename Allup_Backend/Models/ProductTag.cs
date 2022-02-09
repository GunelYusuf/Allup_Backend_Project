using System;
namespace Allup_Backend.Models
{
    public class ProductTag
    {
        public int Id { get; set; }
       
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
