using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDaoTao.Business.Services
{
    public class GiangVienService : IGiangVienService
    {
        private readonly ApplicationDbContext _dbContext;
        public GiangVienService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GiangVien> CreateGiangVienAsync(GiangVien giangVien)
        {
            giangVien.NgaySinh = EnsureUtc(giangVien.NgaySinh);
            _dbContext.giangViens.Add(giangVien);
            await _dbContext.SaveChangesAsync();
            return giangVien;
        }

        public async Task DeleteGiangVienAsync(int id)
        {
            var giangVien = await _dbContext.giangViens.FindAsync(id);
            if (giangVien != null)
            {
                _dbContext.giangViens.Remove(giangVien);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GiangVien>> GetAllGiangVienAsync(bool includeKhoa = false)
        {
            if (includeKhoa)
            {
                return await _dbContext.giangViens.Include(gv=>gv.Khoa).ToListAsync();
            }
            else
            {
                return await _dbContext.giangViens.ToListAsync();
            }
        }

        public async Task<GiangVien?> GetGiangVienByIdAsync(int id, bool includeKhoa = false)
        {
            if(includeKhoa)
            {
                return await _dbContext.giangViens.Include(gv => gv.Khoa).FirstOrDefaultAsync(gv => gv.MaGiangVien == id);
            }
            else
            {
                return await _dbContext.giangViens.FirstOrDefaultAsync(gv => gv.MaGiangVien == id);
            }
        }

        public async Task<GiangVien> UpdateGiangVienAsync(GiangVien giangVien)
        {
            giangVien.NgaySinh = EnsureUtc(giangVien.NgaySinh);

            var existingGiangVien = await _dbContext.giangViens.FindAsync(giangVien.MaGiangVien);
            if (existingGiangVien == null)
            {
                throw new ArgumentException("Giảng viên không tồn tại");
            }

            _dbContext.Entry(existingGiangVien).CurrentValues.SetValues(giangVien);
            await _dbContext.SaveChangesAsync();
            return existingGiangVien;
        }

        private static DateTime EnsureUtc(DateTime value) => value.Kind switch
        {
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
        };
    }
}