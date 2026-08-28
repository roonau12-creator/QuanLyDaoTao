using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLySinhVien.Models
{
    public class HocKy
    {
        [Key]
        public int MaHocKy { get; set; }
        [Required]
        public string TenHocKy { get; set; }
        public DateTime NamHoc { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

    }
}