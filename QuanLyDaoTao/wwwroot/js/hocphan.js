var hocPhanDataTable;

$(document).ready(function () {

    LoadDataTable();

});


function LoadDataTable() {

    hocPhanDataTable = $("#tblData").DataTable({

        ajax: {
            url: "/Admin/HocPhan/GetAll",
            type: "GET"
        },

        columns: [

            {
                data: "maHocPhanCode",
                width: "10%"
            },

            {
                data: "monHoc.tenMonHoc",
                width: "15%"
            },

            {
                data: "hocKy.tenHocKy",
                width: "15%"
            },

            {
                data: "giangVien.hoTen",
                width: "15%"
            },

            {
                data: null,
                width: "10%",

                render: function (data) {

                    return `${data.siSoHienTai}/${data.siSoToiDa}`;

                }
            },

            {
                data: "ngayMoDangKy",
                width: "10%"
            },

            {
                data: "ngayDongDangKy",
                width: "10%"
            },

            {
                data: "trangThai",
                width: "10%",

                render: function (data) {

                    if (data === "Đang mở") {

                        return `
                            <span class="badge bg-success">
                                Đang mở
                            </span>
                        `;

                    }

                    if (data === "Đã đóng") {

                        return `
                            <span class="badge bg-secondary">
                                Đã đóng
                            </span>
                        `;

                    }

                    return `
                        <span class="badge bg-warning">
                            ${data}
                        </span>
                    `;
                }
            },

            {
                data: "maHocPhan",
                width: "15%",

                render: function (data) {

                    return `
                        <div class="text-center">

                            <a href="/Admin/HocPhan/Upsert/${data}"
                               class="btn btn-success btn-sm">
                                Sửa
                            </a>

                            <button onclick="Delete('/Admin/HocPhan/Delete/${data}')"
                                    class="btn btn-danger btn-sm">
                                Xóa
                            </button>

                        </div>
                    `;
                }
            }

        ],

        language: {

            emptyTable: "Không có dữ liệu học phần",

            zeroRecords: "Không tìm thấy học phần",

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
        text: "Bạn có muốn xóa học phần này không?",
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

                        hocPhanDataTable.ajax.reload();

                        Swal.fire({
                            title: "Đã xóa!",
                            text: data.message,
                            icon: "success"
                        });

                    }
                    else {

                        Swal.fire({
                            title: "Không thể xóa!",
                            text: data.message,
                            icon: "error"
                        });

                    }
                },

                error: function () {

                    Swal.fire({
                        title: "Lỗi!",
                        text: "Có lỗi xảy ra khi xóa học phần.",
                        icon: "error"
                    });

                }

            });

        }

    });
}