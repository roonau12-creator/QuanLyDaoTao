using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models.ViewModels
{
    public class DiemDanhVM
    {
        public DiemDanh DiemDanh { get; set; } = new DiemDanh();
        public LichHoc LichHoc { get; set; } = new LichHoc();
        public SinhVien SinhVien { get; set; } = new SinhVien();
        [ValidateNever]
        public IEnumerable<SinhVien> ListSinhVien { get; set; } = new List<SinhVien>();
        [ValidateNever]
        public IEnumerable<LichHoc> ListLichHoc { get; set; } = new List<LichHoc>();
        [ValidateNever]
        public List<DiemDanh> SinhViens { get; set; } = new List<DiemDanh>();
    }
}