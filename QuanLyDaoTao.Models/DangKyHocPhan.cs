using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models
{
    public class DangKyHocPhan
    {
        [Key]
        public int MaDangKy { get; set; }


        [Required(ErrorMessage = "Sinh viên không được để trống")]
        public int MaSinhVien { get; set; }
        [ValidateNever]
        [ForeignKey("MaSinhVien")]
        public SinhVien SinhVien { get; set; } = null!;


        [Required(ErrorMessage = "Học phần không được để trống")]
        public int MaHocPhan { get; set; }
        [ValidateNever]
        [ForeignKey("MaHocPhan")]
        public HocPhan HocPhan { get; set; } = null!;

        public int MaHocKy { get; set; }
        [ValidateNever]
        [ForeignKey("MaHocKy")]
        public HocKy HocKy { get; set; } = null!;
        [Required(ErrorMessage = "Ngày đăng ký không được để trống")]
        public DateTime NgayDangKy { get; set; } = DateTime.UtcNow;


        [Required]
        [MaxLength(30)]
        public string TrangThai { get; set; } = "Đã đăng ký";


        [MaxLength(500)]
        public string? GhiChu { get; set; }
    }
}