using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using QuanLyDaoTao.Utility;
namespace QuanLyDaoTao.Areas.Admin.Controllers;
[Area("Admin")]
public class SinhVienController : Controller
{
    // GET
    private readonly ISinhVienService _sinhVienService;
    private readonly ILopService _lopService;
    private readonly IWebHostEnvironment  _webHostEnvironment;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SinhVienController(ISinhVienService sinhVienService, ILopService lopService, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _lopService = lopService;
        _sinhVienService = sinhVienService;
        _webHostEnvironment = webHostEnvironment;
        _userManager = userManager;
        _roleManager = roleManager;
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
                await EnsureStudentAccountAsync(sinhVienVM.SinhVien);
            }
            else
            {
                var existingSinhVien = await _sinhVienService.GetSinhVienByIdAsync(sinhVienVM.SinhVien.MaSinhVien);
                if (existingSinhVien == null)
                {
                    return NotFound();
                }

                var submitted = sinhVienVM.SinhVien;
                existingSinhVien.MaSoSinhVien = submitted.MaSoSinhVien;
                existingSinhVien.Hoten = submitted.Hoten;
                existingSinhVien.NgaySinh = submitted.NgaySinh;
                existingSinhVien.GioiTinh = submitted.GioiTinh;
                existingSinhVien.CanCuocCongDan = submitted.CanCuocCongDan;
                existingSinhVien.Email = submitted.Email;
                existingSinhVien.SoDienThoai = submitted.SoDienThoai;
                existingSinhVien.DiaChi = submitted.DiaChi;
                existingSinhVien.NgayNhapHoc = submitted.NgayNhapHoc;
                existingSinhVien.MaLop = submitted.MaLop;

                if (file != null)
                {
                    existingSinhVien.AnhDaiDien = submitted.AnhDaiDien;
                }

                await _sinhVienService.UpdateSinhVien(existingSinhVien);
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

    private async Task EnsureStudentAccountAsync(Models.SinhVien sinhVien)
    {
        if (string.IsNullOrWhiteSpace(sinhVien.MaSoSinhVien))
        {
            return;
        }

        if (!await _roleManager.RoleExistsAsync(SD.Role_Student))
        {
            await _roleManager.CreateAsync(new IdentityRole(SD.Role_Student));
        }

        var user = await _userManager.FindByNameAsync(sinhVien.MaSoSinhVien);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = sinhVien.MaSoSinhVien,
                Email = sinhVien.Email,
                EmailConfirmed = true,
                Hoten = sinhVien.Hoten,
                MaSinhVien = sinhVien.MaSinhVien
            };
            var result = await _userManager.CreateAsync(user, ApplicationDbInitializer.DefaultStudentPassword);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, SD.Role_Student);
            }
        }
    }
}
