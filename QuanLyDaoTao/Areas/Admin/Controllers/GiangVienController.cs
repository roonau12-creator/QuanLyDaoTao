using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using  Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDaoTao.Models.ViewModels;
using Microsoft.Extensions.Logging;
using QuanLyDaoTao.Business;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiangVienController : Controller
    {
        private readonly IGiangVienService _giangVienService;
        private readonly IKhoaService _khoaService;

        public GiangVienController(IGiangVienService giangVienService, IKhoaService khoaService)
        {
            _giangVienService = giangVienService;
            _khoaService = khoaService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            GiangVienVM giangVienVM = new GiangVienVM();
            giangVienVM.GiangVien = new GiangVien();
            giangVienVM.DanhSachKhoa = (await _khoaService.GetAllKhoasAsync()).Select(x => new SelectListItem
            {
                Text = x.TenKhoa,
                Value = x.MaKhoa.ToString()
            });

            if (id == null || id == 0)
            {
                return View(giangVienVM);
            }

            var giangVien = await _giangVienService.GetGiangVienByIdAsync(id.Value);

            if (giangVien == null)
            {
                return NotFound();
            }

            giangVienVM.GiangVien = giangVien;

            return View(giangVienVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(GiangVienVM giangVienVM)
        {
            giangVienVM.GiangVien.NgaySinh = EnsureUtc(giangVienVM.GiangVien.NgaySinh);

            if (ModelState.IsValid)
            {
                if (giangVienVM.GiangVien.MaGiangVien == 0)
                {
                    await _giangVienService.CreateGiangVienAsync(giangVienVM.GiangVien);
                }
                else
                {
                    await _giangVienService.UpdateGiangVienAsync(giangVienVM.GiangVien);
                }
                

                TempData["success"] =
                    giangVienVM.GiangVien.MaGiangVien == 0
                        ? "Thêm giảng viên thành công"
                        : "Cập nhật giảng viên thành công";

                return RedirectToAction("Index");
            }


            giangVienVM.DanhSachKhoa = (await _khoaService.GetAllKhoasAsync()).Select(x => new SelectListItem
            {
                Text = x.TenKhoa,
                Value = x.MaKhoa.ToString()
            });

            return View(giangVienVM);
        }
        
        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var giangViens = await _giangVienService.GetAllGiangVienAsync(includeKhoa: true);
            return Json(new { data = giangViens });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new
                {
                    success = false,
                    message = "Mã giảng viên không hợp lệ."
                });
            }


            await _giangVienService.DeleteGiangVienAsync(id.Value);

            return Json(new
            {
                success = true,
                message = "Xóa giảng viên thành công."
            });
        }
        #endregion

        private static DateTime EnsureUtc(DateTime value) => value.Kind switch
        {
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
        };
    }
}