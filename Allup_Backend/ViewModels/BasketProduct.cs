using System;
namespace Allup_Backend.ViewModels
{
    public class BasketProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public double Price { get; set; }

        public int Count { get; set; }

        public int Discount { get; set; }

        public int BrandId { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }
    }
}
