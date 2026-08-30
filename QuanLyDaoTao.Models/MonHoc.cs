using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyDaoTao.Models
{
    public class MonHoc
    {
        [Key]
        public int MaMonHoc { get; set; }
        [Required]
        public string TenMonHoc { get; set; } = string.Empty;
        public int SoTinChi { get; set; }
        [Required]
        public int MaKhoa { get; set; }
        [ForeignKey("MaKhoa")]
        [ValidateNever]
        public Khoa? Khoa { get; set; }
        public string MoTa { get; set; } = string.Empty;
    }
}