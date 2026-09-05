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
    public class ThongBaoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IThongBaoService _thongBaoService;

        public ThongBaoController(
            ApplicationDbContext context,
            IThongBaoService thongBaoService)
        {
            _context = context;
            _thongBaoService = thongBaoService;
        }

        public async Task<IActionResult> Index()
        {
            var thongBaos = await _thongBaoService.GetAllThongBaoAsync();
            return View(thongBaos.OrderByDescending(t => t.NgayDang));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var thongBao = await _thongBaoService.GetThongBaoByIdAsync(id.Value);
            if (thongBao == null) return NotFound();

            return View(thongBao);
        }

        [HttpPost]
        public async Task<IActionResult> DanhDaDoc(int id)
        {
            var thongBao = await _thongBaoService.GetThongBaoByIdAsync(id);
            if (thongBao == null)
                return Json(new { success = false, message = "Không tìm thấy thông báo." });

            return Json(new { success = true, message = "Đã đánh dấu đã đọc." });
        }
    }
}
