using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;
namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface IHocKyService
    {
        Task<IEnumerable<HocKy>> GetAllHocKyAsync();
        Task<HocKy?> GetHocKyByIdAsync(int id);
        Task CreateHocKyAsync(HocKy hocKy);
        Task UpdateHocKyAsync(HocKy hocKy);
        Task DeleteHocKyAsync(int id);
    }
}