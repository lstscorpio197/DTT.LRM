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
            url: abp.toAbsAppPath('Statistics/GetDataTable'),
            type: 'post',
            data: function (data) {
                maxResultCount = data.length;
                data.startDate = $('input[name=DateStart]').val();
                data.endDate = $('input[name=DateEnd]').val();
                data.keyword = $('[name=searchkeyword]').val();
                data.bookClassifyId = $('[name=BookClassifyId]').val();
                data.bookFieldId = $('[name=BookFieldId]').val();
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
                "data": "bookCategoryName", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "total", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "totalLiquidation", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "totalLost", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "totalBorrow", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
            {
                "data": "totalGiveBack", className: "", render: function (text, display, data) {
                    return `<span>${text}</span>`;
                }
            },
        ],
    });

    $('.btnStatistic').off('click').on('click', function () {
        table.draw();
    })

    $(document).on('click', '#searchicon', function () {
        table.draw();
    })
    $(document).on('click', '#closeicon', function () {
        $('[name=searchkeyword]').val('');
        table.draw();
    })
    $('.exportexcel').off('click').on('click', function () {
        let dataSend = new FormData();
        let ipn = {};
        ipn.dateStart = $('input[name=DateStart]').val();
        ipn.dateEnd = $('input[name=DateEnd]').val();
        ipn.keyword = $('[name=searchkeyword]').val();
        ipn.bookClassifyId = $('[name=BookClassifyId]').val();
        ipn.bookFieldId = $('[name=BookFieldId]').val();
        dataSend.append('data', JSON.stringify(ipn));
        $.ajax({
            dataType: "json",
            type: "POST",
            url: "/Statistics/ExportExcel",
            data: dataSend,
            contentType: false,
            processData: false,
            success: function (res) {
                let fileGuild = res.result.fileGuild;
                let fileName = res.result.fileName;
                window.open("/Statistics/Download?fileGuild=" + fileGuild + "&fileName=" + fileName);
            }
        });
    })
});