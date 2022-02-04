using System;
namespace Allup_Backend.Models
{
    public class CommentBlog
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public AppUser User { get; set; }

        public string UserId { get; set; }

        public Blog Blog { get; set; }

        public int BlogId { get; set; }
    }
}
