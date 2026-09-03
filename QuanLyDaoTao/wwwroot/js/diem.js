var diemDataTable;

$(document).ready(function () {

    diemDataTable = $("#tblData").DataTable({

        ajax: "/Admin/Diem/GetAll",

        columns: [

            {
                data: "maDiem",
                width: "8%"
            },

            {
                data: "dangKyHocPhan",
                render: function (data) {

                    return data.maSinhVien;
                }
            },

            {
                data: "dangKyHocPhan",
                render: function (data) {

                    return data.sinhVien.hoten;
                }
            },

            {
                data: "dangKyHocPhan",
                render: function (data) {

                    return data.hocPhan.tenHocPhan;
                }
            },

            {
                data: "diemChuyenCan"
            },

            {
                data: "diemNhanXet"
            },

            {
                data: "diemGiuaKy"
            },

            {
                data: "diemCuoiKy"
            },

            {
                data: "diemTongKet"
            },

            {
                data: "ketQua"
            },

            {
                data: "maDiem",
                render: function (data) {

                    return `
                        <div class="text-end">

                            <a href="/Admin/Diem/Upsert/${data}"
                               class="btn btn-success btn-sm">
                                Sửa
                            </a>

                            <a onclick="Delete('/Admin/Diem/Delete/${data}')"
                               class="btn btn-danger btn-sm text-white">
                                Xóa
                            </a>

                        </div>
                    `;
                }
            }

        ],

        language: {
            search: "Tìm kiếm:",
            lengthMenu: "Hiển thị _MENU_ dòng",
            info: "Hiển thị _START_ đến _END_ trong tổng số _TOTAL_ dòng",
            emptyTable: "Chưa có dữ liệu điểm",
            paginate: {
                first: "Đầu",
                last: "Cuối",
                next: "Sau",
                previous: "Trước"
            }
        }

    });

});
function Delete(url) {

    Swal.fire({
        title: "Bạn có chắc muốn xóa?",
        text: "Điểm sẽ bị xóa khỏi hệ thống!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#6c757d",
        confirmButtonText: "Xóa",
        cancelButtonText: "Hủy"
    }).then((result) => {

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

                        diemDataTable.ajax.reload();
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
                        text: "Không thể xóa điểm.",
                        icon: "error"
                    });

                }
            });

        }

    });
}