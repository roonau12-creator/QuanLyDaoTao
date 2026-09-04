using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models
{
    public class GiangVien
    {
        [Key]
        public int MaGiangVien { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập mã số giảng viên")]
        [MaxLength(20)]
        public string MaSoGiangVien { get; set; } = string.Empty;


        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [MaxLength(100)]
        public string HoTen { get; set; } = string.Empty;


        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public DateTime NgaySinh { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [MaxLength(10)]
        public string GioiTinh { get; set; } = string.Empty;


        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;


        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(15)]
        public string? SoDienThoai { get; set; }


        [MaxLength(200)]
        public string? DiaChi { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập học vị")]
        [MaxLength(50)]
        public string HocVi { get; set; } = string.Empty;


        [MaxLength(100)]
        public string? ChucVu { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn khoa")]
        public int MaKhoa { get; set; }


        [ForeignKey("MaKhoa")]
        [ValidateNever]
        public Khoa Khoa { get; set; } = null!;


        public bool TrangThai { get; set; } = true;

    }
}