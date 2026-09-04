using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models
{
    public class Diem
{
    [Key]
    public int MaDiem { get; set; }

    [Required(ErrorMessage = "Đăng ký học phần không được để trống")]
    public int MaDangKy { get; set; }
    [ValidateNever]
    [ForeignKey("MaDangKy")]
    public DangKyHocPhan DangKyHocPhan { get; set; } = null!;

    [Range(0, 10, ErrorMessage = "Điểm phải từ 0 đến 10")]
    public decimal? DiemChuyenCan { get; set; }

    [Range(0, 10, ErrorMessage = "Điểm phải từ 0 đến 10")]
    public decimal? DiemNhanXet { get; set; }

    [Range(0, 10, ErrorMessage = "Điểm phải từ 0 đến 10")]
    public decimal? DiemGiuaKy { get; set; }

    [Range(0, 10, ErrorMessage = "Điểm phải từ 0 đến 10")]
    public decimal? DiemCuoiKy { get; set; }

    [Range(0, 10, ErrorMessage = "Điểm phải từ 0 đến 10")]
    public decimal? DiemTongKet { get; set; }

    [MaxLength(255)]
    public string? KetQua { get; set; }
}
}