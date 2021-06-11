let _bookClassifyService = abp.services.app.bookClassify;

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
            url: abp.toAbsAppPath('BookClassifies/GetDataTable'),
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
                "data": "id", className: "btn-checkbox", render: function (text, display, data) {
                    return `<input id="status-${text}" type="checkbox" name="Status" value="${text}" class="form-control" />
                            <label for="status-${text}"></label>`;
                }
            },
            {
                "data": null, className: "", render: function (text, display, data) {
                    return `<span>${startIndex++}</span>`;
                }
            },
            {
                "data": "codeText", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "name", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "statusText", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
        ],
        createdRow: function (row, data, index, arrCell) {
            $(row).addClass('cursor-pointer');
            $(row).data('id', data.id);
        }
    });

    $(document).on('click', '#myTable tbody tr td:not(.btn-checkbox)', function () {
        let id = $(this).closest('tr').data('id');
        window.location = abp.toAbsAppPath('BookClassifies/CreateOrUpdate?bookClassifyId=' + id);
    })
});

$('.btn-trash').on('click', function () {
    abp.message.confirm(
        "Bạn có chắc muốn xóa các phân loại đã chọn?","Xóa?",
        function (isConfirmed) {
            if (isConfirmed) {
                let count = $('#myTable tbody tr input[type=checkbox]:checked').length;
                $.each($('#myTable tbody tr input[type=checkbox]:checked'), function (i) {
                    let id = $(this).closest('tr').data('id');
                    _bookClassifyService.deleteById(id).done(function () {
                        if (count == i + 1) {
                            abp.notify.success("Đã xóa thành công");
                            setTimeout(function () {
                                window.location = abp.toAbsAppPath('BookClassifies/Index');
                            }, 500);
                        }
                    })
                })
            }
        }
    );
})

$(document).on('change', '#myTable tr th input[type=checkbox]', function () {
    if ($(this).is(':checked')) {
        $('input:checkbox').prop('checked', true);
    }
    else {
        $('input:checkbox').prop('checked', false);
    }
})