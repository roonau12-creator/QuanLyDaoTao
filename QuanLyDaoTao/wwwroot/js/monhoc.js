var monhocTable;

$(document).ready(function () {

    monhocTable = $("#tblData").DataTable({
        ajax: "/Admin/MonHoc/GetAll",

        columns: [
            {
                data: "tenMonHoc",
                width: "15%"
            },
            {
                data: "soTinChi",
                width: "15%"
            },
            {
                data: "khoa.tenKhoa",
                width: "15%"
            },
            {
                data: "moTa",
                width: "15%"
            },
            {
                data: "maMonHoc",
                width: "20%",
                render: function (data) {
                    return `
                        <div class="text-end">
                            <a href="/Admin/MonHoc/Upsert/${data}" 
                               class="btn btn-primary text-white">
                                Sửa
                            </a>

                            <a onclick="Delete('/Admin/MonHoc/Delete/${data}')" 
                               class="btn btn-warning text-white">
                                Xóa
                            </a>
                        </div>
                    `;
                }
            }
        ]
    });

});
function Delete(url) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then(function (result) {

        if (result.isConfirmed) {

            $.ajax({
                url: url,
                type: "DELETE",

                success: function (data) {

                    monhocTable.ajax.reload();

                    Swal.fire({
                        title: "Deleted!",
                        text: "Your file has been deleted.",
                        icon: "success"
                    });

                },

                error: function (xhr) {

                    Swal.fire({
                        title: "Error!",
                        text: "Không thể xóa môn học.",
                        icon: "error"
                    });

                    console.log(xhr);
                }
            });

        }

    });

}