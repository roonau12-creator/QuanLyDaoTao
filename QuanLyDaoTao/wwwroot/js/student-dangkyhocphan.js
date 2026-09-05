var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        ajax: {
            url: "/Student/DangKyHocPhan/GetHocPhanMoDangKy",
            dataSrc: "data"
        },
        columns: [
            { data: "maHocPhanCode", width: "10%" },
            { data: "tenHocPhan", width: "20%" },
            { data: "soTinChi", width: "8%" },
            {
                data: "giangVien",
                render: function (data) {
                    return data ? data.hoTen : "-";
                },
                width: "15%"
            },
            {
                data: "hocKy",
                render: function (data) {
                    return data ? data.tenHocKy : "-";
                },
                width: "15%"
            },
            { data: "siSoToiDa", width: "8%" },
            { data: "trangThai", width: "10%" },
            {
                data: "maHocPhan",
                render: function (data) {
                    return `<a href="/Student/DangKyHocPhan/DangKy/${data}" class="btn btn-primary btn-sm"><i class="bi bi-plus-circle"></i> Đăng ký</a>`;
                },
                width: "14%"
            }
        ],
        language: {
            emptyTable: "Không có học phần nào đang mở đăng ký",
            zeroRecords: "Không tìm thấy",
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
