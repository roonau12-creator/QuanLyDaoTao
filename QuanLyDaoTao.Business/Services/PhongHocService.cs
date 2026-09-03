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
    public class PhongHocService : IHocPhongService
    {
        private readonly ApplicationDbContext _dbContext;
        public PhongHocService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PhongHoc> CreatePhongHocAsync(PhongHoc phongHoc)
        {
            _dbContext.phongHocs.Add(phongHoc);
            await _dbContext.SaveChangesAsync();
            return phongHoc;
        }

        public async Task DeletePhongHocAsync(int id)
        {
            var phongHoc = await _dbContext.phongHocs.FindAsync(id);
            if (phongHoc != null)
            {
                _dbContext.phongHocs.Remove(phongHoc);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PhongHoc>> GetAllPhongHocsAsync()
        {
            return await _dbContext.phongHocs.ToListAsync();
        }

        public async Task<PhongHoc?> GetPhongHocByIdAsync(int id)
        {
            return await _dbContext.phongHocs.FindAsync(id);
        }

        public async Task UpdatePhongHocAsync(PhongHoc phongHoc)
        {
            _dbContext.phongHocs.Update(phongHoc);
            await _dbContext.SaveChangesAsync();
        }
        
    }
}