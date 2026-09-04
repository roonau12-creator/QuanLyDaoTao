using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDaoTao.Models.ViewModels
{
    public class HocPhanDetailsVM
    {
        public int MaHocPhan { get; set; }

        public string TenHocPhan { get; set; } = string.Empty;

        public int SoTinChi { get; set; }

        public string TenMonHoc { get; set; } = string.Empty;

        public string TenHocKy { get; set; } = string.Empty;

        public int SiSoToiDa { get; set; }

        public int SoLuongDaDangKy { get; set; }

        public string TrangThai { get; set; } = string.Empty;

        public string? MoTa { get; set; }

        public List<LichHoc> LichHocs { get; set; }
            = new List<LichHoc>();
    }
}