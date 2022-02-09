using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Extension;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Allup_Backend.Areas.AdminArea.Controllers
{

    [Area("AdminArea")]
    public class ProductController : Controller
    {

        private readonly IWebHostEnvironment _env;
        private readonly Context _context;

        public ProductController(Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            List<Product> product = _context.Products.Include(c => c.Campaign).Include(b => b.Brand).ToList();
            return View(product);
        }

        public IActionResult CallBackCategory(int? id)
        {
            List<Category> categories = _context.CategoryBrands.Where(b => b.BrandId == id)
                .Select(c => c.Category)
                .ToList();

            return PartialView("_CreateProductPartial", categories);
        }

        //Get Create
        public IActionResult Create()
        {
            var brands = new SelectList(_context.Brands.OrderBy(l => l.Name)
            .ToDictionary(us => us.Id, us => us.Name), "Key", "Value");
            ViewBag.Brand = brands;

            var campaign = new SelectList(_context.Campaigns.OrderBy(l => l.Discount)
             .ToDictionary(us => us.Id, us => us.Discount), "Key", "Value");
            ViewBag.Campaign = campaign;

            var colors = _context.Colors.ToList();
            ViewBag.Colors = colors;

            var tags = _context.Tags.ToList();
            ViewBag.Tags = tags;
          
            return View();
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product product, int categoryId, int[] colorId, int[] tagId)
        {
            bool isExist = _context.Categories.Any(c => c.Name.ToLower().Trim() == product.Name.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name","The category with this name already exists");

                return RedirectToAction(nameof(Index));
            }
            if (categoryId == 0) return NotFound();

            Product newProduct = new Product()
            {
                Name = product.Name,
                BrandId = product.BrandId,
                CampaignId = product.CampaignId,
                Price = product.Price,
                Availability = product.Availability,
                Tax = product.Tax,
                Description = product.Description,
                Quantity = product.Quantity,
                IsFeatured = product.IsFeatured,
                ProductCode = product.ProductCode
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            ProductRelated productRelated = new ProductRelated()
            {
                ProductId = newProduct.Id,
                BrandId = newProduct.BrandId,
                CategoryId = categoryId
            };
            await _context.ProductRelateds.AddAsync(productRelated);


            if (ModelState["Photos"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Don't empty");
            }
            var count = 0;
            foreach (IFormFile photo in product.Photos)
            {
                if (!photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "just image");
                    return RedirectToAction(nameof(Index));

                }
                if (photo.IsCorrectSize(300))
                {
                    ModelState.AddModelError("Photo", "Enter the size correctly");
                    return RedirectToAction("Index");
                }
                ProductImage productImage = new ProductImage();

                string fileName = await photo.SaveImageAsync(_env.WebRootPath, "assets/images/product/");

                if (count == 0) productImage.IsMain = true;
                productImage.ImageUrl = fileName;
                productImage.ProductId = newProduct.Id;

                await _context.ProductImages.AddAsync(productImage);
                await _context.SaveChangesAsync();
                count++;
            }

            if (colorId != null && tagId != null )
            {
                foreach (var item in colorId)
                {
                    ProductColor productColor = new ProductColor()
                    {
                        ProductId = newProduct.Id,
                        ColorId = item,
                    };
                    await _context.ProductColors.AddAsync(productColor);
                    await _context.SaveChangesAsync();
                }
                foreach (var item in tagId)
                {
                    ProductTag productTag = new ProductTag()
                    {
                        ProductId = newProduct.Id,
                        TagId = item,
                    };
                    await _context.ProductTags.AddAsync(productTag);
                    await _context.SaveChangesAsync();
                }
              
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}