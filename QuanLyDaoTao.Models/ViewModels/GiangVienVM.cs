using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuanLyDaoTao.Models.ViewModels;

public class GiangVienVM
{
    public GiangVien GiangVien { get; set; } = new();
    [ValidateNever]
    public IEnumerable<SelectListItem> DanhSachKhoa { get; set; } = new List<SelectListItem>();
}