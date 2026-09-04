using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;
namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface IDiemDanhService
    {
        Task<IEnumerable<DiemDanh>> GetAllDiemDanhAsync(bool includeSinhVien = false, bool includeLichHoc = false);
        Task<DiemDanh?> GetDiemDanhByIdAsync(int id, bool includeSinhVien = false, bool includeLichHoc = false);
        Task<DiemDanh> CreateDiemDanhAsync(DiemDanh diemDanh);
        Task<DiemDanh> UpdateDiemDanhAsync(DiemDanh diemDanh);
        Task DeleteDiemDanhAsync(int id);
        Task<IEnumerable<DiemDanh>> HistoryDiemDanhAsync(int maSinhVien);
        Task<IEnumerable<ThongKeChuyenCanVM>> ThongKeChuyenCanAsync(int MaSinhVien);
    }
}