using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.Models;

namespace QuanLySinhVien.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiemController : Controller
    {
        private readonly IDiemService _diemService;
        private readonly IDangKyHocPhanService _dangKyHocPhanService;

        public DiemController(IDiemService diemService, IDangKyHocPhanService dangKyHocPhanService)
        {
            _diemService = diemService;
            _dangKyHocPhanService = dangKyHocPhanService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> TinhDiem(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var diem = await _diemService.GetDiemByIdAsync(id.Value, true);

            if (diem == null)
            {
                return NotFound();
            }

            return View(diem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TinhDiem(int id)
        {
            try
            {
                var diem = await _diemService
                    .TinhDiemTongKetAsync(id);

                TempData["success"] =
                    $"Đã tính điểm tổng kết: {diem.DiemTongKet} - {diem.KetQua}";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                var dangKy = await _dangKyHocPhanService.GetAllDangKyHocPhansAsync(true, true, false);
                ViewBag.DangKyHocPhans = dangKy;
                return View(new Diem());
            }

            var diem = await _diemService.GetDiemByIdAsync(
                id.Value,
                true);

            if (diem == null)
            {
                return NotFound();
            }

            var danhSachDangKy = await _dangKyHocPhanService.GetAllDangKyHocPhansAsync(true, true, false);

            ViewBag.DangKyHocPhans = danhSachDangKy;

            return View(diem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Diem diem)
        {
            if (!ModelState.IsValid)
            {
                var dangKy = await _dangKyHocPhanService.GetAllDangKyHocPhansAsync(true, true, false);

                ViewBag.DangKyHocPhans = dangKy;

                return View(diem);
            }

            try
            {
                if (diem.MaDiem == 0)
                {
                    await _diemService.CreateDiemAsync(diem);

                    TempData["success"] =
                        "Thêm điểm thành công.";
                }
                else
                {
                    await _diemService.UpdateDiemAsync(diem);

                    TempData["success"] =
                        "Cập nhật điểm thành công.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;

                return View(diem);
            }
        }
        #region CALL API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diem = await _diemService.GetAllDiemAsync(true);

            return Json(new
            {
                data = diem
            });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new
                {
                    success = false,
                    message = "Mã điểm không hợp lệ."
                });
            }

            try
            {
                await _diemService.DeleteDiemAsync(id.Value);

                return Json(new
                {
                    success = true,
                    message = "Xóa điểm thành công."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        #endregion
    }
}