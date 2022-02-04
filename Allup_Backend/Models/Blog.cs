using System;
using System.Collections.Generic;

namespace Allup_Backend.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public AppUser User { get; set; }

        public string UserId { get; set; }

        public IEnumerable<CommentBlog> CommentBlogs { get; set; }
    }
}
