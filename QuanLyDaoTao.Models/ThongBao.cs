using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDaoTao.Models
{
    public class ThongBao
    {
        [Key]
        public int MaThongBao { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(255)]
        public string TieuDe { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string NoiDung { get; set; } = string.Empty;

        [Required(ErrorMessage = "Loại nội dung không được để trống")]
        [MaxLength(100)]
        public string LoaiNoiDung { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày đăng không được để trống")]
        public DateTime NgayDang { get; set; } = DateTime.UtcNow;

        public DateTime? NgayHetHan { get; set; }

        [Required(ErrorMessage = "Người đăng không được để trống")]
        [MaxLength(255)]
        public string NguoiDang { get; set; } = string.Empty;
    }
}