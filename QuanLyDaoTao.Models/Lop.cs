using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models;

public class Lop
{
    [Key]
    public int MaLop { get; set; }

    [Required(ErrorMessage = "Tên lớp không được để trống.")]
    [StringLength(100, ErrorMessage = "Tên lớp không quá 100 ký tự.")]
    public string TenLop { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mã khoa không được để trống.")]
    public int MaKhoa { get; set; }

    [ForeignKey(nameof(MaKhoa))]
    [ValidateNever]
    public Khoa? Khoa { get; set; }
}
