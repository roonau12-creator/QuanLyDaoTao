using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDaoTao.Models.ViewModels
{
    public class ThongKeChuyenCanVM
    {
        public int MaSinhVien { get; set; }

        public string HoTen { get; set; } = string.Empty;

        public int TongSoBuoi { get; set; }

        public int SoBuoiCoMat { get; set; }

        public int SoBuoiVang { get; set; }

        public int SoBuoiDiTre { get; set; }

        public int SoBuoiCoPhep { get; set; }

        public double TyLeChuyenCan { get; set; }
    }
}