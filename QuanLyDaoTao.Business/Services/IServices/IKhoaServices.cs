using QuanLySinhVien.DataAccess.Data;
using QuanLySinhVien.Models;
namespace QuanLySinhVien.Business;

public interface IKhoaService
{
    Task<List<Khoa>> GetAllKhoasAsync();
    Task<Khoa?> GetKhoaByIdAsync(int id);
    Task CreateKhoaAsync(Khoa khoa);
    Task UpdateKhoaAsync(Khoa khoa);
    Task DeleteKhoaAsync(int id);

}
