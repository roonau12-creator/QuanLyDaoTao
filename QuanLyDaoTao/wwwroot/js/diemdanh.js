var diemDanhDataTable;

$(document).ready(function () {
  LoadDataTable();
});

function LoadDataTable() {
  diemDanhDataTable = $("#tblData").DataTable({
    ajax: {
      url: "/Admin/DiemDanh/GetAll",
      type: "GET",
    },

    columns: [
      {
        data: "maDiemDanh",
        width: "10%",
      },

      {
        data: "maSinhVien",
        width: "12%",
      },

      {
        data: "sinhVien.hoten",
        width: "20%",
      },

      {
        data: "maLichHoc",
        width: "12%",
      },

      {
        data: "trangThai",
        width: "15%",
      },

      {
        data: "ghiChu",
        width: "15%",
      },

      {
        data: "maDiemDanh",
        width: "16%",

        render: function (data) {
          return `
                        <div class="text-center">

                            <a href="/Admin/DiemDanh/Details/${data}"
                               class="btn btn-info btn-sm text-white">
                                Chi tiết
                            </a>
                             <button onclick="Delete('/Admin/DiemDanh/Delete/${data}')"
                        class="btn btn-danger btn-sm">
                    Xóa
                </button>

                            <a href="/Admin/DiemDanh/Upsert/${data}"
                               class="btn btn-success btn-sm">
                                Sửa
                            </a>

                        </div>
                    `;
        },
      },
    ],

    language: {
      emptyTable: "Không có dữ liệu điểm danh",

      zeroRecords: "Không tìm thấy dữ liệu",

      search: "Tìm kiếm:",

      lengthMenu: "Hiển thị _MENU_ dòng",

      info: "Hiển thị _START_ đến _END_ của _TOTAL_ dòng",

      paginate: {
        first: "Đầu",

        last: "Cuối",

        next: "Sau",

        previous: "Trước",
      },
    },
  });

  // Tìm kiếm
  $("#txtSearch").on("keyup", function () {
    diemDanhDataTable.search(this.value).draw();
  });

  // Lọc trạng thái
  $("#filterTrangThai").on("change", function () {
    diemDanhDataTable.column(4).search(this.value).draw();
  });
}
function Delete(url) {
  Swal.fire({
    title: "Bạn có chắc chắn?",
    text: "Dữ liệu điểm danh sẽ bị xóa!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Có, xóa!",
    cancelButtonText: "Hủy",
  }).then(function (result) {
    if (result.isConfirmed) {
      $.ajax({
        url: url,
        type: "DELETE",

        success: function (data) {
          if (data.success) {
            diemDanhDataTable.ajax.reload();

            Swal.fire({
              title: "Đã xóa!",
              text: data.message,
              icon: "success",
            });
          } else {
            Swal.fire({
              title: "Lỗi!",
              text: data.message,
              icon: "error",
            });
          }
        },

        error: function () {
          Swal.fire({
            title: "Lỗi!",
            text: "Không thể xóa điểm danh.",
            icon: "error",
          });
        },
      });
    }
  });
}
