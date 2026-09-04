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
    public class ThongBaoService : IThongBaoService
    {
        private readonly ApplicationDbContext _context;
        public ThongBaoService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<ThongBao> CreateThongBaoAsync(ThongBao thongBao)
        {
            _context.thongBaos.Add(thongBao);
            await _context.SaveChangesAsync();
            return thongBao;
        }

        public async Task DeleteThongBaoAsync(int id)
        {
            var thongBao = await _context.thongBaos.FindAsync(id);
            if (thongBao != null)
            {
                _context.thongBaos.Remove(thongBao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ThongBao>> GetAllThongBaoAsync()
        {
            return await _context.thongBaos.ToListAsync();
        }

        public async Task<ThongBao?> GetThongBaoByIdAsync(int id)
        {
            return await _context.thongBaos.FindAsync(id);
        }

        public async Task<ThongBao> UpdateThongBaoAsync(ThongBao thongBao)
        {
            _context.thongBaos.Update(thongBao);
            await _context.SaveChangesAsync();
            return thongBao;
        }
    }
}