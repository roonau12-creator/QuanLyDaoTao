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
    public class DangKyHocPhanService : IDangKyHocPhanService
    {
        private readonly ApplicationDbContext _db;
        public DangKyHocPhanService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> CountAsync(int MaHocPhan)
        {
            return await _db.dangKyHocPhans.CountAsync(x => x.MaHocPhan == MaHocPhan);
        }

        public async Task CreateDangKyHocPhanAsync(DangKyHocPhan dangKyHocPhan)
        {
            _db.dangKyHocPhans.Add(dangKyHocPhan);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> DeleteDangKyHocPhanAsync(int id)
        {
            var dangKyHocPhan = await _db.dangKyHocPhans.FirstOrDefaultAsync(d => d.MaDangKy == id);
            if (dangKyHocPhan == null)
                return false;

            _db.dangKyHocPhans.Remove(dangKyHocPhan);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DangKyHocPhan>> GetAllDangKyHocPhansAsync(bool includeSinhVien = false, bool includeHocPhan = false, bool includeHocKy = false)
        {
            IQueryable<DangKyHocPhan> query = _db.dangKyHocPhans;
            if (includeSinhVien) query = query.Include(d => d.SinhVien);
            if (includeHocPhan) query = query.Include(d => d.HocPhan).ThenInclude(hp => hp.MonHoc);
            if (includeHocKy) query = query.Include(d => d.HocKy);
            return await query.ToListAsync();
        }

        public async Task<DangKyHocPhan?> GetDangKyHocPhanByIdAsync(int id, bool includeSinhVien = false, bool includeHocPhan = false, bool includeHocKy = false)
        {
            IQueryable<DangKyHocPhan> query = _db.dangKyHocPhans;
            if (includeSinhVien) query = query.Include(d => d.SinhVien);
            if (includeHocPhan) query = query.Include(d => d.HocPhan).ThenInclude(hp => hp.MonHoc);
            if (includeHocKy) query = query.Include(d => d.HocKy);
            return await query.FirstOrDefaultAsync(d => d.MaDangKy == id);
        }

        public async Task UpdateDangKyHocPhanAsync(DangKyHocPhan dangKyHocPhan)
        {
            _db.dangKyHocPhans.Update(dangKyHocPhan);
            await _db.SaveChangesAsync();
        }
    }
}