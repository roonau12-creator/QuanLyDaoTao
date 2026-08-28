using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Threading.Tasks;

namespace QuanLyDaoTao.Models
{
    public class Lop
    {
        [Key]
        public int MaLop { get; set; }
        [Required]
        public string TenLop { get; set; } = string.Empty;
        [Required]
        public int MaKhoa { get; set; }
        [ForeignKey("MaKhoa")]
        [ValidateNever]
        public Khoa khoa { get; set; }
    }
}