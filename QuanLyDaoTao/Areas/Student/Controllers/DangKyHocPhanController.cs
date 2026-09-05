using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;
using QuanLyDaoTao.Utility;

namespace QuanLyDaoTao.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Role_Student)]
    public class DangKyHocPhanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDangKyHocPhanService _dangKyHocPhanService;

        public DangKyHocPhanController(
            ApplicationDbContext context,
            IDangKyHocPhanService dangKyHocPhanService)
        {
            _context = context;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var dangKy = await _dangKyHocPhanService.GetDangKyHocPhanByIdAsync(
                id.Value, includeSinhVien: true, includeHocPhan: true, includeHocKy: true);

            if (dangKy == null) return NotFound();

            return View(dangKy);
        }

        [HttpGet]
        public async Task<IActionResult> DangKy(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var hocPhan = await _context.hocPhans
                .Include(x => x.MonHoc)
                .Include(x => x.HocKy)
                .FirstOrDefaultAsync(x => x.MaHocPhan == id.Value);

            if (hocPhan == null) return NotFound();

            var viewModel = new DangKyHocPhanVM
            {
                MaHocPhan = hocPhan.MaHocPhan,
                TenHocPhan = hocPhan.TenHocPhan,
                TenMonHoc = hocPhan.MonHoc.TenMonHoc,
                SoTinChi = hocPhan.SoTinChi,
                MaHocKy = hocPhan.MaHocKy,
                TenHocKy = hocPhan.HocKy.TenHocKy,
                SiSoToiDa = hocPhan.SiSoToiDa,
                SoLuongDaDangKy = await _context.dangKyHocPhans
                    .CountAsync(x => x.MaHocPhan == hocPhan.MaHocPhan)
            };

            viewModel.SoChoConLai = viewModel.SiSoToiDa - viewModel.SoLuongDaDangKy;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy(DangKyHocPhanVM viewModel)
        {
            if (viewModel.MaHocPhan == 0) return NotFound();

            var maSinhVien = await GetMaSinhVienAsync();
            if (maSinhVien == null) return RedirectToAction("AccessDenied", "Account", new { area = "Identity" });

            var hocPhan = await _context.hocPhans
                .FirstOrDefaultAsync(x => x.MaHocPhan == viewModel.MaHocPhan);

            if (hocPhan == null) return NotFound();

            if (hocPhan.TrangThai != "Đang mở")
            {
                TempData["error"] = "Học phần hiện không mở đăng ký.";
                return RedirectToAction("Index");
            }

            var daDangKy = await _context.dangKyHocPhans
                .AnyAsync(x =>
                    x.MaSinhVien == maSinhVien &&
                    x.MaHocPhan == viewModel.MaHocPhan &&
                    x.MaHocKy == hocPhan.MaHocKy);

            if (daDangKy)
            {
                TempData["error"] = "Bạn đã đăng ký học phần này.";
                return RedirectToAction("Index");
            }

            var soLuongDaDangKy = await _context.dangKyHocPhans
                .CountAsync(x => x.MaHocPhan == viewModel.MaHocPhan);

            if (soLuongDaDangKy >= hocPhan.SiSoToiDa)
            {
                TempData["error"] = "Học phần đã đủ sĩ số.";
                return RedirectToAction("Index");
            }

            var dangKy = new DangKyHocPhan
            {
                MaSinhVien = maSinhVien.Value,
                MaHocPhan = viewModel.MaHocPhan,
                MaHocKy = hocPhan.MaHocKy,
                NgayDangKy = DateTime.UtcNow,
                TrangThai = "Đã đăng ký",
                GhiChu = null
            };

            await _context.dangKyHocPhans.AddAsync(dangKy);
            await _context.SaveChangesAsync();

            TempData["success"] = "Đăng ký học phần thành công.";
            return RedirectToAction("DaDangKy");
        }

        public async Task<IActionResult> DaDangKy()
        {
            var maSinhVien = await GetMaSinhVienAsync();
            if (maSinhVien == null) return RedirectToAction("AccessDenied", "Account", new { area = "Identity" });

            var danhSach = await _context.dangKyHocPhans
                .Include(x => x.HocPhan)
                    .ThenInclude(x => x.MonHoc)
                .Include(x => x.HocKy)
                .Where(x => x.MaSinhVien == maSinhVien)
                .OrderByDescending(x => x.MaDangKy)
                .ToListAsync();

            return View(danhSach);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetHocPhanMoDangKy()
        {
            var danhSach = await _context.hocPhans
                .Include(x => x.MonHoc)
                .Include(x => x.HocKy)
                .Include(x => x.GiangVien)
                .Where(x => x.TrangThai == "Đang mở")
                .OrderByDescending(x => x.MaHocPhan)
                .ToListAsync();
            return Json(new { data = danhSach });
        }

        [HttpDelete]
        public async Task<IActionResult> HuyDangKy(int? id)
        {
            if (id == null || id == 0)
                return Json(new { success = false, message = "Mã đăng ký không hợp lệ." });

            var maSinhVien = await GetMaSinhVienAsync();
            if (maSinhVien == null)
                return Json(new { success = false, message = "Không xác định được sinh viên." });

            var dangKy = await _context.dangKyHocPhans
                .FirstOrDefaultAsync(x => x.MaDangKy == id.Value && x.MaSinhVien == maSinhVien);

            if (dangKy == null)
                return Json(new { success = false, message = "Không tìm thấy đăng ký." });

            if (dangKy.TrangThai != "Đã đăng ký")
                return Json(new { success = false, message = "Không thể hủy đăng ký này." });

            _context.dangKyHocPhans.Remove(dangKy);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Hủy đăng ký học phần thành công." });
        }
        #endregion
    }
}
