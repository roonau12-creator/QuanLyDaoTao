using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuanLyDaoTao.Models.ViewModels
{
    public class SinhVienVM
    {
        public SinhVien SinhVien { get; set; } = new();
        [ValidateNever]
        public IEnumerable<SelectListItem> LopList { get; set; } = Enumerable.Empty<SelectListItem>();
        [ValidateNever]
        public IEnumerable<SelectListItem>KhoaList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
