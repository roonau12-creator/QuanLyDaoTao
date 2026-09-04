using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;
namespace QuanLyDaoTao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DangKyHocPhanController : Controller
    {
        private readonly IDangKyHocPhanService _dangKyHocPhanService;
        private readonly ApplicationDbContext _context;

        public DangKyHocPhanController(IDangKyHocPhanService dangKyHocPhanService, ApplicationDbContext context)
        {
            _dangKyHocPhanService = dangKyHocPhanService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var dangKy = await _dangKyHocPhanService.GetDangKyHocPhanByIdAsync(id.Value, includeSinhVien: true, includeHocPhan: true, includeHocKy: true);
            if (dangKy == null)
            {
                return NotFound();
            }

            return View(dangKy);
        }
        [HttpGet]
        public async Task<IActionResult> DangKy(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var hocPhan = await _context.hocPhans
                .Include(x => x.MonHoc)
                .Include(x => x.HocKy)
                .FirstOrDefaultAsync(x => x.MaHocPhan == id.Value);

            if (hocPhan == null)
            {
                return NotFound();
            }

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

            viewModel.SoChoConLai =
                viewModel.SiSoToiDa - viewModel.SoLuongDaDangKy;

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy(DangKyHocPhanVM viewModel)
        {
            if (viewModel.MaHocPhan == 0)
            {
                return NotFound();
            }

            // TODO: Lấy mã sinh viên từ tài khoản đăng nhập
            int maSinhVien = 1;

            var hocPhan = await _context.hocPhans
                .FirstOrDefaultAsync(x => x.MaHocPhan == viewModel.MaHocPhan);

            if (hocPhan == null)
            {
                return NotFound();
            }

            // Kiểm tra học phần có đang mở không
            if (hocPhan.TrangThai != "Đang mở")
            {
                TempData["error"] = "Học phần hiện không mở đăng ký.";

                return RedirectToAction("Index");
            }

            // Kiểm tra đã đăng ký chưa
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

            // Kiểm tra sĩ số
            var soLuongDaDangKy = await _context.dangKyHocPhans
                .CountAsync(x => x.MaHocPhan == viewModel.MaHocPhan);

            if (soLuongDaDangKy >= hocPhan.SiSoToiDa)
            {
                TempData["error"] = "Học phần đã đủ sĩ số.";

                return RedirectToAction("Index");
            }

            // Tạo đăng ký
            DangKyHocPhan dangKy = new DangKyHocPhan
            {
                MaSinhVien = maSinhVien,
                MaHocPhan = viewModel.MaHocPhan,
                MaHocKy = hocPhan.MaHocKy,
                NgayDangKy = DateTime.UtcNow,
                TrangThai = "Đã đăng ký",
                GhiChu = null
            };

            await _context.dangKyHocPhans.AddAsync(dangKy);

            await _context.SaveChangesAsync();

            TempData["success"] = "Đăng ký học phần thành công.";

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DaDangKy()
        {
            // Tạm thời dùng mã sinh viên để test
            int maSinhVien = 1;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _dangKyHocPhanService.GetAllDangKyHocPhansAsync(
                includeSinhVien: true,
                includeHocPhan: true,
                includeHocKy: true);
            return Json(new { data = allObj });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _dangKyHocPhanService.GetDangKyHocPhanByIdAsync(
                id,
                includeSinhVien: true,
                includeHocPhan: true,
                includeHocKy: true);
            if (obj == null)
            {
                return Json(new { success = false, message = "Không tìm thấy đăng ký." });
            }
            return Json(new { success = true, data = obj });
        }

        [HttpDelete]
        public async Task<IActionResult> HuyDangKy(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new
                {
                    success = false,
                    message = "Mã đăng ký không hợp lệ."
                });
            }

            var dangKy = await _context.dangKyHocPhans
                .FirstOrDefaultAsync(x => x.MaDangKy == id.Value);

            if (dangKy == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Không tìm thấy đăng ký."
                });
            }

            // Chỉ cho phép hủy khi đang ở trạng thái Đã đăng ký
            if (dangKy.TrangThai != "Đã đăng ký")
            {
                return Json(new
                {
                    success = false,
                    message = "Không thể hủy đăng ký này."
                });
            }

            _context.dangKyHocPhans.Remove(dangKy);

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "Hủy đăng ký học phần thành công."
            });
        }
        #endregion
    }
}
