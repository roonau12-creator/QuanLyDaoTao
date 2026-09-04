using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
namespace QuanLyDaoTao.Business.Services
{
    public class HocPhanService : IHocPhanService
    {
        private readonly ApplicationDbContext _dbContext;
        public HocPhanService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<HocPhan> CreateHocPhanAsync(HocPhan hocPhan)
        {
            await _dbContext.hocPhans.AddAsync(hocPhan);
            await _dbContext.SaveChangesAsync();
            return hocPhan;
        }

        public async Task DeleteHocPhanAsync(int id)
        {
            var hocPhan = await _dbContext.hocPhans.FindAsync(id);
            if (hocPhan != null)
            {
                _dbContext.hocPhans.Remove(hocPhan);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<HocPhan>> GetAllHocPhansAsync(bool includeMonHoc = false, bool includeGiangVien = false, bool includeHocKy = false)
        {
            IQueryable<HocPhan> query = _dbContext.hocPhans;
            if (includeMonHoc) query = query.Include(hp => hp.MonHoc);
            if (includeGiangVien) query = query.Include(hp => hp.GiangVien);
            if (includeHocKy) query = query.Include(hp => hp.HocKy);
            return await query.ToListAsync();
        }

        public async Task<HocPhan?> GetHocPhanByIdAsync(int id, bool includeMonHoc = false, bool includeGiangVien = false, bool includeHocKy = false)
        {
            IQueryable<HocPhan> query = _dbContext.hocPhans;
            if (includeMonHoc) query = query.Include(hp => hp.MonHoc);
            if (includeGiangVien) query = query.Include(hp => hp.GiangVien);
            if (includeHocKy) query = query.Include(hp => hp.HocKy);
            return await query.FirstOrDefaultAsync(hp => hp.MaHocPhan == id);
        }

        public async Task UpdateHocPhanAsync(HocPhan hocPhan)
        {
            _dbContext.hocPhans.Update(hocPhan);
            await _dbContext.SaveChangesAsync();
        }
    }
}