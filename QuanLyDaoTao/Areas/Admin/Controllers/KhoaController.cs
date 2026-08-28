using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.DataAccess.Data;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Areas.Admin
{
    [Area("Admin")]
    public class KhoaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public KhoaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var khoas = await _context.khoas
                .AsNoTracking()
                .OrderBy(khoa => khoa.TenKhoa)
                .ToListAsync();
            return View(khoas);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            if (id is null)
            {
                return View(new Khoa());
            }

            var khoa = await _context.khoas.FindAsync(id);
            if (khoa is null)
            {
                return NotFound();
            }

            return View(khoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Khoa khoa)
        {
            if (!ModelState.IsValid)
            {
                return View(khoa);
            }

            var isNewKhoa = khoa.MaKhoa == 0;
            if (isNewKhoa)
            {
                _context.khoas.Add(khoa);
                TempData["Success"] = "Đã thêm khoa mới.";
            }
            else
            {
                var existingKhoa = await _context.khoas.FindAsync(khoa.MaKhoa);
                if (existingKhoa is null)
                {
                    return NotFound();
                }

                existingKhoa.TenKhoa = khoa.TenKhoa;
                existingKhoa.MoTa = khoa.MoTa;
                TempData["Success"] = "Đã cập nhật thông tin khoa.";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #region CALL API
        public async Task<IActionResult> GetAll()
        {
            var khoas = await _context.khoas.AsNoTracking().ToListAsync();
            return Json(new { data = khoas });
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var khoa = await _context.khoas.FindAsync(id);
            if (khoa is null)
            {
                return Json(new { success = false, message = "Không tìm thấy khoa cần xóa." });
            }

            _context.khoas.Remove(khoa);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Đã xóa khoa thành công." });
        }

    }
}
