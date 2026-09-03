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
    public class HocPhongController : Controller
    {
        private readonly IHocPhongService _hocPhongService;

        public HocPhongController(IHocPhongService hocPhongService)
        {
            _hocPhongService = hocPhongService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new PhongHoc());
            }

            var phongHoc = await _hocPhongService.GetPhongHocByIdAsync(id.Value);

            if (phongHoc == null)
            {
                return NotFound();
            }

            return View(phongHoc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PhongHoc phongHoc)
        {
            if (ModelState.IsValid)
            {
                if (phongHoc.MaPhongHoc == 0)
                {
                    await _hocPhongService.CreatePhongHocAsync(phongHoc);
                }
                else
                {
                    await _hocPhongService.UpdatePhongHocAsync(phongHoc);
                }
                return RedirectToAction("Index");
            }

            TempData["success"] = "Cập nhật phòng học thành công.";
            return View(phongHoc);

        }
        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _hocPhongService.GetAllPhongHocsAsync();
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
                    message = "Mã phòng học không hợp lệ."
                });
            }



            await _hocPhongService.DeletePhongHocAsync(id.Value);

            return Json(new
            {
                success = true,
                message = "Xóa phòng học thành công."
            });
        }

        #endregion

    }
}
