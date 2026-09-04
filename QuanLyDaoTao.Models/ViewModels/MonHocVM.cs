using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuanLyDaoTao.Models.ViewModels
{
    public class MonHocVM
    {
        public MonHoc MonHoc { get; set; } = new();
        [ValidateNever]
        public IEnumerable<SelectListItem> KhoaList { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
