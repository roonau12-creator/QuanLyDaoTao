using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTao.Models;

public class Khoa
{
    [Key]
    public int MaKhoa { get; set; }
    [MaxLength(200)]
    [Required]
    public string TenKhoa { get; set; } = string.Empty;
    [MaxLength(500)]
    [Required]
    public string MoTa { get; set; } = string.Empty;
}
