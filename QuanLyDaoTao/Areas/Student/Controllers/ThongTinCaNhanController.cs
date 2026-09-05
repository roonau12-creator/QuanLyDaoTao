using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Utility;

namespace QuanLyDaoTao.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Role_Student)]
    public class ThongTinCaNhanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThongTinCaNhanController(ApplicationDbContext context)
        {
            _context = context;
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

            var sinhVien = await _context.sinhViens
                .Include(s => s.Lop)
                    .ThenInclude(l => l!.Khoa)
                .FirstOrDefaultAsync(s => s.MaSinhVien == maSinhVien);

            if (sinhVien == null) return NotFound();

            return View(sinhVien);
        }
    }
}
