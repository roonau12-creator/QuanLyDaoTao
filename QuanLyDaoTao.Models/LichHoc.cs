using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models
{
    public class LichHoc
    {
        [Key]
        public int MaLichHoc { get; set; }
        [Required]
        public int MaMonHoc { get; set; }
        [ValidateNever]
        [ForeignKey("MaMonHoc")]
        public MonHoc MonHoc { get; set; } = null!;


        [Required]
        public int MaLop { get; set; }
        [ValidateNever]
        [ForeignKey("MaLop")]
        public Lop Lop { get; set; } = null!;


        [Required(ErrorMessage = "Vui lòng chọn thứ")]
        [Range(2, 8, ErrorMessage = "Thứ phải từ 2 đến 8")]
        public int Thu { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn tiết bắt đầu")]
        [Range(1, 15, ErrorMessage = "Tiết phải từ 1 đến 15")]
        public int TietBatDau { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập số tiết")]
        [Range(1, 6, ErrorMessage = "Số tiết phải từ 1 đến 6")]
        public int SoTiet { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
        public DateTime NgayBatDau { get; set; }


        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc")]
        public DateTime NgayKetThuc { get; set; }


        [MaxLength(500)]
        public string? GhiChu { get; set; }
    }
}