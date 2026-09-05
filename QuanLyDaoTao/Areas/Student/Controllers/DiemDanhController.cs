using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Utility;

namespace QuanLyDaoTao.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Role_Student)]
    public class DiemDanhController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDiemDanhService _diemDanhService;

        public DiemDanhController(
            ApplicationDbContext context,
            IDiemDanhService diemDanhService)
        {
            _context = context;
            _diemDanhService = diemDanhService;
        }

        private async Task<int?> GetMaSinhVienAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return null;
            var user = await _context.Users.FindAsync(userId);
            if (user is Models.ApplicationUser appUser)
                return appUser.MaSinhVien;
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var maSinhVien = await GetMaSinhVienAsync();
            if (maSinhVien == null) return RedirectToAction("AccessDenied", "Account", new { area = "Identity" });

            var lichSu = await _diemDanhService.HistoryDiemDanhAsync(maSinhVien.Value);

            return View(lichSu);
        }

        public async Task<IActionResult> TyLeChuyenCan()
        {
            var maSinhVien = await GetMaSinhVienAsync();
            if (maSinhVien == null) return RedirectToAction("AccessDenied", "Account", new { area = "Identity" });

            var thongKe = await _diemDanhService.ThongKeChuyenCanAsync(maSinhVien.Value);

            return View(thongKe.FirstOrDefault());
        }
    }
}
