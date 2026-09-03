using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTao.Models
{
    public class PhongHoc
    {
        [Key]
        public int MaPhongHoc { get; set; }


        [Required]
        [MaxLength(20)]
        public string TenPhong { get; set; } = string.Empty;


        [MaxLength(100)]
        public string? ToaNha { get; set; }


        [Range(1, 1000)]
        public int SucChua { get; set; }


        [MaxLength(50)]
        public string LoaiPhong { get; set; } = string.Empty;


        [MaxLength(500)]
        public string? MoTa { get; set; }


        public bool TrangThai { get; set; } = true;


    }
}