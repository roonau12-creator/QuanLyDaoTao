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
    public class DiemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDiemService _diemService;

        public DiemController(
            ApplicationDbContext context,
            IDiemService diemService)
        {
            _context = context;
            _diemService = diemService;
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

            var diems = await _context.diems
                .Include(d => d.DangKyHocPhan)
                    .ThenInclude(dk => dk.SinhVien)
                .Include(d => d.DangKyHocPhan)
                    .ThenInclude(dk => dk.HocPhan)
                        .ThenInclude(hp => hp.MonHoc)
                .Include(d => d.DangKyHocPhan)
                    .ThenInclude(dk => dk.HocKy)
                .Where(d => d.DangKyHocPhan.MaSinhVien == maSinhVien)
                .OrderByDescending(d => d.MaDiem)
                .ToListAsync();

            return View(diems);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var maSinhVien = await GetMaSinhVienAsync();
            if (maSinhVien == null) return RedirectToAction("AccessDenied", "Account", new { area = "Identity" });

            var diem = await _diemService.GetDiemByIdAsync(id.Value, includeDangKyHocPhan: true);
            if (diem == null) return NotFound();

            if (diem.DangKyHocPhan?.MaSinhVien != maSinhVien)
                return Forbid();

            return View(diem);
        }

        public async Task<IActionResult> TongKet()
        {
            var maSinhVien = await GetMaSinhVienAsync();
            if (maSinhVien == null) return RedirectToAction("AccessDenied", "Account", new { area = "Identity" });

            var diems = await _context.diems
                .Include(d => d.DangKyHocPhan)
                    .ThenInclude(dk => dk.HocPhan)
                        .ThenInclude(hp => hp.MonHoc)
                .Include(d => d.DangKyHocPhan)
                    .ThenInclude(dk => dk.HocKy)
                .Where(d => d.DangKyHocPhan.MaSinhVien == maSinhVien)
                .ToListAsync();

            var tongTinChi = diems
                .Where(d => d.DiemTongKet >= 5.0m)
                .Sum(d => d.DangKyHocPhan?.HocPhan?.SoTinChi ?? 0);

            var diemTB = diems
                .Where(d => d.DiemTongKet.HasValue)
                .Any()
                ? diems
                    .Where(d => d.DiemTongKet.HasValue)
                    .Average(d => d.DiemTongKet!.Value)
                : 0;

            ViewBag.TongTinChi = tongTinChi;
            ViewBag.DiemTrungBinh = Math.Round(diemTB, 2);
            ViewBag.SoMonRot = diems.Count(d => d.DiemTongKet.HasValue && d.DiemTongKet < 4.0m);
            ViewBag.SoMonDat = diems.Count(d => d.DiemTongKet.HasValue && d.DiemTongKet >= 5.0m);

            return View(diems);
        }

        public async Task<IActionResult> KetQuaHocTap()
        {
            var maSinhVien = await GetMaSinhVienAsync();
            if (maSinhVien == null) return RedirectToAction("AccessDenied", "Account", new { area = "Identity" });

            var sinhVien = await _context.sinhViens
                .Include(s => s.Lop)
                    .ThenInclude(l => l!.Khoa)
                .FirstOrDefaultAsync(s => s.MaSinhVien == maSinhVien);

            var diems = await _context.diems
                .Include(d => d.DangKyHocPhan)
                    .ThenInclude(dk => dk.HocPhan)
                        .ThenInclude(hp => hp.MonHoc)
                .Include(d => d.DangKyHocPhan)
                    .ThenInclude(dk => dk.HocKy)
                .Where(d => d.DangKyHocPhan.MaSinhVien == maSinhVien)
                .ToListAsync();

            ViewBag.SinhVien = sinhVien;

            return View(diems);
        }
    }
}
