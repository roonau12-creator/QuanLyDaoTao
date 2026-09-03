using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao.Business.Services
{
    public class DiemService : IDiemService
    {
        private readonly ApplicationDbContext _db;
        public DiemService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Diem> CreateDiemAsync(Diem diem)
        {
            _db.diems.Add(diem);
            await _db.SaveChangesAsync();
            return diem;
        }

        public Task DeleteDiemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Diem>> GetAllDiemAsync(bool includeDangKyHocPhan = false)
        {
            if (includeDangKyHocPhan)
            {
                return await _db.diems.Include(p => p.DangKyHocPhan).ThenInclude(p => p.SinhVien).Include(p => p.DangKyHocPhan).ThenInclude(p => p.HocPhan).Include(p => p.DangKyHocPhan).ThenInclude(p => p.HocKy).OrderByDescending(p => p.MaDiem).ToListAsync();
            }
            return await _db.diems.ToListAsync();
        }

        public async Task<Diem?> GetDiemByIdAsync(int id, bool includeDangKyHocPhan = false)
        {
            if (includeDangKyHocPhan)
            {
                return await _db.diems
                    .Include(p => p.DangKyHocPhan).ThenInclude(p => p.SinhVien)
                    .Include(p => p.DangKyHocPhan).ThenInclude(p => p.HocPhan)
                    .Include(p => p.DangKyHocPhan).ThenInclude(p => p.HocKy)
                    .FirstOrDefaultAsync(p => p.MaDiem == id);
            }
            return await _db.diems.FirstOrDefaultAsync(p => p.MaDiem == id);
        }

        public Task<IEnumerable<Diem>> SearchDiemAsync(string keyword, bool includeDangKyHocPhan = false)
        {
            throw new NotImplementedException();
        }

        public async Task<Diem> TinhDiemTongKetAsync(int maDiem)
        {
            var diem = await _db.diems
                .FirstOrDefaultAsync(x => x.MaDiem == maDiem);

            if (diem == null)
            {
                throw new Exception("Không tìm thấy điểm.");
            }

            decimal diemChuyenCan = diem.DiemChuyenCan ?? 0;
            decimal diemNhanXet = diem.DiemNhanXet ?? 0;
            decimal diemGiuaKy = diem.DiemGiuaKy ?? 0;
            decimal diemCuoiKy = diem.DiemCuoiKy ?? 0;

            // Tính điểm tổng kết
            diem.DiemTongKet =
                (diemChuyenCan * 0.1m) +
                (diemNhanXet * 0.1m) +
                (diemGiuaKy * 0.2m) +
                (diemCuoiKy * 0.6m);

            // Làm tròn 2 chữ số
            diem.DiemTongKet = Math.Round(
                diem.DiemTongKet.Value,
                2
            );

            // Xếp loại
            if (diem.DiemTongKet >= 8.5m)
            {
                diem.KetQua = "Giỏi";
            }
            else if (diem.DiemTongKet >= 7.0m)
            {
                diem.KetQua = "Khá";
            }
            else if (diem.DiemTongKet >= 5.0m)
            {
                diem.KetQua = "Trung bình";
            }
            else if (diem.DiemTongKet >= 4.0m)
            {
                diem.KetQua = "Yếu";
            }
            else
            {
                diem.KetQua = "Kém";
            }

            await _db.SaveChangesAsync();

            return diem;
        }
        public async Task<Diem> UpdateDiemAsync(Diem diem)
        {
            _db.diems.Update(diem);
            await _db.SaveChangesAsync();
            return diem;
        }
    }
}