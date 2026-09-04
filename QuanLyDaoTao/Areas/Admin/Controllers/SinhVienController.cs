using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;
namespace QuanLyDaoTao.Areas.Admin.Controllers;
[Area("Admin")]
public class SinhVienController : Controller
{
    // GET
    private readonly ISinhVienService _sinhVienService;
    private readonly ILopService _lopService;
    private readonly IWebHostEnvironment  _webHostEnvironment;

    public SinhVienController(ISinhVienService sinhVienService, ILopService lopService, IWebHostEnvironment webHostEnvironment)
    {
        _lopService = lopService;
        _sinhVienService = sinhVienService;
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async Task<IActionResult> Index()
    {
        return View();
    }
    public async Task<IActionResult> UpSert(int? id)
    {

        var lop = await _lopService.GetAllLopAsync();
        SinhVienVM sinhVienVM = new()
        {
            LopList = lop.Select(u => new SelectListItem
            {
                Text = u.TenLop,
                Value = u.MaLop.ToString()
            })

        };
        if (id == 0 || id == null)
        {
            return View(sinhVienVM);
        }
        else
        {
            sinhVienVM.SinhVien = await _sinhVienService.GetSinhVienByIdAsync(id.Value) ?? new Models.SinhVien();
            return View(sinhVienVM);
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>UpSert(SinhVienVM sinhVienVM,IFormFile? file)
    {
        sinhVienVM.SinhVien.NgaySinh = EnsureUtc(sinhVienVM.SinhVien.NgaySinh);
        sinhVienVM.SinhVien.NgayNhapHoc = EnsureUtc(sinhVienVM.SinhVien.NgayNhapHoc);

        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string sinhvienPath = Path.Combine("images", "sinhviens");
                string finalPath = Path.Combine(wwwRootPath, sinhvienPath);

                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }
                using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                sinhVienVM.SinhVien.AnhDaiDien = "/" + Path.Combine(sinhvienPath, fileName).Replace("\\", "/");
            }
            var isNew = sinhVienVM.SinhVien.MaSinhVien == 0;
            if (isNew)
            {
                await _sinhVienService.CreateSinhVienAsync(sinhVienVM.SinhVien);
            }
            else
            {
                var existingSinhVien = await _sinhVienService.GetSinhVienByIdAsync(sinhVienVM.SinhVien.MaSinhVien);
                if (existingSinhVien == null)
                {
                    return NotFound();
                }

                if (file == null)
                {
                    sinhVienVM.SinhVien.AnhDaiDien = existingSinhVien.AnhDaiDien;
                }
                await _sinhVienService.UpdateSinhVien(sinhVienVM.SinhVien);
            }
            TempData["success"] = isNew ? "Thêm sinh viên thành công" : "Cập nhật sinh viên thành công";
            return RedirectToAction("Index");
        }
        else
        {
            var lop = await _lopService.GetAllLopAsync();
            sinhVienVM.LopList = lop.Select(u => new SelectListItem
            {
                Text = u.TenLop,
                Value = u.MaLop.ToString()
            });
            return View(sinhVienVM);
        }
    }

    #region CALL API

    public async Task<IActionResult> GetAll()
    {
        var sinhvien = await _sinhVienService.GetAllSinhVien(includeLop:true);
        return Json(new {data = sinhvien});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var sinhVien = await _sinhVienService.GetSinhVienByIdAsync(id);
        if (sinhVien == null)
        {
            return Json(new { success = false, message = "Không tìm thấy sinh viên cần xóa." });
        }

        await _sinhVienService.DeleteSinhVien(id);
        return Json(new { success = true, message = "Đã xóa sinh viên thành công." });
    }
    #endregion

    private static DateTime EnsureUtc(DateTime value) => value.Kind switch
    {
        DateTimeKind.Utc => value,
        DateTimeKind.Local => value.ToUniversalTime(),
        _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
    };
}
