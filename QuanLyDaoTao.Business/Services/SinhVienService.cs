using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.DataAccess.Migrations;
using SinhVien = QuanLyDaoTao.Models.SinhVien;

namespace QuanLyDaoTao.Business.Services;

public class SinhVienService:ISinhVienService
{
    private readonly ApplicationDbContext _dbContext;
    public SinhVienService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<SinhVien>> GetAllSinhVien(bool includeLop = false)
    {
        if (includeLop)
        {
            return await _dbContext.sinhViens.Include(x => x.Lop).ThenInclude(l=>l.Khoa).ToListAsync();
        }
        return await _dbContext.sinhViens.ToListAsync();
    }

    public async Task<SinhVien?> GetSinhVienByIdAsync(int id, bool includeLop = false)
    {
        if (includeLop)
        {
            return await _dbContext.sinhViens.Include(lop => lop.Lop).ThenInclude(l=>l.Khoa).FirstOrDefaultAsync(u => u.MaSinhVien == id);
        }

        return await _dbContext.sinhViens.FirstOrDefaultAsync(u => u.MaSinhVien == id);
    }

    public async Task CreateSinhVienAsync(SinhVien sinhVien)
    {
        _dbContext.sinhViens.Add(sinhVien);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateSinhVien(SinhVien sinhVien)
    {
        _dbContext.sinhViens.Update(sinhVien);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteSinhVien(int id)
    {
        var sinhvien = await _dbContext.sinhViens.FirstOrDefaultAsync(u => u.MaSinhVien == id);
        if (sinhvien == null)
        {
            throw new KeyNotFoundException($"Mã sinh viên {id} không tìm thấy");
        }
        _dbContext.sinhViens.Remove(sinhvien);
        await _dbContext.SaveChangesAsync();
    }
}