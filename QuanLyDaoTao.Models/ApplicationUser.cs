using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDaoTao.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Hoten { get; set; }= string.Empty;
        public int? MaSinhVien { get; set; }
    }
}