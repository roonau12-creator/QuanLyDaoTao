using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDaoTao.Models.ViewModels
{
    public class DangKyHocPhanVM
    {
        public int MaHocPhan { get; set; }

        public string TenHocPhan { get; set; } = string.Empty;

        public string TenMonHoc { get; set; } = string.Empty;

        public int SoTinChi { get; set; }

        public int MaHocKy { get; set; }

        public string TenHocKy { get; set; } = string.Empty;

        public int SiSoToiDa { get; set; }

        public int SoLuongDaDangKy { get; set; }

        public int SoChoConLai { get; set; }
    }
}