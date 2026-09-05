using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Utility;

namespace QuanLyDaoTao.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Role_Student)]
    public class TaiKhoanController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TaiKhoanController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoiMatKhau(string matKhauCu, string matKhauMoi, string xacNhanMatKhau)
        {
            if (string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(xacNhanMatKhau))
            {
                TempData["error"] = "Vui lòng nhập đầy đủ thông tin.";
                return View();
            }

            if (matKhauMoi != xacNhanMatKhau)
            {
                TempData["error"] = "Mật khẩu xác nhận không khớp.";
                return View();
            }

            if (matKhauMoi.Length < 6)
            {
                TempData["error"] = "Mật khẩu mới phải có ít nhất 6 ký tự.";
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account", new { area = "Identity" });

            var result = await _userManager.ChangePasswordAsync(user, matKhauCu, matKhauMoi);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["success"] = "Đổi mật khẩu thành công.";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Mật khẩu hiện tại không đúng.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangXuat()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }
    }
}
