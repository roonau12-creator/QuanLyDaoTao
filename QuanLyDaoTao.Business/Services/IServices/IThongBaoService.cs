using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;
namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface IThongBaoService
    {
        Task<IEnumerable<ThongBao>> GetAllThongBaoAsync();
        Task<ThongBao?> GetThongBaoByIdAsync(int id);
        Task<ThongBao> CreateThongBaoAsync(ThongBao thongBao);
        Task<ThongBao> UpdateThongBaoAsync(ThongBao thongBao);
        Task DeleteThongBaoAsync(int id);
    }
}