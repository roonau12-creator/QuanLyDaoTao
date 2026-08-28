using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.DataAccess.Data;

namespace QuanLySinhVien.Controllers;

public class KhoaController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var khoas = await dbContext.khoas
            .AsNoTracking()
            .OrderBy(khoa => khoa.TenKhoa)
            .ToListAsync();

        return View(khoas);
    }
}
