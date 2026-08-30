using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace QuanLyDaoTao.Areas.Admin
{
    [Area("Admin")]
    public class LopController : Controller
    {
        private readonly ILopService _lopService;
        private readonly IKhoaService _khoaService;

        public LopController(ILopService lopService, IKhoaService khoaService)
        {
            _lopService = lopService;
            _khoaService = khoaService;
        }

        public async Task<IActionResult> Index()
        {
            var lops = await _lopService.GetAllLopAsync(includeKhoa: true);
            return View(lops);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var khoa = await _khoaService.GetAllKhoasAsync();
            LopVM lopVM = new()
            {
                KhoaList = khoa.Select(c=>new SelectListItem
                {
                    Text = c.TenKhoa,
                    Value=c.MaKhoa.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(lopVM);
            }

            var lop = await _lopService.GetLopByIdAsync(id.Value);
            if (lop == null)
            {
                return NotFound();
            }

            lopVM.Lop = lop;
            return View(lopVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(LopVM lopvm)
    {
        if (ModelState.IsValid)
        {
            if (lopvm.Lop.MaLop == 0)
            {
                await _lopService.CreateLopAsync(lopvm.Lop);
            }
            else
            {
            await _lopService.UpdateLopAsync(lopvm.Lop);
            }

return RedirectToAction("Index");
}

var khoa = await _khoaService.GetAllKhoasAsync();

lopvm.KhoaList = khoa.Select(c => new SelectListItem
{
Text = c.TenKhoa,
Value = c.MaKhoa.ToString()
});

return View(lopvm);
    }

        #region CALL API
        public async Task<IActionResult> GetAll()
        {
            var lops = await _lopService.GetAllLopAsync(includeKhoa: true);
            return Json(new { data = lops });
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Mã lớp không hợp lệ." });
            }

            var lop = await _lopService.GetLopByIdAsync(id);
            if (lop == null)
            {
                return Json(new { success = false, message = "Không tìm thấy lớp cần xóa." });
            }

            var sinhVienCount = await _lopService.GetRelatedCountsAsync(id);
            if (sinhVienCount > 0)
            {
                return Json(new
                {
                    success = false,
                    message = $"Không thể xóa lớp \"{lop.TenLop}\" vì còn {sinhVienCount} sinh viên đang thuộc lớp này. Vui lòng xóa các sinh viên liên quan trước."
                });
            }

            try
            {
                await _lopService.DeleteLopAsync(id);
                return Json(new { success = true, message = $"Đã xóa lớp \"{lop.TenLop}\" thành công." });
            }
            catch (DbUpdateException dbEx)
            {
                return Json(new
                {
                    success = false,
                    message = $"Không thể xóa lớp do lỗi cơ sở dữ liệu: {dbEx.InnerException?.Message ?? dbEx.Message}"
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
                    message = $"Lỗi khi xóa lớp: {ex.Message}"
                });
            }
        }
    }
}
