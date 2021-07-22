$(document).ready(function () {
    const table = $('#myTable').DataTable({
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
            zeroRecords: "Không có thông tin",
            infoEmpty: "",
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
            url: abp.toAbsAppPath('SearchInfos/GetDataTable'),
            type: 'post',
            data: function (data) {
                maxResultCount = data.length;
                data.keyword = $('input[name=Keyword]').val().trim();
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
                "data": "readerCode", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "readerName", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "bookCode", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "bookName", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "borrowDateText", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "giveBackDateExpectText", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "giveBackDateText", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "statusText", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
        ],
    });

    $('.btnSearch').off('click').on('click', function () {
        table.draw();
    })
});