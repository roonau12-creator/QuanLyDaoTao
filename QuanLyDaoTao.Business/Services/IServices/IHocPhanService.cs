using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;
namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface IHocPhanService
    {
        Task<IEnumerable<HocPhan>> GetAllHocPhansAsync(bool includeMonHoc = false, bool includeGiangVien = false, bool includeHocKy = false);
        Task<HocPhan?> GetHocPhanByIdAsync(int id, bool includeMonHoc = false, bool includeGiangVien = false, bool includeHocKy = false);
        Task<HocPhan> CreateHocPhanAsync(HocPhan hocPhan);
        Task UpdateHocPhanAsync(HocPhan hocPhan);
        Task DeleteHocPhanAsync(int id);

    }
}