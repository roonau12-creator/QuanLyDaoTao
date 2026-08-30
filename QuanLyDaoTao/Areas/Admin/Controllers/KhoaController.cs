using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;

namespace QuanLySinhVien.Areas.Admin
{
    [Area("Admin")]
    public class KhoaController : Controller
    {
        private readonly IKhoaService _context;
        public KhoaController(IKhoaService context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var khoas = await _context.GetAllKhoasAsync();
            return View(khoas);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Khoa());
            }

            var khoa = await _context.GetKhoaByIdAsync(id.Value);

            if (khoa == null)
            {
                return NotFound();
            }

            return View(khoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                if (khoa.MaKhoa == 0)
                {
                    await _context.CreateKhoaAsync(khoa);
                }
                else
                {
                    await _context.UpdateKhoaAsync(khoa);
                }

                return RedirectToAction("Index");
            }

            return View(khoa);
        }

        #region CALL API
        public async Task<IActionResult> GetAll()
        {
            var khoas = await _context.GetAllKhoasAsync();
            return Json(new { data = khoas });
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Mã khoa không hợp lệ." });
            }

            var khoa = await _context.GetKhoaByIdAsync(id);
            if (khoa == null)
            {
                return Json(new { success = false, message = "Không tìm thấy khoa cần xóa." });
            }

            var (lopCount, monHocCount) = await _context.GetRelatedCountsAsync(id);
            if (lopCount > 0 || monHocCount > 0)
            {
                var parts = new List<string>();
                if (lopCount > 0) parts.Add($"{lopCount} lớp");
                if (monHocCount > 0) parts.Add($"{monHocCount} môn học");
                return Json(new
                {
                    success = false,
                    message = $"Không thể xóa khoa \"{khoa.TenKhoa}\" vì còn {string.Join(" và ", parts)} đang tham chiếu đến khoa này. Vui lòng xóa các bản ghi liên quan trước."
                });
            }

            try
            {
                await _context.DeleteKhoaAsync(id);
                return Json(new { success = true, message = $"Đã xóa khoa \"{khoa.TenKhoa}\" thành công." });
            }
            catch (DbUpdateException dbEx)
            {
                return Json(new
                {
                    success = false,
                    message = $"Không thể xóa khoa do lỗi cơ sở dữ liệu: {dbEx.InnerException?.Message ?? dbEx.Message}"
                });
            }
            catch (KeyNotFoundException knfEx)
            {
                return Json(new { success = false, message = knfEx.Message });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = $"Lỗi khi xóa khoa: {ex.Message}"
                });
            }
        }
    }
}
