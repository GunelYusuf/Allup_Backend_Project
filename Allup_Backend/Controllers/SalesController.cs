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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class SalesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly Context _context;
        private readonly IConfiguration _config;
        public SalesController(Context context, UserManager<AppUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            _config = config;
        }


        // GET: SalesController
        public async Task<IActionResult> Index()
        {

            string basketCookie = Request.Cookies["basketCookie"];

            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "home");

       
            string UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            List<BasketProduct> basketProductList = new List<BasketProduct>();

            if (basketCookie != null)
            {

                basketProductList = JsonConvert.DeserializeObject<List<BasketProduct>>(basketCookie);

                
                var IsExsist = basketProductList.FirstOrDefault(x => x.UserId == UserID);
                if (IsExsist == null) return RedirectToAction("Index", "home");

                foreach (var item in basketProductList)
                {

                    Product product = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                    item.Price = product.Price;
                    item.Name = product.Name;
                }
                Response.Cookies.Append("basketCookie", JsonConvert.SerializeObject(basketProductList), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });
               
            }

            ViewBag.User = await _userManager.FindByIdAsync(UserID);

            return View(basketProductList.Where(x => x.UserId == UserID).ToList());
        }


        [HttpPost]
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
                basketProducts.Remove(basketProduct);
            }
            Response.Cookies.Append("basketCookie", JsonConvert.SerializeObject(basketProducts), new CookieOptions { MaxAge = TimeSpan.FromMinutes(14) });


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
            product.Quantity = product.Quantity - basketProduct.Count;
            await _context.SaveChangesAsync();
        }


        
    }
}
