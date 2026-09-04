using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDaoTao.Business;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;

namespace QuanLySinhVien.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MonHocController : Controller
    {
        private readonly IMonHocService _monHocService;
        private readonly IKhoaService _khoaService;
        public MonHocController(IMonHocService monHocService, IKhoaService khoaService)
        {
            _monHocService = monHocService;
            _khoaService = khoaService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            var khoas = await _khoaService.GetAllKhoasAsync();
            var monHocVM = new MonHocVM
            {
                MonHoc = new MonHoc(),
                KhoaList = khoas.Select(k => new SelectListItem
                {
                    Text = k.TenKhoa,
                    Value = k.MaKhoa.ToString()
                })
            };

            // Thêm mới
            if (id == null || id == 0)
            {
                return View(monHocVM);
            }

            // Cập nhật
            var monhoc = await _monHocService.GetMonHocByIdAsync(id.Value);

            if (monhoc == null)
            {
                return NotFound();
            }

            monHocVM.MonHoc = monhoc;
            return View(monHocVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(MonHocVM monhocVM)
        {
            if (!ModelState.IsValid)
            {
                var khoas = await _khoaService.GetAllKhoasAsync();
                monhocVM.KhoaList = khoas.Select(k => new SelectListItem
                {
                    Text = k.TenKhoa,
                    Value = k.MaKhoa.ToString()
                });
                return View(monhocVM);
            }

            if (monhocVM.MonHoc.MaMonHoc == 0)
            {
                await _monHocService.CreateMonHocAsync(monhocVM.MonHoc);
            }
            else
            {
                await _monHocService.UpdateMonHocAsync(monhocVM.MonHoc);
            }

            return RedirectToAction("Index");
        }
        #region CALL API
        public async Task<IActionResult>GetAll()
        {
            var monhoc = await _monHocService.GetAllMonHocAsync(includeKhoa: true);
            return Json(new { data = monhoc });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null)
            {
                return Json(new {success = false,message = "Không hợp lệ"});
            }
            
            await _monHocService.DeleteMonHocAsync(id.Value);
            TempData["success"] = "Bạn đã xóa thành công";
            return Json(new {success = true,message = "Bạn đã xóa thành công"});
        }
        #endregion
       
    }
}
