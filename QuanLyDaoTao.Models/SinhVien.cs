using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models
{
    public class SinhVien
    {
        [Key]
        public int MaSinhVien { get; set; }

        [Required]
        [Display(Name = "Mã số sinh viên")]
        public string MaSoSinhVien { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Họ và tên")]
        public string Hoten { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [Required]
        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; }

        [Required]
        [Display(Name = "Căn cước công dân")]
        public string CanCuocCongDan { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Ngày nhập học")]
        public DateTime NgayNhapHoc { get; set; }

        [Required]
        [Display(Name = "Lớp")]
        public int MaLop { get; set; }

        [ForeignKey("MaLop")]
        [ValidateNever]
        public Lop? Lop { get; set; }

        [ValidateNever]
        public string? AnhDaiDien { get; set; }
    }
}
