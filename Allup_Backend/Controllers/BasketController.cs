using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Allup_Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class BasketController : Controller
    {

        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> AddBasket(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("LogIn", "Account");
            }
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (id == null) return RedirectToAction("Index", "Error");
            Product product = await _context.Products.Include(p => p.Campaign)
                 .Include(p => p.Brand)
                 .Include(p => p.Images)
                 .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return RedirectToAction("Index", "Error");
            string basketCookie = Request.Cookies["basketCookie"];
            List<BasketProduct> basketProductList;
            if (basketCookie == null)
            {
                basketProductList = new List<BasketProduct>();
            }
            else
            {
                basketProductList = JsonConvert.DeserializeObject<List<BasketProduct>>(basketCookie);
            }

            BasketProduct isExistProduct = basketProductList.FirstOrDefault(p => p.Id == product.Id);

            if (isExistProduct == null)
            {
                BasketProduct basketProduct = new BasketProduct
                {
                    Name = product.Name,
                    Id=product.Id,
                    Discount = product.Campaign.Discount,
                    ImageUrl =product.Images[0].ImageUrl,
                    Price = product.Price,
                    Count = 1,
                    BrandId = product.BrandId,
                    UserId = userId
                };
                basketProductList.Add(basketProduct);
            }
            else
            {
                isExistProduct.Count++;
            }

            Response.Cookies.Append("basketCookie", JsonConvert.SerializeObject(basketProductList), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowBasket()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            string basketCookie = Request.Cookies["basketCookie"];
            List<BasketProduct> basketProductList = new List<BasketProduct>();
            if (basketCookie != null)
            {
                basketProductList = JsonConvert.DeserializeObject<List<BasketProduct>>(basketCookie);
                foreach (var item in basketProductList)
                {
                    Product product = _context.Products.Include(p => p.ProductColors)
                        .Include(p => p.Campaign)
                        .Include(p => p.Brand)
                        .Include(p => p.Images).FirstOrDefault(p => p.Id == item.Id);
                    item.Name = product.Name;
                    item.ImageUrl = product.Images[0].ImageUrl;
                    item.Price = product.Price;
                    item.Discount = product.Campaign.Discount;

                }
                Response.Cookies.Append("basketCookie", JsonConvert.SerializeObject(basketProductList), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });
            }
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.UserID = userId;
            return View(basketProductList);
        }

        public IActionResult Remove(int? id)
        {
            if (id == null) RedirectToAction("Index", "Error");
            Product product = _context.Products.Find(id);
            string basketCookie = Request.Cookies["basketCookie"];
            List<BasketProduct> basketProductList = JsonConvert.DeserializeObject<List<BasketProduct>>(basketCookie);
            BasketProduct isExistProduct = basketProductList.FirstOrDefault(p => p.Id == product.Id);
            basketProductList.Remove(isExistProduct);
            Response.Cookies.Append("basketCookie", JsonConvert.SerializeObject(basketProductList), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });
            return View(basketProductList);
        }

        public async Task<IActionResult> Sale()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("LogIn", "Account");

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            Sales sales = new Sales();
            sales.AppUserId = user.Id;
            sales.SaleDate = DateTime.Now;

            List<BasketProduct> basketProducts = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies["basketCookie"]);
            List<ProductSales> productSalesList = new List<ProductSales>();


            List<Product> dbProducts = new List<Product>();
            foreach (var item in basketProducts)
            {
                Product dbProduct = await _context.Products.FindAsync(item.Id);
                if (dbProduct.Count < item.Count)
                {
                    TempData["Failed"] = $"{item.Name} is not in the database";
                    return RedirectToAction("ShowBaket", "Basket");
                }
                dbProducts.Add(dbProduct);

            }

            double total = 0;
            foreach (var basketProduct in basketProducts)
            {
                Product dbProduct = dbProducts.Find(p => p.Id == basketProduct.Id);

                await UpdateProductCount(dbProduct, basketProduct);

                ProductSales productSales = new ProductSales();
                productSales.SalesId = sales.Id;
                productSales.ProductId = dbProduct.Id;

                productSalesList.Add(productSales);
                total += basketProduct.Count * basketProduct.Price;
            }

            sales.ProductSales = productSalesList;
            sales.Total = total;
            await _context.Sales.AddAsync(sales);
            await _context.SaveChangesAsync();
            TempData["Success"] = "The sale was completed successfully";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("guntebrustemov@gmail.com", "Succes Sale!");
                mail.To.Add(user.Email);
                mail.Subject = "Successfully";
                mail.Body = "The sale was completed successfully!";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("guntebrustemov", "gunteb7@");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                }
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task UpdateProductCount(Product product, BasketProduct basketProduct)
        {
            product.Count = product.Count - basketProduct.Count;
            await _context.SaveChangesAsync();
        }

        public IActionResult BasketCount([FromForm] int id, string change)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string basketCookie = Request.Cookies["basketCookie"];

            List<BasketProduct> basketProducts = new List<BasketProduct>();

            List<BasketProduct> basketProductList = JsonConvert.DeserializeObject<List<BasketProduct>>(basketCookie);
            Product product = _context.Products.Find(id);
            var totalcount = 0;
            foreach (var basketProduct in basketProducts)
            {
                if (basketProduct.Id == id && basketProduct.UserId == UserId)
                {
                    if (change == "sub" && (basketProduct.Count) > 1)
                    {
                        basketProduct.Count--;
                        totalcount += basketProduct.Count;

                    }
                    if (change == "add" && basketProduct.Count != product.Quantity)
                    {
                        basketProduct.Count++;
                        totalcount += basketProduct.Count;
                    }
                    if (totalcount != 0) basketProduct.Count = totalcount;
                }

            }
            Response.Cookies.Append("basketCookie", JsonConvert.SerializeObject(basketProducts), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });
            
            if (totalcount != 0)
            {
                return Ok(totalcount);
            }
            return Ok("error message");

        }
       

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
