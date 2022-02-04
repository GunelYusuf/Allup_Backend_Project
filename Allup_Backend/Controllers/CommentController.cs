using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Allup_Backend.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly Context _context;

        public CommentController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<ActionResult> Create(CommentProduct commentProducts)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var dataComment = new CommentProduct();
                    dataComment.ProductId = commentProducts.ProductId;
                    dataComment.UserId = commentProducts.UserId;
                    dataComment.Text = commentProducts.Text;
                    await _context.CommentProducts.AddAsync(dataComment);
                    _context.SaveChanges();

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Detail", "Product", new { id = commentProducts.ProductId });

        }

        [HttpGet]

        public async Task<ActionResult> Delete(int? id)
        {
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                RedirectToAction("LogIn", "Account");
            }

            if (id == null) return RedirectToAction("Index");

            CommentProduct comment = _context.CommentProducts.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            else
            {
                if (comment.UserId == userId)
                {
                    _context.CommentProducts.Remove(comment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Detail", "Product", new { id = comment.ProductId });
                }
            }
            return RedirectToAction("Index");

        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Update(int? id, CommentProduct comment)
        {
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                RedirectToAction("LogIn", "Account");
            }

            if (id == null) return RedirectToAction("Index");

            CommentProduct comments = _context.CommentProducts.Find(id);
            if (comments == null)
            {
                return NotFound();
            }
            else
            {
                if (comments.UserId == userId)
                {
                    comments.Text = comment.Text;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Detail", "Product", new { id = comments.ProductId });
                }

            }
            return RedirectToAction("Index");
        }

        //Blog's comments

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<ActionResult> Create(CommentBlog commentBlogs)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var text = new CommentBlog();
                    text.BlogId = commentBlogs.BlogId;
                    text.UserId = commentBlogs.UserId;
                    text.Text = commentBlogs.Text;
                    await _context.CommentBlogs.AddAsync(text);
                    _context.SaveChanges();

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Detail", "Blog", new { id = commentBlogs.BlogId });

        }

        [HttpGet]
        [ActionName("Delete")]

        public async Task<ActionResult> DeleteBlogComment(int? id)
        {
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                RedirectToAction("LogIn", "Account");
            }

            if (id == null) return RedirectToAction("Index");

            CommentBlog comment = _context.CommentBlogs.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            else
            {
                if (comment.UserId == userId)
                {
                    _context.CommentBlogs.Remove(comment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Detail", "Blog", new { id = comment.BlogId });
                }
            }
            return RedirectToAction("Index");

        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Update(int? id, CommentBlog comment)
        {
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                RedirectToAction("LogIn", "Account");
            }

            if (id == null) return RedirectToAction("Index");

            CommentBlog comments = _context.CommentBlogs.Find(id);
            if (comments == null)
            {
                return NotFound();
            }
            else
            {
                if (comments.UserId == userId)
                {
                    comments.Text = comment.Text;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Detail", "Blog", new { id = comments.BlogId });
                }

            }
            return RedirectToAction("Index");
        }
    }
}
