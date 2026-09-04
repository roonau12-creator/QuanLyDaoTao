var hocKyTable;

$(document).ready(function () {

    hocKyTable = $("#tblData").DataTable({
        ajax: "/Admin/HocKy/GetAll",
        columns: [
            {
                data: "tenHocKy",
                width: "20%"
            },
            {
                data: "namHoc",
                width: "15%"
            },
            {
                data: "ngayBatDau",
                width: "20%",
                render: function (data) {
                    if (!data) return "";
                    return new Date(data).toLocaleDateString("vi-VN");
                }
            },
            {
                data: "ngayKetThuc",
                width: "20%",
                render: function (data) {
                    if (!data) return "";
                    return new Date(data).toLocaleDateString("vi-VN");
                }
            },
            {
                data: "maHocKy",
                width: "25%",
                render: function (data) {
                    return `
                        <div class="text-end">
                            <a href="/Admin/HocKy/Upsert/${data}" class="btn btn-primary text-white">
                                Sửa
                            </a>
                            <a onclick="Delete('/Admin/HocKy/Delete/${data}')" class="btn btn-danger text-white">
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
        title: "Bạn có chắc chắn?",
        text: "Bạn sẽ không thể khôi phục dữ liệu này!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Có, xóa!",
        cancelButtonText: "Hủy"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function () {
                    hocKyTable.ajax.reload();
                    Swal.fire({
                        title: "Đã xóa!",
                        text: "Xóa học kỳ thành công.",
                        icon: "success"
                    });
                },
                error: function () {
                    Swal.fire({
                        title: "Lỗi!",
                        text: "Không thể xóa học kỳ.",
                        icon: "error"
                    });
                }
            });
        }
    });
}