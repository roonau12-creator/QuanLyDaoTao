const table = $("#tblData").DataTable({
   ajax:"/Admin/SinhVien/GetAll",
   columns: [
       {data:"maSoSinhVien","width":"15%"},
       {data:"hoten","width":"15%"},
       {data:"ngaySinh","width":"15%",render:function(data)
        {
            var date=new Date(data);
            return date.toLocaleDateString("vi-VN");
        }
       },
       {data:"gioiTinh","width":"15%",render:function(data)
        {
            return data==true?"Nam":"Nữ";
        }
       },
       {data:"canCuocCongDan","width":"15%"},
       {data:"email","width":"15%"},
       {data:"soDienThoai","width":"15%"},
       {data:"diaChi","width":"15%"},
       {data:"ngayNhapHoc","width":"20%",render:function(data)
        {
            var date=new Date(data);
            return date.toLocaleDateString("vi-VN");
        }
       },
       {data:"lop.tenLop","width":"15%"},
       {data:"lop.khoa.tenKhoa","width":"15%"},
       {data:"anhDaiDien","width":"15%",render:function(data) {
            return data ? '<img src="' + data + '" alt="Ảnh đại diện" class="img-thumbnail" style="max-height: 48px">' : "";
       }},
       {data:"maSinhVien","width":"20%",render:function(data)
        {
            return `
                    <div class="text-end">
                        <a href="/Admin/SinhVien/Upsert/${data}" class="btn btn-primary text-white">Sửa</a>
                        <button type="button" class="btn btn-danger btn-delete" data-id="${data}">Xóa</button>
                    </div>
            `
        }
       }
   ]
});

$(document).on("click", ".btn-delete", function () {
    const id = $(this).data("id");
    const xsrfToken = $("input[name='__RequestVerificationToken']").val();
    if (!confirm("Bạn có chắc muốn xóa sinh viên này?")) return;

    $.ajax({
        url: "/Admin/SinhVien/Delete",
        type: "POST",
        data: { id: id },
        headers: { "RequestVerificationToken": xsrfToken },
        success: function (result) {
            if (result.success) {
                table.ajax.reload(null, false);
                alert(result.message);
            } else {
                alert(result.message || "Xóa sinh viên thất bại.");
            }
        },
        error: function () {
            alert("Không thể xóa sinh viên. Vui lòng thử lại.");
        }
    });
});
