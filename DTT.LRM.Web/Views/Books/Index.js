let _bookService = abp.services.app.book;

$(document).ready(function () {
    const table =$('#myTable').DataTable({
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
            url: abp.toAbsAppPath('Books/GetDataTable'),
            type: 'post',
            data: function (data) {
                maxResultCount = data.length;
                data.status = $('[name=status]').val();
                data.keyword = $('[name=searchkeyword]').val();
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
                "data": "code", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "name", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "publisherName", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "releaseYear", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "note", className: "", render: function (text, display, data) {
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
        window.location = abp.toAbsAppPath('Books/CreateOrUpdate?bookId=' + id);
    })

    $(document).on('change', '[name=status]', function () {
        table.draw();
    })
    $(document).on('click', '#searchicon', function () {
        table.draw();
    })
    $(document).on('click', '#closeicon', function () {
        $('[name=searchkeyword]').val('');
        table.draw();
    })
});