$(function () {
    var tableEl = $("#tblClass");
    var xsrfToken = tableEl.data("xsrf") || "";

    if (tableEl.length === 0) return;

    var table = tableEl.DataTable({
        language: {
            emptyTable: "Chưa có dữ liệu",
            info: "Hiển thị _START_ đến _END_ của _TOTAL_ bản ghi",
            infoEmpty: "Hiển thị 0 đến 0 của 0 bản ghi",
            lengthMenu: "Hiển thị _MENU_ bản ghi",
            search: "Tìm kiếm:",
            zeroRecords: "Không tìm thấy bản ghi phù hợp",
            paginate: {
                first: "Đầu",
                last: "Cuối",
                next: "Sau",
                previous: "Trước"
            }
        },
        columnDefs: [
            { orderable: false, targets: 3 }
        ],
        order: [[0, "asc"]]
    });

    $("#classSearch").on("keyup", function () {
        table.search(this.value).draw();
    });

    function showToast(type, message) {
        if (typeof toastr !== "undefined") {
            if (toastr.options) {
                toastr.options.closeButton = true;
                toastr.options.progressBar = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.options.timeOut = 4000;
            }
            if (type === "success") toastr.success(message);
            else if (type === "error") toastr.error(message);
            else toastr.info(message);
        } else {
            alert(message);
        }
    }

    $(document).on("click", ".btn-delete-class", function () {
        var button = $(this);
        var row = table.row(button.closest("tr"));
        var id = button.data("id");
        var name = button.data("name") || ("mã " + id);

        if (id === undefined || id === null || id === "") {
            showToast("error", "Mã lớp không xác định, không thể xóa.");
            return;
        }

        if (!confirm("Bạn có chắc muốn xóa lớp \"" + name + "\" không?")) {
            return;
        }

        $.ajax({
            url: "/Admin/Lop/Delete",
            type: "POST",
            data: { id: id },
            headers: {
                "RequestVerificationToken": xsrfToken
            },
            success: function (res) {
                if (res && res.success) {
                    if (row.length) row.remove().draw(false);
                    showToast("success", res.message || "Xóa thành công.");
                } else {
                    showToast("error", (res && res.message) ? res.message : "Xóa thất bại.");
                }
            },
            error: function (xhr) {
                var msg = "Lỗi kết nối khi xóa.";
                if (xhr && xhr.status === 400) msg = "Lỗi bảo mật (mã xác thực không hợp lệ). Hãy tải lại trang và thử lại.";
                else if (xhr && xhr.status === 500) msg = "Lỗi máy chủ khi xóa lớp.";
                else if (xhr && xhr.responseJSON && xhr.responseJSON.message) msg = xhr.responseJSON.message;
                showToast("error", msg);
            }
        });
    });
});
