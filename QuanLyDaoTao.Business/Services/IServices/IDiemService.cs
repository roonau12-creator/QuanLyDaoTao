using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface IDiemService
    {
        Task<IEnumerable<Diem>> GetAllDiemAsync(bool includeDangKyHocPhan = false);
        Task<Diem?> GetDiemByIdAsync(int id, bool includeDangKyHocPhan = false);
        Task<Diem> CreateDiemAsync(Diem diem);
        Task<Diem> UpdateDiemAsync(Diem diem);
        Task DeleteDiemAsync(int id);
        Task<IEnumerable<Diem>> SearchDiemAsync(string keyword, bool includeDangKyHocPhan = false);
        Task<Diem> TinhDiemTongKetAsync(int maDiem);

    }
}