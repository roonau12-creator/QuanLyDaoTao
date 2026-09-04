using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Business.Services.IServices
{
    public  interface IMonHocService
    {
        Task<IEnumerable<MonHoc>> GetAllMonHocAsync(bool includeKhoa = false);
        Task<MonHoc?> GetMonHocByIdAsync(int id, bool includeKhoa = false);
        Task CreateMonHocAsync(MonHoc monHoc);
        Task UpdateMonHocAsync(MonHoc monHoc);
        Task DeleteMonHocAsync(int id);
    }
}