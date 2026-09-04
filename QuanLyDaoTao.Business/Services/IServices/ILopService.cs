using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Business.Services.IServices
{
    public interface ILopService
    {
    Task<List<Lop>> GetAllLopAsync(bool includeKhoa = false);
    Task<Lop?> GetLopByIdAsync(int id, bool includeKhoa = false);
    Task CreateLopAsync(Lop lop);
    Task UpdateLopAsync(Lop lop);
    Task DeleteLopAsync(int id);
    Task<int> GetRelatedCountsAsync(int lopId);
    }
}
