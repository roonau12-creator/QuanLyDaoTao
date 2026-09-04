using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Business.Services
{
    
    public class MonHocService : IMonHocService
    {
        private readonly ApplicationDbContext _db;
        public MonHocService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateMonHocAsync(MonHoc monHoc)
        {
            _db.monHocs.Add(monHoc);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteMonHocAsync(int id)
        {
            var monhoc = await _db.monHocs.FirstOrDefaultAsync(u => u.MaMonHoc == id);
            if(monhoc == null)
            {
                throw new KeyNotFoundException($"Mã môn học ${id} không tìm thấy");
            }
            _db.monHocs.Remove(monhoc);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<MonHoc>> GetAllMonHocAsync(bool includeKhoa = false)
        {
            if (includeKhoa)
            {
                return await _db.monHocs.Include(k => k.Khoa).ToListAsync();
            }
            return await _db.monHocs.ToListAsync();
        }

        public async Task<MonHoc?> GetMonHocByIdAsync(int id, bool includeKhoa = false)
        {
            if (includeKhoa)
            {
                return await _db.monHocs.Include(k => k.Khoa).FirstOrDefaultAsync(p => p.MaMonHoc == id);
            }
            return await _db.monHocs.FirstOrDefaultAsync(p => p.MaMonHoc==id);
            
        }

        public async Task UpdateMonHocAsync(MonHoc monHoc)
        {
            _db.monHocs.Update(monHoc);
            await _db.SaveChangesAsync();
        }
    }
}
