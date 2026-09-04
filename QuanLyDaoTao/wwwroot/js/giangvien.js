var giangVienDataTable;

$(document).ready(function () {

    LoadDataTable();

});


function LoadDataTable() {

    giangVienDataTable = $("#tblData").DataTable({

        ajax: {
            url: "/Admin/GiangVien/GetAll",
            type: "GET",
            dataSrc: "data"
        },

        columns: [

            {
                data: "maSoGiangVien",
                width: "10%"
            },

            {
                data: "hoTen",
                width: "15%"
            },

            {
                data: "gioiTinh",
                width: "10%"
            },

            {
                data: "email",
                width: "15%"
            },

            {
                data: "soDienThoai",
                width: "12%"
            },

            {
                data: "hocVi",
                width: "10%"
            },

            {
                data: "khoa.tenKhoa",
                width: "15%",
                render: function (data) {
                    return data || "";
                }
            },

            {
                data: "trangThai",
                width: "10%",

                render: function (data) {

                    if (data === true) {

                        return `
                            <span class="badge bg-success">
                                Đang làm việc
                            </span>
                        `;

                    }

                    return `
                        <span class="badge bg-danger">
                            Nghỉ
                        </span>
                    `;
                }
            },

            {

                data: "maGiangVien",

                width: "15%",

                render: function (data) {

                    return `
                        <div class="text-center">

                            <a href="/Admin/GiangVien/Upsert/${data}"
                               class="btn btn-success btn-sm">

                                Sửa

                            </a>

                            <button onclick="Delete('/Admin/GiangVien/Delete/${data}')"
                                    class="btn btn-danger btn-sm">

                                Xóa

                            </button>

                        </div>
                    `;
                }

            }

        ],

        language: {

            emptyTable: "Không có dữ liệu",

            zeroRecords: "Không tìm thấy dữ liệu",

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

        text: "Dữ liệu giảng viên sẽ bị xóa!",

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

                            title: "Thành công!",

                            text: data.message,

                            icon: "success"

                        });

                        giangVienDataTable.ajax.reload();

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

                        text: "Có lỗi xảy ra khi xóa.",

                        icon: "error"

                    });

                }

            });

        }

    });

}