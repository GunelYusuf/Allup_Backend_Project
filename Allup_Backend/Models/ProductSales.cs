using System;
namespace Allup_Backend.Models
{
    public class ProductSales
    {
        public int Id { get; set; }

        public int SalesId { get; set; }

        public Sales Sales { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
