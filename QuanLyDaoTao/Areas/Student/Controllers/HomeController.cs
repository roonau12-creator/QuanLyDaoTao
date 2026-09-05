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
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IThongBaoService _thongBaoService;
        private readonly IDangKyHocPhanService _dangKyHocPhanService;

        public HomeController(
            ApplicationDbContext context,
            IThongBaoService thongBaoService,
            IDangKyHocPhanService dangKyHocPhanService)
        {
            _context = context;
            _thongBaoService = thongBaoService;
            _dangKyHocPhanService = dangKyHocPhanService;
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

            var soHocPhanDaDangKy = await _context.dangKyHocPhans
                .CountAsync(d => d.MaSinhVien == maSinhVien && d.TrangThai == "Đã đăng ký");

            var soThongBao = await _context.thongBaos.CountAsync();

            var thongBaoMoiNhat = await _context.thongBaos
                .OrderByDescending(t => t.NgayDang)
                .Take(3)
                .ToListAsync();

            ViewBag.SinhVien = sinhVien;
            ViewBag.SoHocPhanDaDangKy = soHocPhanDaDangKy;
            ViewBag.SoThongBao = soThongBao;
            ViewBag.ThongBaoMoiNhat = thongBaoMoiNhat;

            return View();
        }
    }
}
