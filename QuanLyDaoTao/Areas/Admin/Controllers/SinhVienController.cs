using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDaoTao.Business;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.Models.ViewModels;
namespace QuanLyDaoTao.Areas.Admin.Controllers;
[Area("Admin")]
public class SinhVienController : Controller
{
    // GET
    private readonly ISinhVienService _sinhVienService;
    private readonly ILopService _lopService;
    private readonly IKhoaService _khoaService;
    private readonly IWebHostEnvironment  _webHostEnvironment;

    public SinhVienController(ISinhVienService sinhVienService,ILopService lopService,IWebHostEnvironment webHostEnvironment,IKhoaService khoaService)
    {
        _lopService = lopService;
        _sinhVienService = sinhVienService;
        _webHostEnvironment = webHostEnvironment;
        _khoaService = khoaService;
    }
    
    public async Task<IActionResult> Index()
    {
        return View();
    }
    public async Task<IActionResult> UpSert(int? id)
    {
        var lop = await _lopService.GetAllLopAsync();
        var khoa = await _khoaService.GetAllKhoasAsync();
        SinhVienVM sinhVienVM = new()
        {
            LopList = lop.Select(u => new SelectListItem
            {
                Text = u.TenLop,
                Value = u.MaLop.ToString()
            }),
            KhoaList = khoa.Select(u => new SelectListItem
            {
                Text = u.TenKhoa,
                Value = u.MaKhoa.ToString()
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
            if (sinhVienVM.SinhVien.MaSinhVien == 0)
            {
                await _sinhVienService.CreateSinhVienAsync(sinhVienVM.SinhVien);
            }
            else
            {
                await _sinhVienService.UpdateSinhVien(sinhVienVM.SinhVien);
            }
            TempData["success"] = sinhVienVM.SinhVien.MaSinhVien == 0 ? "Thêm sinh viên thành công" : "Cập nhật sinh viên thành công";
            return RedirectToAction("Index");
        }
        else
        {
            var lop = await _lopService.GetAllLopAsync();
            var khoa = await _khoaService.GetAllKhoasAsync();
            SinhVienVM sinhVien = new()
            {
                LopList = lop.Select(u => new SelectListItem
                {
                    Text = u.TenLop,
                    Value = u.MaLop.ToString()
                }),
                KhoaList = khoa.Select(u => new SelectListItem
                {
                    Text = u.TenKhoa,
                    Value = u.MaKhoa.ToString()
                })

            };
            return View(sinhVienVM);
        }
    }

    #region CALL API

    public async Task<IActionResult> GetAll()
    {
        var sinhvien = await _sinhVienService.GetAllSinhVien(includeLop:true);
        return Json(new {data = sinhvien});
    }
    

    #endregion
}