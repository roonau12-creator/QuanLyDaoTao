using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.Models;
namespace QuanLyDaoTao.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HocKyController : Controller
    {
        private readonly IHocKyService _hocKyService;
        public HocKyController(IHocKyService hocKyService)
        {
            _hocKyService = hocKyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new HocKy());
            }

            var hocKy = await _hocKyService.GetHocKyByIdAsync(id.Value);
            if (hocKy == null)
            {
                return NotFound();
            }

            return View(hocKy);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult>Upsert(HocKy hocKy)
        {
            hocKy.NgayBatDau = EnsureUtc(hocKy.NgayBatDau);
            hocKy.NgayKetThuc = EnsureUtc(hocKy.NgayKetThuc);

            if (ModelState.IsValid)
            {
                if (hocKy.MaHocKy == 0)
                {
                    await _hocKyService.CreateHocKyAsync(hocKy);
                }
                else
                {
                    await _hocKyService.UpdateHocKyAsync(hocKy);

                }
                return RedirectToAction("Index");
            }
            return View(hocKy);
        }
        #region CALL API
        public async Task<IActionResult> GetAll()
        {
            var hocky = await _hocKyService.GetAllHocKyAsync();
            return Json(new { data = hocky });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var hocKy = await _hocKyService.GetHocKyByIdAsync(id);

            if (hocKy == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Không tìm thấy học kỳ"
                });
            }

            await _hocKyService.DeleteHocKyAsync(id);

            return Json(new
            {
                success = true,
                message = "Xóa thành công"
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