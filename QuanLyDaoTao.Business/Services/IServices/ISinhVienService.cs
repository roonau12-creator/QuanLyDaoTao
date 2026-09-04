using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;
namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface ISinhVienService
    {
        Task<List<SinhVien>> GetAllSinhVien(bool includeLop = false);
        Task<SinhVien?> GetSinhVienByIdAsync(int id, bool includeLop=false);
        Task CreateSinhVienAsync(SinhVien sinhVien);
        Task UpdateSinhVien(SinhVien sinhVien);
        Task DeleteSinhVien(int id);
        
    }
}