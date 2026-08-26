using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuanLySinhVien.DataAccess.Data;

namespace QuanLySinhVien.Areas.Admin
{
    [Area("Admin")]
    public class KhoaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public KhoaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var khoas = await _context.khoas.ToListAsync();
            return View(khoas);
        }
        #region CALL API
        public async Task<IActionResult> GetAll()
        {
            var khoas = await _context.khoas.ToListAsync();
            return Json(new { data = khoas });
        }
        #endregion


    }
}