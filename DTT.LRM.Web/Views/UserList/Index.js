let _userService = abp.services.app.user;

$(document).ready(function () {
    $('#myTable').DataTable({
        destroy: true,
        pageLength: 10,
        searching: true,
        autoWidth: false,
        sDom:
            '<"row view-filter"<"col-sm-12"<"pull-left"><"pull-right">>>t<"view-pager"r<"footer-table"<"col-md-6"i>< and ><"col-md-6"p>>>',
        sort: false,
        processing: true,
        serverSide: true,
        language: {
            lengthMenu: "Hiện thị _MENU_ bản ghi",
            zeroRecords: "Không có bản ghi",
            infoEmpty: "Bản ghi không tồn tại",
            infoFiltered: "(Được lọc từ _MAX_ tổng số bản ghi)",
            info: "Hiển thị _START_ đến _END_ của _TOTAL_ bản ghi",
            search: "Tìm kiếm",
            paginate: {
                previous: "Trang trước",
                next: "Trang sau"
            },
            sProcessing: '<div class="spinner"></div>',
        },
        ajax: {
            url: abp.toAbsAppPath('UserList/GetDataTable'),
            type: 'post',
            data: function (data) {
                maxResultCount = data.length;
            },
            dataFilter: function (json) {
                let data = JSON.parse(json);
                var totalCounter = data.result.data.totalCount;
                data.recordsTotal = totalCounter;
                data.recordsFiltered = totalCounter;
                data.data = data.result.data.items;
                startIndex = data.result.startIndex;
                return JSON.stringify(data);
            },

        },
        columns: [
            {
                "data": null, className: "", render: function (text, display, data) {
                    return `<span>${startIndex++}</span>`;
                }
            },
            {
                "data": "userName", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "emailAddress", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "surname", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "isActiveIcon", className: "", render: function (text, display, data) {
                    return `<i class="${text}"></i>`;
                }
            },
            {
                "data": null, className: "", render: function (text, display, data) {
                    return `<button class="active">Khóa/Mở khóa</button><button class="resetpass">Reset Mật khẩu</button>`;
                }
            },
        ],
        createdRow: function (row, data, index, arrCell) {
            $(row).data('id', data.id);
        }
    });

    $(document).on('click', '.active', function () {
        let isCanEdit = $(this).closest('table').data('iscanedit');
        if (isCanEdit == 0) {
            abp.notify.error("Bạn không có quyền này");
            return false;
        }
        let userId = $(this).closest('tr').data('id');
        if (userId > 0) {
            abp.message.confirm(
                "Bạn có chắc muốn khóa/kích hoạt tài khoản này?", "Xác nhận?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        _userService.activeToggle(userId).done(function (data) {
                            if (data == 1) {
                                abp.notify.success("Thành công");
                                setTimeout(function () {
                                    window.location = abp.toAbsAppPath('UserList/Index');
                                }, 500);
                            }
                            else {
                                abp.notify.error("Xảy ra lỗi");
                            }
                        })
                    }
                }
            );
        }
    })
    $(document).on('click', '.resetpass', function () {
        let isCanEdit = $(this).closest('table').data('iscanedit');
        if (isCanEdit == 0) {
            abp.notify.error("Bạn không có quyền này");
            return false;
        }
        let userId = $(this).closest('tr').data('id');
        if (userId > 0) {
            abp.message.confirm(
                "Bạn có chắc muốn reset mật khẩu tài khoản này?", "Xác nhận?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        $.ajax({
                            dataType: "json",
                            type: "POST",
                            url: "/Account/ResetPassword?userId=" + userId,
                            contentType: false,
                            processData: false,
                            success: function (res) {
                                switch (res.result) {
                                    case 0:
                                        abp.notify.error("Xảy ra lỗi");
                                        break;
                                    case 1:
                                        abp.notify.success("Reset mật khẩu thành công");
                                        $.blockUI();
                                        setTimeout(function () {
                                            $.unblockUI()
                                            window.location = abp.toAbsAppPath('UserList/Index');
                                        }, 500);
                                }
                            }
                        });
                    }
                }
            );
        }
    })
});