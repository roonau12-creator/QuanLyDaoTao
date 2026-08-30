$(function () {
    "use strict";

    // =========================================================
    // 1. CONFIGURATION
    // =========================================================
    const SELECTORS = {
        table:     "#tblData",
        search:    "#facultySearch",
        deleteBtn: ".btn-delete"
    };

    const $table = $(SELECTORS.table);

    const API = {
        // Ưu tiên lấy URL từ data-attribute, fallback sang URL mặc định
        deleteUrl: $table.data("delete-url") || "/Admin/Khoa/Delete"
    };

    const xsrfToken = $table.data("xsrf") || "";

    // =========================================================
    // 2. TOAST HELPER (tránh lặp code và cấu hình một lần)
    // =========================================================
    const Toast = {
        _configure: function () {
            if (typeof toastr === "undefined") return;
            toastr.options = {
                closeButton: true,
                progressBar: true,
                positionClass: "toast-top-right",
                timeOut: 4000,
                preventDuplicates: true
            };
        },
        show: function (type, message) {
            if (typeof toastr === "undefined") {
                alert(message);
                return;
            }
            this._configure();
            if (type === "success") toastr.success(message);
            else if (type === "error") toastr.error(message);
            else toastr.info(message);
        },
        success: (msg) => Toast.show("success", msg),
        error:   (msg) => Toast.show("error",   msg),
        info:    (msg) => Toast.show("info",    msg)
    };

    // =========================================================
    // 3. DATA TABLE
    // =========================================================
    const table = $table.DataTable({
        language: {
            emptyTable:  "Chưa có dữ liệu",
            info:        "Hiển thị _START_ đến _END_ của _TOTAL_ bản ghi",
            infoEmpty:   "Hiển thị 0 đến 0 của 0 bản ghi",
            lengthMenu:  "Hiển thị _MENU_ bản ghi",
            search:      "Tìm kiếm:",
            zeroRecords: "Không tìm thấy bản ghi phù hợp",
            paginate: {
                first: "Đầu", last: "Cuối",
                next: "Sau",  previous: "Trước"
            }
        },
        columnDefs: [
            // Cột action (cột cuối) không cho sort
            { orderable: false, targets: -1 }
        ],
        order: [[0, "asc"]]
    });

    // Debounced search — tránh gọi search liên tục khi người dùng gõ
    let searchTimer;
    $(SELECTORS.search).on("keyup", function () {
        const value = $(this).val();
        clearTimeout(searchTimer);
        searchTimer = setTimeout(() => table.search(value).draw(), 250);
    });

    // =========================================================
    // 4. UI HELPER: trạng thái nút khi đang xử lý
    // =========================================================
    function setButtonLoading($btn, isLoading) {
        if (isLoading) {
            $btn.data("original-html", $btn.html());
            $btn.prop("disabled", true)
                .html('<span class="spinner-border spinner-border-sm me-1"></span>Đang xóa...');
        } else {
            $btn.prop("disabled", false).html($btn.data("original-html") || "Xóa");
        }
    }

    // =========================================================
    // 5. DELETE HANDLER (dùng async/await cho sạch)
    // =========================================================
    async function deleteFaculty(id, name, $row, $btn) {
        if (!id) {
            Toast.error("Mã khoa không xác định, không thể xóa.");
            return;
        }

        const confirmed = confirm(`Bạn có chắc muốn xóa khoa "${name}" không?`);
        if (!confirmed) return;

        setButtonLoading($btn, true);

        try {
            const res = await $.ajax({
                url: API.deleteUrl,
                type: "POST",
                data: { id: id },
                headers: {
                    "RequestVerificationToken": xsrfToken,
                    "X-Requested-With": "XMLHttpRequest"
                }
            });

            if (res && res.success) {
                $row.remove().draw(false);
                Toast.success(res.message || "Xóa khoa thành công.");
            } else {
                Toast.error((res && res.message) || "Xóa thất bại, vui lòng thử lại.");
            }
        } catch (xhr) {
            let msg = "Lỗi kết nối khi xóa.";
            if (xhr && xhr.status === 400)
                msg = "Mã xác thực không hợp lệ. Hãy tải lại trang và thử lại.";
            else if (xhr && xhr.status === 500)
                msg = "Lỗi máy chủ khi xóa khoa.";
            else if (xhr && xhr.responseJSON && xhr.responseJSON.message)
                msg = xhr.responseJSON.message;
            Toast.error(msg);
        } finally {
            setButtonLoading($btn, false);
        }
    }

    // =========================================================
    // 6. EVENT BINDING
    // =========================================================
    $(document).on("click", SELECTORS.deleteBtn, function (e) {
        e.preventDefault();
        const $btn = $(this);
        if ($btn.prop("disabled")) return; // chống double-click

        const $row = table.row($btn.closest("tr"));
        const id   = $btn.data("id");
        const name = $btn.data("name") || ("mã " + id);

        deleteFaculty(id, name, $row, $btn);
    });
});