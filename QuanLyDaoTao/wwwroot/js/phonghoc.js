var phongHocDataTable;

$(document).ready(function () {

    LoadDataTable();

});


function LoadDataTable() {

    phongHocDataTable = $("#tblData").DataTable({

        ajax: {
            url: "/Admin/HocPhong/GetAll",
            type: "GET"
        },

        columns: [

            {
                data: "maPhongHoc",
                width: "10%"
            },

            {
                data: "tenPhong",
                width: "20%"
            },

            {
                data: "toaNha",
                width: "20%"
            },

            {
                data: "sucChua",
                width: "15%"
            },

            {
                data: "trangThai",
                width: "15%",

                render: function (data) {

                    if (data === true) {

                        return `
                            <span class="badge bg-success">
                                Hoạt động
                            </span>
                        `;

                    }

                    return `
                        <span class="badge bg-danger">
                            Không hoạt động
                        </span>
                    `;
                }
            },

            {
                data: "maPhongHoc",
                width: "15%",

                render: function (data) {

                    return `
                        <div class="text-center">

                            <a href="/Admin/HocPhong/Upsert/${data}"
                   class="btn btn-success btn-sm text-white">
                    Sửa
                </a>

                <a onclick="Delete('/Admin/HocPhong/Delete/${data}')"
                   class="btn btn-danger btn-sm text-white">
                    Xóa
                </a>


                        </div>
                    `;
                }
            }

        ],

        language: {

            emptyTable: "Không có dữ liệu phòng học",

            zeroRecords: "Không tìm thấy phòng học",

            search: "Tìm kiếm:",

            lengthMenu: "Hiển thị _MENU_ dòng",

            info: "Hiển thị _START_ đến _END_ của _TOTAL_ dòng",

            paginate: {

                first: "Đầu",

                last: "Cuối",

                next: "Sau",

                previous: "Trước"

            }

        }

    });

}
function Delete(url) {

    Swal.fire({
        title: "Bạn có chắc chắn?",
        text: "Bạn sẽ không thể hoàn tác thao tác này!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Có, xóa!",
        cancelButtonText: "Hủy"
    }).then(function (result) {

        if (result.isConfirmed) {

            $.ajax({

                url: url,
                type: "DELETE",

                success: function (data) {

                    if (data.success) {

                        Swal.fire({
                            title: "Đã xóa!",
                            text: data.message,
                            icon: "success"
                        });

                        phongHocDataTable.ajax.reload();

                    }
                    else {

                        Swal.fire({
                            title: "Lỗi!",
                            text: data.message,
                            icon: "error"
                        });

                    }
                },

                error: function () {

                    Swal.fire({
                        title: "Lỗi!",
                        text: "Không thể xóa phòng học.",
                        icon: "error"
                    });

                }

            });

        }

    });
}