namespace Allup_Backend.Models
{
    public class CategoryBrand
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}
