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
    public class ThongBaoController : Controller
    {
        private readonly IThongBaoService _thongBaoService;

        public ThongBaoController(IThongBaoService thongBaoService)
        {
            _thongBaoService = thongBaoService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            // Thêm mới
            if (id == null || id == 0)
            {
                return View(new ThongBao());
            }

            // Sửa
            var thongBao = await _thongBaoService.GetThongBaoByIdAsync(id.Value);

            if (thongBao == null)
            {
                return NotFound();
            }

            return View(thongBao);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ThongBao thongBao)
        {
            if (ModelState.IsValid)
            {
                if (thongBao.MaThongBao == 0)
                {
                    // Thêm thông báo
                    await _thongBaoService.CreateThongBaoAsync(thongBao);

                    TempData["success"] = "Thêm thông báo thành công";
                }
                else
                {
                    // Cập nhật thông báo
                    await _thongBaoService.UpdateThongBaoAsync(thongBao);

                    TempData["success"] = "Cập nhật thông báo thành công";
                }


                return RedirectToAction("Index");
            }

            return View(thongBao);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var thongBao = await _thongBaoService.GetThongBaoByIdAsync(id.Value);
            if (thongBao == null)
            {
                return NotFound();
            }

            return View(thongBao);
        }
        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _thongBaoService.GetAllThongBaoAsync();
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
                    message = "Mã thông báo không hợp lệ."
                });
            }



            await _thongBaoService.DeleteThongBaoAsync(id.Value);

            return Json(new
            {
                success = true,
                message = "Xóa thông báo thành công."
            });
        }

        #endregion
    }
}