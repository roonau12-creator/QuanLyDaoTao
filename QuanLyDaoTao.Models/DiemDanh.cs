using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models
{
    public class DiemDanh
{
    [Key]
    public int MaDiemDanh { get; set; }

    [Required(ErrorMessage = "Lịch học không được để trống")]
    public int MaLichHoc { get; set; }
    [ValidateNever]
    [ForeignKey("MaLichHoc")]
    public LichHoc LichHoc { get; set; } = null!;

    [Required(ErrorMessage = "Sinh viên không được để trống")]
    public int MaSinhVien { get; set; }
    [ValidateNever]
    [ForeignKey("MaSinhVien")]
    public SinhVien SinhVien { get; set; } = null!;

    [Required(ErrorMessage = "Trạng thái không được để trống")]
    [MaxLength(50)]
    public string TrangThai { get; set; } = "Có mặt";

    public string? GhiChu { get; set; }
}
}