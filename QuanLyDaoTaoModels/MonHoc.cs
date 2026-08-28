using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLySinhVien.Models
{
    public class MonHoc
    {
        [Key]
        public int MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public string SoTinChi { get; set; }
        [Required]
        public int MaKhoa { get; set; }
        [ForeignKey("MaKhoa")]
        [ValidateNever]
        public Khoa khoa { get; set; }
        public string MoTa { get; set; }
    }
}