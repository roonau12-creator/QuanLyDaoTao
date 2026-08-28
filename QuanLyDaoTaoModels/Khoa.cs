using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models;

public class Khoa
{
    [Key]
    public int MaKhoa { get; set; }
    [Required]
    public string TenKhoa { get; set; } = string.Empty;
    [Required]
    public string MoTa { get; set; } = string.Empty;
}
