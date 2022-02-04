using System;
namespace Allup_Backend.Models
{
    public class CommentProduct
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public AppUser User { get; set; }

        public string UserId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}
