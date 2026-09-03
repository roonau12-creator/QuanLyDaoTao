using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;
namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface IDangKyHocPhanService
    {
        Task<IEnumerable<DangKyHocPhan>> GetAllDangKyHocPhansAsync(bool includeSinhVien = false, bool includeHocPhan = false, bool includeHocKy = false);
        Task<DangKyHocPhan?> GetDangKyHocPhanByIdAsync(int id, bool includeSinhVien = false, bool includeHocPhan = false, bool includeHocKy = false);
        Task CreateDangKyHocPhanAsync(DangKyHocPhan dangKyHocPhan);
        Task UpdateDangKyHocPhanAsync(DangKyHocPhan dangKyHocPhan);
        Task<bool> DeleteDangKyHocPhanAsync(int id);
        Task<int> CountAsync(int MaHocPhan);

    }
}