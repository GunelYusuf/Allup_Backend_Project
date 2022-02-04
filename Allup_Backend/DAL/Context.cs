using System;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Allup_Backend.DAL
{
    public class Context:IdentityDbContext<AppUser>
    {
        public Context(DbContextOptions<Context> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Bio> Bios { get; set; }

        public DbSet<Footer> Footers { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<CommentBlog> CommentBlogs { get; set; }

        public DbSet<CommentProduct> CommentProducts { get; set; }

    }
}
