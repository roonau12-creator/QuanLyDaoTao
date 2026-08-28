using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLySinhVien.Models
{
    public class SinhVien
    {
        [Key]
        public int MaSinhVien { get; set; }
        public string? MaSoSinhVien { get; set; }
        public string? Hoten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool? GioiTinh { get; set; }
        public string? CanCuocCongDan { get; set; }
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public DateTime? NgayNhapHoc { get; set; }
        [Required]
        public int MaLop { get; set; }
        [ForeignKey("MaLop")]
        [ValidateNever]
        public Lop Lop { get; set; }
        [ValidateNever]
        public string? AnhDaiDien { get; set; }
    }
}