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
    public class HocKyService : IHocKyService
    {
        private readonly ApplicationDbContext _db;
        public HocKyService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateHocKyAsync(HocKy hocKy)
        {
            _db.hocKys.Add(hocKy);
            await _db.SaveChangesAsync();

        }

        public async Task DeleteHocKyAsync(int id)
        {
            var hocky = await _db.hocKys.FirstOrDefaultAsync(p => p.MaHocKy == id);
            if (hocky == null)
            {
                throw new KeyNotFoundException($"Mã học kỳ {id} không tìm thấy");
            }
            _db.hocKys.Remove(hocky);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HocKy>> GetAllHocKyAsync()
        {
            return await _db.hocKys.ToListAsync();
        }

        public async Task<HocKy?> GetHocKyByIdAsync(int id)
        {
            return await _db.hocKys.FirstOrDefaultAsync(p => p.MaHocKy == id);
        }

        public async Task UpdateHocKyAsync(HocKy hocKy)
        {
            _db.hocKys.Update(hocKy);
            await _db.SaveChangesAsync();
            
        }
    }
}