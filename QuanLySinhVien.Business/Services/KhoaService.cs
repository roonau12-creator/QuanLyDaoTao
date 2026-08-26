using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.DataAccess.Data;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Business.Services
{
    public class KhoaService : IKhoaService
    {
        private readonly ApplicationDbContext _context;
        public KhoaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateKhoaAsync(Khoa khoa)
        {
            _context.khoas.Add(khoa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteKhoaAsync(int id)
        {
            var khoas = await _context.khoas.FirstOrDefaultAsync(u => u.MaKhoa == id);
            if (khoas == null)
            {
                throw new KeyNotFoundException($"Mã khoa {id} không tồn tại");
            }
            _context.khoas.Remove(khoas);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Khoa>> GetAllKhoasAsync()
        {
            return await _context.khoas
                .AsNoTracking()
                .OrderBy(khoa => khoa.TenKhoa)
                .ToListAsync();
        }

        public async Task<Khoa?> GetKhoaByIdAsync(int id)
        {
            return await _context.khoas.FirstOrDefaultAsync(u => u.MaKhoa == id);
        }

        public async Task UpdateKhoaAsync(Khoa khoa)
        {
            _context.khoas.Update(khoa);
            await _context.SaveChangesAsync();
        }
        
    }
}
