using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;
namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface IHocPhongService
    {
        Task<IEnumerable<PhongHoc>> GetAllPhongHocsAsync();
        Task<PhongHoc?> GetPhongHocByIdAsync(int id);
        Task<PhongHoc> CreatePhongHocAsync(PhongHoc phongHoc);
        Task UpdatePhongHocAsync(PhongHoc phongHoc);
        Task DeletePhongHocAsync(int id);
    }
}