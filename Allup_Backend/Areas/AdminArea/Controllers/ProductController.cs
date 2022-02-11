using System;
using System.Collections.Generic;
using System.IO;
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


        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult CallCategory(int? id)
        {
            List<Category> categories = _context.CategoryBrands.Where(b => b.BrandId == id)
                .Select(c => c.Category).ToList();

            return PartialView("_ProductCreatePartial", categories);
        }

        //Get Create
        public ActionResult Create()
        {

            var brands = new SelectList(_context.Brands.OrderBy(l => l.Name)
            .ToDictionary(us => us.Id, us => us.Name), "Key", "Value");
            ViewBag.Brand = brands;
            var campaign = new SelectList(_context.Campaigns.OrderBy(l => l.Discount)
             .ToDictionary(us => us.Id, us => us.Discount), "Key", "Value");
            ViewBag.Campaign = campaign;
            var colors = _context.Colors.ToList();
            var tags = _context.Tags.ToList();
            ViewBag.Tags = tags;
            ViewBag.Colors = colors;

            return View();
        }

        // Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, int categoryid, int[] tagId, int[] colorId)
        {

            if (categoryid == 0) return NotFound();

            Product newproduct = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                CampaignId = product.CampaignId,
                BrandId = product.BrandId,
                Availability = product.Availability,
                Description = product.Description,
                Tax = product.Tax,
                Quantity = product.Quantity,
                IsFeatured = product.IsFeatured,
                ProductCode = product.ProductCode
            };


            await _context.Products.AddAsync(newproduct);
            await _context.SaveChangesAsync();
            ProductRelated productRelated = new ProductRelated()
            {
                ProductId = newproduct.Id,
                BrandId = newproduct.BrandId,
                CategoryId = categoryid
            };
            await _context.ProductRelateds.AddAsync(productRelated);
            if (tagId != null && colorId != null)
            {
                foreach (var item in colorId)
                {
                    ProductColor productColor = new ProductColor()
                    {
                        ProductId = newproduct.Id,
                        ColorId = item,
                    };
                    await _context.ProductColors.AddAsync(productColor);
                    await _context.SaveChangesAsync();
                }
                foreach (var item in tagId)
                {
                    ProductTag productTag = new ProductTag()
                    {
                        ProductId = newproduct.Id,
                        TagId = item,
                    };
                    await _context.ProductTags.AddAsync(productTag);
                    await _context.SaveChangesAsync();
                }
               
            }

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
                productImage.ProductId = newproduct.Id;
                await _context.ProductImages.AddAsync(productImage);
                await _context.SaveChangesAsync();
                count++;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Get Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _context.Products.FindAsync(id);

            var relation = _context.ProductRelateds.Where(b => b.ProductId == id && b.BrandId == product.BrandId).FirstOrDefault();
            Category category = await _context.Categories.FindAsync(relation.CategoryId);

            var brands = new SelectList(_context.Brands.OrderBy(l => l.Name)
            .ToDictionary(us => us.Id, us => us.Name), "Key", "Value");
            ViewBag.BrandId = brands;

            var campaign = new SelectList(_context.Campaigns.OrderBy(l => l.Discount)
             .ToDictionary(us => us.Id, us => us.Discount), "Key", "Value");
            ViewBag.CampaignId = campaign;

            ViewBag.category = category;
            var photos = _context.ProductImages.Where(p => p.ProductId == id).ToList();
            ViewBag.photos = photos;

            var checkTag = await _context.ProductTags.Where(p => p.ProductId == id).Select(t => t.Tag).ToListAsync();
            var checkColor = await _context.ProductColors.Where(p => p.ProductId == id).Select(c => c.Color).ToListAsync();
            ViewBag.checkTag = checkTag;
            ViewBag.checkColor = checkColor;

            var allTag = await _context.Tags.ToListAsync();
            var allColor = await _context.Colors.ToListAsync();

            var noneCheckTag = allTag.Except(checkTag);
            var noneCheckColor = allColor.Except(checkColor);
            ViewBag.noneTag = noneCheckTag;
            ViewBag.noneColor = noneCheckColor;

            return View(product);
        }

        // Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product, int categoryid, List<int> tagId, List<int> colorId)
        {
            if (categoryid == null) return RedirectToAction("Update", "Product");

            Product newProduct = await _context.Products.FindAsync(id);
            var relationProduct = _context.ProductRelateds.Where(p => p.ProductId == id && p.BrandId == newProduct.BrandId).FirstOrDefault();

            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.CampaignId = product.CampaignId;
            newProduct.BrandId = product.BrandId;
            newProduct.Availability = product.Availability;
            newProduct.Description = product.Description;
            newProduct.Tax = product.Tax;
            newProduct.Quantity = product.Quantity;
            newProduct.IsFeatured = product.IsFeatured;
            newProduct.ProductCode = product.ProductCode;

            await _context.SaveChangesAsync();

            if (relationProduct.CategoryId != categoryid || relationProduct.BrandId != newProduct.BrandId)
            {
                _context.ProductRelateds.Remove(relationProduct);

                ProductRelated newProductRelation = new ProductRelated();
                newProductRelation.CategoryId = categoryid;
                newProductRelation.BrandId = newProduct.BrandId;
                newProductRelation.ProductId = newProduct.Id;

                await _context.ProductRelateds.AddAsync(newProductRelation);
                await _context.SaveChangesAsync();
            }

            List<int> checkTag = _context.ProductTags.Where(p => p.ProductId == newProduct.Id).Select(t => t.TagId).ToList();
            List<int> checkColor = _context.ProductColors.Where(c => c.ProductId == newProduct.Id).Select(c => c.ColorId).ToList();

            List<int> addedTag = tagId.Except(checkTag).ToList();
            List<int> removeTag = checkTag.Except(tagId).ToList();

            List<int> addedColor = colorId.Except(checkColor).ToList();
            List<int> removeColor = checkColor.Except(colorId).ToList();
            int addedTagLength = addedTag.Count();
            int removedTagLength = removeTag.Count();
            int FullLength = addedTagLength + removedTagLength;

            int addedColorLength = addedColor.Count();
            int removedColorLength = removeColor.Count();
            int FullLengthColor = addedColorLength + removedColorLength;



            for (int i = 1; i <= FullLength; i++)
            {
                if (addedTagLength >= i)
                {
                    ProductTag productTag = new ProductTag();

                    productTag.ProductId = newProduct.Id;
                    productTag.TagId = addedTag[i - 1];

                    await _context.ProductTags.AddAsync(productTag);
                    await _context.SaveChangesAsync();
                }

                if (removedTagLength >= i)
                {
                    ProductTag productTag = await _context.ProductTags.FirstOrDefaultAsync(c => c.TagId == removeTag[i - 1] && c.ProductId == newProduct.Id);
                    _context.ProductTags.Remove(productTag);
                    await _context.SaveChangesAsync();
                }
            }

            for (int i = 1; i <= FullLengthColor; i++)
            {
                if (addedTagLength >= i)
                {
                    ProductColor productColor = new ProductColor();

                    productColor.ProductId = newProduct.Id;
                    productColor.ColorId = addedColor[i - 1];

                    await _context.ProductColors.AddAsync(productColor);
                    await _context.SaveChangesAsync();
                }

                if (removedTagLength >= i)
                {
                    ProductColor productColor = await _context.ProductColors.FirstOrDefaultAsync(c => c.ColorId == removeColor[i - 1] && c.ProductId == newProduct.Id);
                    _context.ProductColors.Remove(productColor);
                    await _context.SaveChangesAsync();
                }
            }

            if (product.Photos != null)
            {
                if (ModelState["Photos"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Don't empty");
                }
                var count = 0;
                var oldPhoto = _context.ProductImages.Where(p => p.ProductId == newProduct.Id).ToList();


                if (oldPhoto.Count <= product.Photos.Length)
                {
                    foreach (var item in oldPhoto)
                    {
                        string path = Path.Combine(_env.WebRootPath, "assets/images/product/", item.ImageUrl);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        _context.ProductImages.Remove(item);
                        await _context.SaveChangesAsync();
                    }
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


                }
                else
                {
                    for (int i = 0; i < product.Photos.Length; i++)
                    {
                        if (!product.Photos[i].IsImage())
                        {
                            ModelState.AddModelError("Photo", "just image");
                            return RedirectToAction("Index");

                        }
                        if (product.Photos[i].IsCorrectSize(300))
                        {
                            ModelState.AddModelError("Photo", "Enter the size correctly");
                            return RedirectToAction("Index");
                        }
                        string fileName = await product.Photos[i].SaveImageAsync(_env.WebRootPath, "assets/images/product/");
                        string path = Path.Combine(_env.WebRootPath, "assets/images/product/", oldPhoto[i].ImageUrl);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        oldPhoto[i].ImageUrl = fileName;
                        await _context.SaveChangesAsync();

                    }
                }

            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return NotFound();
            return View(dbProduct);
        }


        // Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Product product)
        {
            if (id == null) return NotFound();
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return NotFound();

            List<int> checkColor = _context.ProductColors.Where(c => c.ProductId == dbProduct.Id).Select(c => c.ColorId).ToList();
            List<int> checkTag = _context.ProductTags.Where(p => p.ProductId == dbProduct.Id).Select(t => t.TagId).ToList();

            int chekedAllColor = checkColor.Count();

            for (int i = 1; i <= chekedAllColor; i++)
            {
               ProductColor productColor = await _context.ProductColors.FirstOrDefaultAsync(c => c.ColorId == dbProduct.Id);
               _context.ProductColors.Remove(productColor);
               await _context.SaveChangesAsync();
                
            }


            //foreach (var item in productColors)
            //{
            //    _context.ProductColors.Remove(item);
            //    await _context.SaveChangesAsync();
            //}

            //List<ProductTag> productTags = await _context.ProductTags.ToListAsync();
            //foreach (var item in productTags)
            //{
            //    _context.ProductTags.Remove(item);
            //    await _context.SaveChangesAsync();
            //}

            //List<ProductRelated> productRelateds = await _context.ProductRelateds.ToListAsync();
            //foreach (var item in productRelateds)
            //{
            //    _context.ProductRelateds.Remove(item);
            //    await _context.SaveChangesAsync();
            //}

            var oldPhoto = _context.ProductImages.Where(p => p.ProductId == id).ToList();

            foreach (var item in oldPhoto)
            {
                string path = Path.Combine(_env.WebRootPath, "assets/images/product/", item.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                _context.ProductImages.Remove(item);
                await _context.SaveChangesAsync();
            }

            _context.Products.Remove(dbProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}