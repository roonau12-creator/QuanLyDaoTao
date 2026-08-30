$("#tblData").DataTable({
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
       {data:"anhDaiDien","width":"15%"},
       {data:null,defaultContent:""},
       {data:"maSinhVien","width":"20%",render:function(data)
        {
            return `
                    <div class="text-end">
                        <a href="/Admin/SinhVien/Upsert/${data}" class="btn btn-primary text-white">Sửa</a>
                        <a onclick="Delete('/Admin/SinhVien/Delete/${data}')" class="btn btn-success text-white">Xóa</a>
                    </div>
            `
        }
       }
   ] 
});