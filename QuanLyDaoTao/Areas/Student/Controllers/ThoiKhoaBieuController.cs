using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Utility;

namespace QuanLyDaoTao.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Role_Student)]
    public class ThoiKhoaBieuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILichHocService _lichHocService;

        public ThoiKhoaBieuController(
            ApplicationDbContext context,
            ILichHocService lichHocService)
        {
            _context = context;
            _lichHocService = lichHocService;
        }

        private async Task<int?> GetMaSinhVienAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return null;
            var user = await _context.Users.FindAsync(userId);
            if (user is Models.ApplicationUser appUser)
                return appUser.MaSinhVien;
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var maSinhVien = await GetMaSinhVienAsync();
            if (maSinhVien == null) return RedirectToAction("AccessDenied", "Account", new { area = "Identity" });

            var lichHocs = await _lichHocService.GetLichHocBySinhVienAsync(maSinhVien.Value);

            return View(lichHocs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var lichHoc = await _lichHocService.GetLichHocByIdAsync(id.Value, includeMonHoc: true, includeLop: true);
            if (lichHoc == null) return NotFound();

            return View(lichHoc);
        }
    }
}
