using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;
namespace QuanLyDaoTao.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Role_Teacher")]
    public class DiemDanhController : Controller
    {
        private readonly IDiemDanhService _diemDanhService;
        private readonly ISinhVienService _sinhVienService;
        private readonly ApplicationDbContext _context;

        public DiemDanhController(IDiemDanhService diemDanhService, ISinhVienService sinhVienService, ApplicationDbContext context)
        {
            _diemDanhService = diemDanhService;
            _sinhVienService = sinhVienService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var diemDanh = await _diemDanhService.GetDiemDanhByIdAsync(id.Value, includeSinhVien: true, includeLichHoc: true);
            if (diemDanh == null)
            {
                return NotFound();
            }

            return View(diemDanh);
        }
        [HttpGet]
        public async Task<IActionResult> Attendance(int maLichHoc)
        {
            var sinhViens = await _sinhVienService.GetAllSinhVien();
            var lichHocs = await _context.lichHocs.ToListAsync();

            DiemDanhVM viewModel = new DiemDanhVM
            {
                LichHoc = lichHocs.FirstOrDefault(l => l.MaLichHoc == maLichHoc) ?? new LichHoc(),
                ListSinhVien = sinhViens,
                ListLichHoc = lichHocs
            };

            if (viewModel.LichHoc.MaLichHoc == 0)
            {
                return NotFound();
            }

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Attendance(DiemDanhVM model)
        {
            if (model.LichHoc == null || model.LichHoc.MaLichHoc == 0)
            {
                ModelState.AddModelError("", "Vui lòng chọn lịch học.");
            }

            if (!ModelState.IsValid)
            {
                var sinhViens = await _sinhVienService.GetAllSinhVien();
                var lichHocs = await _context.lichHocs.ToListAsync();

                model.ListSinhVien = sinhViens;
                model.ListLichHoc = lichHocs;

                return View(model);
            }

            if (model.SinhViens != null)
            {
                foreach (var item in model.SinhViens)
                {
                    if (item.MaSinhVien == 0)
                    {
                        continue;
                    }

                    await _diemDanhService.CreateDiemDanhAsync(new DiemDanh
                    {
                        MaLichHoc = model.LichHoc!.MaLichHoc,
                        MaSinhVien = item.MaSinhVien,
                        TrangThai = string.IsNullOrWhiteSpace(item.TrangThai) ? "Có mặt" : item.TrangThai,
                        GhiChu = item.GhiChu
                    });
                }
            }

            TempData["success"] = "Điểm danh sinh viên thành công";

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var diemDanh = await _diemDanhService.GetDiemDanhByIdAsync(id.Value, includeSinhVien: true, includeLichHoc: true);

            if (diemDanh == null)
            {
                return NotFound();
            }

            DiemDanhVM viewModel = new DiemDanhVM
            {
                DiemDanh = new DiemDanh
                {
                    MaDiemDanh = diemDanh.MaDiemDanh,
                    MaSinhVien = diemDanh.MaSinhVien,
                    MaLichHoc = diemDanh.MaLichHoc,
                    TrangThai = diemDanh.TrangThai,
                    GhiChu = diemDanh.GhiChu
                }
            };

            return View("Edit", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DiemDanhVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var diemDanh = await _diemDanhService.GetDiemDanhByIdAsync(viewModel.DiemDanh.MaDiemDanh, includeSinhVien: true, includeLichHoc: true);

                if (diemDanh == null)
                {
                    return NotFound();
                }

                diemDanh.MaSinhVien = viewModel.DiemDanh.MaSinhVien;
                diemDanh.MaLichHoc = viewModel.DiemDanh.MaLichHoc;
                diemDanh.TrangThai = viewModel.DiemDanh.TrangThai;
                diemDanh.GhiChu = viewModel.DiemDanh.GhiChu;

                await _diemDanhService.UpdateDiemDanhAsync(diemDanh);

                TempData["success"] = "Cập nhật điểm danh thành công";

                return RedirectToAction("Index");
            }

            return View("Edit", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ThongKe(int maSinhVien)
        {
            var thongKe = await _diemDanhService
                .ThongKeChuyenCanAsync(maSinhVien);

            if (thongKe == null)
            {
                return NotFound();
            }

            return View(thongKe);
        }
        [HttpGet]
        public async Task<IActionResult> History(int maSinhVien)
        {
            var diemDanhs = await _diemDanhService.HistoryDiemDanhAsync(maSinhVien);

            return View(diemDanhs);
        }
        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diemDanhList = await _diemDanhService.GetAllDiemDanhAsync(includeSinhVien: true);
            return Json(new { data = diemDanhList });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new
                {
                    success = false,
                    message = "Mã điểm danh không hợp lệ."
                });
            }

            await _diemDanhService.DeleteDiemDanhAsync(id.Value);
            return Json(new
            {
                success = true,
                message = "Xóa điểm danh thành công."
            });
        }
        #endregion
    }
}
