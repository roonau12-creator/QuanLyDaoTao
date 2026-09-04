var thongBaoDataTable;

$(document).ready(function () {

    LoadDataTable();

});


function LoadDataTable() {

    thongBaoDataTable = $("#tblData").DataTable({

        ajax: {
            url: "/Admin/ThongBao/GetAll",
            type: "GET"
        },

        columns: [

            {
                data: "maThongBao",
                width: "8%"
            },

            {
                data: "tieuDe",
                width: "25%"
            },

            {
                data: "loaiNoiDung",
                width: "15%"
            },

            {
                data: "ngayDang",
                width: "12%"
            },

            {
                data: "ngayHetHan",
                width: "12%"
            },

            {
                data: "nguoiDang",
                width: "15%"
            },

            {
                data: "maThongBao",
                width: "18%",

                render: function (data) {

                    return `
                        <div class="text-center">

                            <a href="/Admin/ThongBao/Details/${data}"
                               class="btn btn-info btn-sm text-white">
                                Chi tiết
                            </a>

                            <a href="/Admin/ThongBao/Upsert/${data}"
                               class="btn btn-success btn-sm">
                                Sửa
                            </a>

                            <button onclick="Delete('/Admin/ThongBao/Delete/${data}')"
                                    class="btn btn-danger btn-sm">
                                Xóa
                            </button>

                        </div>
                    `;
                }
            }

        ],

        language: {

            emptyTable: "Không có thông báo",

            zeroRecords: "Không tìm thấy thông báo",

            search: "Tìm kiếm:",

            lengthMenu: "Hiển thị _MENU_ dòng",

            info: "Hiển thị _START_ đến _END_ của _TOTAL_ thông báo",

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
        text: "Thông báo sẽ bị xóa và không thể khôi phục!",
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

                        thongBaoDataTable.ajax.reload();

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
                        text: "Có lỗi xảy ra khi xóa thông báo.",
                        icon: "error"
                    });

                }

            });

        }

    });
}