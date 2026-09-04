var dangKyHocPhanDataTable;

$(document).ready(function () {

    dangKyHocPhanDataTable = $("#tblData").DataTable({

        ajax: {
            url: "/Admin/DangKyHocPhan/GetHocPhanMoDangKy",
            type: "GET"
        },

        columns: [

            {
                data: "maHocPhanCode",
                width: "10%"
            },

            {
                data: "monHoc.tenMonHoc",
                width: "18%"
            },

            {
                data: "monHoc.soTinChi",
                width: "8%"
            },

            {
                data: "giangVien.hoTen",
                width: "18%"
            },

            {
                data: "hocKy.tenHocKy",
                width: "14%"
            },

            {
                data: null,
                width: "10%",
                render: function (data) {
                    return `${data.siSoHienTai}/${data.siSoToiDa}`;
                }
            },

            {
                data: "trangThai",
                width: "10%",
                render: function (data) {
                    return data === "Đang mở"
                        ? `<span class="badge bg-success">Đang mở</span>`
                        : `<span class="badge bg-secondary">${data}</span>`;
                }
            },

            {
                data: "maHocPhan",
                width: "12%",
                render: function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/DangKyHocPhan/DangKy/${data}"
                               class="btn btn-primary btn-sm">
                                Đăng ký
                            </a>
                        </div>
                    `;
                }
            }

        ],

        language: {

            emptyTable: "Không có học phần mở đăng ký",

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

});
