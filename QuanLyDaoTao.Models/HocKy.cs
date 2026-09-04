using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDaoTao.Models
{
    public class HocKy : IValidatableObject
{
    [Key]
    public int MaHocKy { get; set; }

    [Required(ErrorMessage = "Tên học kỳ không được để trống")]
    [StringLength(
        200,
        MinimumLength = 2,
        ErrorMessage = "Tên học kỳ phải từ 2 đến 200 ký tự"
    )]
    public string TenHocKy { get; set; } = string.Empty;

    [Required(ErrorMessage = "Năm học không được để trống")]
    [Range(2000, 2100, ErrorMessage = "Năm học phải từ 2000 đến 2100")]
    public int NamHoc { get; set; }

    [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
    [DataType(DataType.Date)]
    public DateTime NgayBatDau { get; set; }

    [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
    [DataType(DataType.Date)]
    public DateTime NgayKetThuc { get; set; }

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (NgayKetThuc < NgayBatDau)
        {
            yield return new ValidationResult(
                "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu",
                new[] { nameof(NgayKetThuc) }
            );
        }
    }
}
}
