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

        public  DbSet<Contact>  Contacts { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<CommentBlog> CommentBlogs { get; set; }

        public DbSet<CommentProduct> CommentProducts { get; set; }

        public DbSet<CompanySlider> CompanySliders { get; set; }

        public DbSet<ServicesSlider> ServicesSliders { get; set; }

        public DbSet<Subscribe> Subscribes { get; set; }

        public DbSet<BillingAddress> BillingAddresses { get; set; }

        public DbSet<About> Abouts { get; set; }

    }
}
