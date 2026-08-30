using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models
{
    public class SinhVien
    {
        [Key]
        public int MaSinhVien { get; set; }
        [Required]
        [Display(Name="Mã số sinh viên")]
        public string? MaSoSinhVien { get; set; }
        [Required]
        [Display(Name="Họ và tên")]
        public string? Hoten { get; set; }
        [Required]
        [Display(Name="Ngày sinh")]
        public DateTime? NgaySinh { get; set; }
        [Required]
        [Display(Name="Giới tính")]
        public bool? GioiTinh { get; set; }
        [Required]
        [Display(Name="Giới tính")]
        public string? CanCuocCongDan { get; set; }
        [Required]
        [Display(Name="Căn cước công dân")]
        public string? Email { get; set; }
        [Required]
        [Display(Name="Email")]
        public string? SoDienThoai { get; set; }
        [Required]
        [Display(Name="Địa chỉ")]
        public string? DiaChi { get; set; }
        [Required]  
        [Display(Name="Ngày nhập học")]
        public DateTime? NgayNhapHoc { get; set; }
        [Required]
        public int MaLop { get; set; }
        [ForeignKey("MaLop")]
        [ValidateNever]
        public Lop? Lop { get; set; }
        [ValidateNever]
        public string? AnhDaiDien { get; set; }
    }
}