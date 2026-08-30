using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;
namespace QuanLyDaoTao.Business;

public interface IKhoaService
{
    Task<List<Khoa>> GetAllKhoasAsync();
    Task<Khoa?> GetKhoaByIdAsync(int id);
    Task CreateKhoaAsync(Khoa khoa);
    Task UpdateKhoaAsync(Khoa khoa);
    Task DeleteKhoaAsync(int id);
    Task<(int LopCount, int MonHocCount)> GetRelatedCountsAsync(int khoaId);

}
