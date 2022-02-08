using System;
namespace Allup_Backend.Models
{
    public class BlogImage
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public Blog Blog { get; set; }

        public int BlogId { get; set; }

    }
}
