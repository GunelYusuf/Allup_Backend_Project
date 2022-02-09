using System.Linq;
using System.Threading.Tasks;
using Allup_Backend.DAL;
using Allup_Backend.Models;
using Allup_Backend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Allup_Backend.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Context _context;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<IActionResult> Index(string name)
        {

            var users = name == null ? _userManager.Users.ToList() :
             _userManager.Users.Where(u => u.FullName.ToLower().Contains(name.ToLower())).ToList();
            //List<UserReturnVM> userVMs = new List<UserReturnVM>();
            //foreach (var user in users)
            //{
            //    UserReturnVM userVM = new UserVM();
            //    userReturnVM.FullName = user.FullName;
            //    userReturnVM.UserName = user.UserName;
            //    userReturnVM.Email = user.Email;
            //    userReturnVM.Role = (await _userManager.GetRolesAsync(user))[0];
            //    userReturnVMs.Add(userVM);
            //}
            return View(users);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserRoleVM userRoleVM = new UserRoleVM();
            userRoleVM.AppUser = user;
            //userRoleVM.Roles= _roleManager.Roles.ToList();
            userRoleVM.Roles = await _userManager.GetRolesAsync(user);
            return View(userRoleVM);
        }

        public IActionResult CreateUser()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateUser(RegisterVM user)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = new AppUser
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,

            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, user.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(appUser, true);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateUser(string id)
        {
            if (id == null) return NotFound();
            var user = _userManager.FindByIdAsync(id);
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> UpdateUser(AppUser appUser)
        {
            AppUser user = await _userManager.FindByIdAsync(appUser.Id);

            user.FullName = appUser.FullName;
            user.UserName = appUser.UserName;
            user.PasswordHash = appUser.PasswordHash;
            user.Email = appUser.Email;
            await _userManager.UpdateAsync(user);
            return View();

        }


        public async Task<IActionResult> IsActive(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.IsActive)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}