using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;
using QuanLyDaoTao.Utility;
using static QuanLyDaoTao.Utility.SD;

namespace QuanLyDaoTao.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly SignInManager<ApplicationUser> _signInManager;
      private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        private static List<SelectListItem> GetRoleList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = SD.Role_Admin, Text = SD.Role_Admin },
                new SelectListItem { Value = SD.Role_Student, Text = SD.Role_Student },
                new SelectListItem { Value = SD.Role_Teacher, Text = SD.Role_Teacher }
            };
        }

        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginVM
            {
                RoleList = GetRoleList()
            };

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            model.RoleList ??= GetRoleList();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.MaSoSinhVien);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Mã số sinh viên hoặc mật khẩu không đúng.");
            }
            model.RoleList = GetRoleList();
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}