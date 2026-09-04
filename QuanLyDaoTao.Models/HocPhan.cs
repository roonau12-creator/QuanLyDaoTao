using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models;

public class HocPhan
{
    [Key]
    public int MaHocPhan { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập mã học phần")]
    [MaxLength(20)]
    public string MaHocPhanCode { get; set; } = string.Empty;


    [Required(ErrorMessage = "Vui lòng chọn môn học")]
    public int MaMonHoc { get; set; }
    [ValidateNever]
    [ForeignKey("MaMonHoc")]
    public MonHoc MonHoc { get; set; } = null!;


    [Required(ErrorMessage = "Vui lòng chọn học kỳ")]
    public int MaHocKy { get; set; }
    [ValidateNever]
    [ForeignKey("MaHocKy")]
    public HocKy HocKy { get; set; } = null!;


    public int? MaGiangVien { get; set; }
    [ValidateNever]
    [ForeignKey("MaGiangVien")]
    public GiangVien? GiangVien { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập sĩ số tối đa")]
    [Range(1, 200, ErrorMessage = "Sĩ số phải từ 1 đến 200")]
    public int SiSoToiDa { get; set; }


    public int SiSoHienTai { get; set; } = 0;


    [Required(ErrorMessage = "Vui lòng chọn ngày mở đăng ký")]
    public DateTime NgayMoDangKy { get; set; }


    [Required(ErrorMessage = "Vui lòng chọn ngày đóng đăng ký")]
    public DateTime NgayDongDangKy { get; set; }


    [Required]
    [MaxLength(30)]
    public string TrangThai { get; set; } = "Đang mở";


    [MaxLength(500)]
    public string? GhiChu { get; set; }

    [NotMapped]
    public string TenHocPhan => $"{MonHoc?.TenMonHoc} ({MaHocPhanCode})";

    [NotMapped]
    public int SoTinChi => MonHoc?.SoTinChi ?? 0;
}