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

        public DbSet<AuthorSlider> AuthorSliders { get; set; }

        public DbSet<CategoryBrand> CategoryBrands { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<ProductSales> ProductSales { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<ProductColor> ProductColors { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProductTag> ProductTags { get; set; }

        public DbSet<ProductRelated> ProductRelateds { get; set; }
    }
}
