using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface IGiangVienService
    {
        Task<IEnumerable<GiangVien>> GetAllGiangVienAsync(bool includeKhoa = false);
        Task<GiangVien?> GetGiangVienByIdAsync(int id, bool includeKhoa = false);
        Task<GiangVien> CreateGiangVienAsync(GiangVien giangVien);
        Task<GiangVien> UpdateGiangVienAsync(GiangVien giangVien);
        Task DeleteGiangVienAsync(int id);
    }
}