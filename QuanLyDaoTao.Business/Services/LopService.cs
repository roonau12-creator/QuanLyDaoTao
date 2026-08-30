using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Business.Services;

public class LopService : ILopService
{
    private readonly ApplicationDbContext _context;

    public LopService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateLopAsync(Lop lop)
    {
        _context.lops.Add(lop);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLopAsync(int id)
    {
        var lop = await _context.lops.FirstOrDefaultAsync(l => l.MaLop == id);
        if (lop == null)
        {
            throw new KeyNotFoundException($"Mã lớp {id} không tồn tại");
        }
        _context.lops.Remove(lop);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Lop>> GetAllLopAsync(bool includeKhoa = false)
    {
        if (includeKhoa)
        {
            return await _context.lops.Include(l => l.Khoa).ToListAsync();
        }
        return await _context.lops.ToListAsync();
        
    }

    public async Task<Lop?> GetLopByIdAsync(int id, bool includeKhoa = false)
    {
        if (includeKhoa)
        {
            return await _context.lops.Include(u => u.Khoa).FirstOrDefaultAsync(u => u.MaLop == id);
        }
        return await _context.lops.FirstOrDefaultAsync(u => u.MaLop == id);
    }

    public async Task UpdateLopAsync(Lop lop)
    {
        _context.lops.Update(lop);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetRelatedCountsAsync(int lopId)
    {
        var sinhVienCount = await _context.sinhViens.CountAsync(s => s.MaLop == lopId);
        return sinhVienCount;
    }
}
