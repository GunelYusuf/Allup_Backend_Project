using System;
namespace Allup_Backend.Models
{
    public class ProductColor
    {
        public int Id { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
