using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.Models;
namespace QuanLyDaoTao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HocPhanController : Controller
    {
        private readonly IHocPhanService _hocPhanService;
        private readonly IMonHocService _monHocService;
        private readonly IHocKyService _hocKyService;
        private readonly IGiangVienService _giangVienService;

        public HocPhanController(
            IHocPhanService hocPhanService,
            IMonHocService monHocService,
            IHocKyService hocKyService,
            IGiangVienService giangVienService)
        {
            _hocPhanService = hocPhanService;
            _monHocService = monHocService;
            _hocKyService = hocKyService;
            _giangVienService = giangVienService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            ViewBag.MonHocs = await BuildMonHocDropdown();
            ViewBag.HocKys = await BuildHocKyDropdown();
            ViewBag.GiangViens = await BuildGiangVienDropdown();

            if (id == null || id == 0)
            {
                return View(new HocPhan());
            }

            var hocPhan = await _hocPhanService.GetHocPhanByIdAsync(id.Value, includeMonHoc: true, includeGiangVien: true, includeHocKy: true);

            if (hocPhan == null)
            {
                return NotFound();
            }

            return View(hocPhan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(HocPhan hocPhan)
        {
            if (ModelState.IsValid)
            {
                if (hocPhan.MaHocPhan == 0)
                {
                    await _hocPhanService.CreateHocPhanAsync(hocPhan);

                    TempData["success"] = "Thêm học phần thành công.";
                }
                else
                {
                    await _hocPhanService.UpdateHocPhanAsync(hocPhan);

                    TempData["success"] = "Cập nhật học phần thành công.";
                }

                return RedirectToAction("Index");
            }

            ViewBag.MonHocs = await BuildMonHocDropdown();
            ViewBag.HocKys = await BuildHocKyDropdown();
            ViewBag.GiangViens = await BuildGiangVienDropdown();

            return View(hocPhan);
        }

        #region  API CALLS
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _hocPhanService.GetAllHocPhansAsync(includeMonHoc: true, includeGiangVien: true, includeHocKy: true);
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new
                {
                    success = false,
                    message = "Mã học phần không hợp lệ."
                });
            }

            await _hocPhanService.DeleteHocPhanAsync(id.Value);

            return Json(new
            {
                success = true,
                message = "Xóa học phần thành công."
            });
        }
        #endregion

        private async Task<IEnumerable<SelectListItem>> BuildMonHocDropdown()
        {
            return (await _monHocService.GetAllMonHocAsync()).Select(x => new SelectListItem
            {
                Text = x.TenMonHoc,
                Value = x.MaMonHoc.ToString()
            });
        }

        private async Task<IEnumerable<SelectListItem>> BuildHocKyDropdown()
        {
            return (await _hocKyService.GetAllHocKyAsync()).Select(x => new SelectListItem
            {
                Text = x.TenHocKy,
                Value = x.MaHocKy.ToString()
            });
        }

        private async Task<IEnumerable<SelectListItem>> BuildGiangVienDropdown()
        {
            return (await _giangVienService.GetAllGiangVienAsync()).Select(x => new SelectListItem
            {
                Text = x.HoTen,
                Value = x.MaGiangVien.ToString()
            });
        }
    }
}
