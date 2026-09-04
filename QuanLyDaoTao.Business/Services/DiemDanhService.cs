using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Business.Services.IServices;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Models;
using QuanLyDaoTao.Models.ViewModels;
namespace QuanLyDaoTao.Business.Services
{
    public class DiemDanhService : IDiemDanhService
    {
        private readonly ApplicationDbContext _context;
        public DiemDanhService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DiemDanh> CreateDiemDanhAsync(DiemDanh diemDanh)
        {
            _context.diemDanhs.Add(diemDanh);
            await _context.SaveChangesAsync();
            return diemDanh;
        }

        public async Task DeleteDiemDanhAsync(int id)
        {
            var diemDanh = await _context.diemDanhs.FindAsync(id);
            if (diemDanh != null)
            {
                _context.diemDanhs.Remove(diemDanh);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DiemDanh>> GetAllDiemDanhAsync(bool includeSinhVien = false, bool includeLichHoc = false)
        {
            if (includeSinhVien && includeLichHoc)
            {
                return await _context.diemDanhs
                    .Include(d => d.SinhVien)
                    .Include(d => d.LichHoc)
                    .ToListAsync();
            }
            else if (includeSinhVien)
            {
                return await _context.diemDanhs.Include(d => d.SinhVien).ToListAsync();
            }
            else if (includeLichHoc)
            {
                return await _context.diemDanhs.Include(d => d.LichHoc).ToListAsync();
            }
            else
            {
                return await _context.diemDanhs.ToListAsync();
            }
        }
        public async Task<DiemDanh?> GetDiemDanhByIdAsync(int id, bool includeSinhVien = false, bool includeLichHoc = false)
        {
            if (includeSinhVien && includeLichHoc)
            {
                return await _context.diemDanhs.Include(d => d.SinhVien).Include(d => d.LichHoc).FirstOrDefaultAsync(d => d.MaDiemDanh == id);
            }
            else if (includeSinhVien)
            {
                return await _context.diemDanhs.Include(d => d.SinhVien).FirstOrDefaultAsync(d => d.MaDiemDanh == id);
            }
            else
            {
                return await _context.diemDanhs.FirstOrDefaultAsync(d => d.MaDiemDanh == id);
            }
        }

        public async Task<IEnumerable<DiemDanh>> HistoryDiemDanhAsync(int maSinhVien)
        {
            return await _context.diemDanhs
                .Include(x => x.SinhVien)
                .Include(x => x.LichHoc)
                .Where(x => x.MaSinhVien == maSinhVien)
                .OrderByDescending(x => x.MaDiemDanh)
                .ToListAsync();
        }

        public async Task<IEnumerable<ThongKeChuyenCanVM>> ThongKeChuyenCanAsync(int MaSinhVien)
        {
            var result = new List<ThongKeChuyenCanVM>();

            var sinhVien = await _context.sinhViens
                .FirstOrDefaultAsync(x => x.MaSinhVien == MaSinhVien);

            if (sinhVien == null)
            {
                return result;
            }

            var diemDanhs = await _context.diemDanhs
                .Where(x => x.MaSinhVien == MaSinhVien)
                .ToListAsync();

            int tongSoBuoi = diemDanhs.Count;

            int soBuoiCoMat = diemDanhs.Count(
                x => x.TrangThai == "Có mặt");

            int soBuoiVang = diemDanhs.Count(
                x => x.TrangThai == "Vắng");

            int soBuoiDiTre = diemDanhs.Count(
                x => x.TrangThai == "Đi trễ");

            int soBuoiCoPhep = diemDanhs.Count(
                x => x.TrangThai == "Có phép");

            double tyLeChuyenCan = 0;

            if (tongSoBuoi > 0)
            {
                tyLeChuyenCan =
                    (double)(soBuoiCoMat + soBuoiCoPhep)
                    / tongSoBuoi * 100;
            }

            result.Add(new ThongKeChuyenCanVM
            {
                MaSinhVien = sinhVien.MaSinhVien,
                HoTen = sinhVien.Hoten,

                TongSoBuoi = tongSoBuoi,

                SoBuoiCoMat = soBuoiCoMat,

                SoBuoiVang = soBuoiVang,

                SoBuoiDiTre = soBuoiDiTre,

                SoBuoiCoPhep = soBuoiCoPhep,

                TyLeChuyenCan = tyLeChuyenCan
            });

            return result;
        }

        public async Task<DiemDanh> UpdateDiemDanhAsync(DiemDanh diemDanh)
        {
            _context.diemDanhs.Update(diemDanh);
            await _context.SaveChangesAsync();
            return diemDanh;
        }
    }
}