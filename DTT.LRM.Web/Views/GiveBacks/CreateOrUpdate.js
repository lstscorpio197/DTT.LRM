let _bookReaderUsingService = abp.services.app.bookReaderUsing;
let _$form = $('form[name=GiveBackForm]');
let _$violateForm = $('form[name=ViolateForm]');
let _$modal = $('#ViolateModal');


let dataSend = new Array();

_$form.validate({
    rules: {
        Code: {
            required: true,
            minlength: 2,
            maxlength: 20
        },
        Creator: {
            required: true,
            minlength: 2,
            maxlength: 32
        },
        ReaderId: {
            required: true,
        }
    },
    messages: {
        Code: {
            required: "Mã phiếu trả không được để trống",
            minlength: "Mã phiếu trả tối thiểu 2 ký tự",
            maxlength: "Mã phiếu trả tối đa 20 ký tự"
        },
        Creator: {
            required: "Tên người tạo phiếu không được để trống",
            minlength: "Tên người tạo phiếu tối thiểu 2 ký tự",
            maxlength: "Tên người tạo phiếu tối đa 32 ký tự"
        },
        ReaderId: {
            required: "Vui lòng chọn độc giả",
        }
    }
});

_$violateForm.validate({
    rules: {
        ReaderName: {
            required: true
        },
        BookCode: {
            required: true
        },
        Money: {
            required: true
        }
    },
    messages: {
        ReaderName: {
            required: "Tên độc giả không được để trống"
        },
        BookCode: {
            required: "Mã sách không được để trống"
        },
        Money: {
            required: "Số tiền phạt không được để trống"
        }
    }
});

$(document).on('change', 'select[name=ReaderId]', function () {
    let readerId = ($(this).val() || 0);
    let html = '<option value="" data-borrowdate="" data-givebackdate="" data-outofdate="0">-- Chọn mã sách --</option>';
    _bookReaderUsingService.getBookUsingByReaderId(readerId).done(function (data) {
        for (let bookReader of data) {
            html += `<option value="${bookReader.book.id}" data-bookcode="${bookReader.book.code}" data-borrowdate="${bookReader.borrowDateText}" data-givebackdate="${bookReader.giveBackDateExpectText}" data-outofdate="${bookReader.outOfDate}">${bookReader.book.code}</option>`;
        }
        $('select[name=BookId]').html(html).selectpicker('refresh');
    })
})

let rowDefault = $('#tblBookGiveBack tbody tr:first-child').clone();
rowDefault.find('input').val('');

$(document).on('click', '.add-row', function () {
    let newRow = rowDefault.clone().appendTo('#tblBookGiveBack');
    newRow.find('select').selectpicker('refresh');
    let index = newRow.index();
    newRow.find('input[type=checkbox]').attr('id', 'checkbox-' + index);
    newRow.find('label.btn-checkbox').attr('for', 'checkbox-' + index);
    newRow.find("select").selectpicker('val', '').parent().siblings("button").remove();
    newRow.data('id', 0);
    $(".bootstrap-select").removeClass("open").find("*").attr("aria-expanded", false);
    OrderIndex();
    GetListBook(newRow);
})

function OrderIndex() {
    $.each($('#tblBookGiveBack tbody tr'), function () {
        let index = $(this).index();
        $(this).find('.stt').text(index + 1);
    })
}

function GetListBook(newRow) {
    let readerId = ($('select[name = ReaderId]').val() || 0);
    let html = '<option value="" data-borrowdate="" data-borrowdate="" data-outofdate="0">-- Chọn mã sách --</option>';
    _bookReaderUsingService.getBookUsingByReaderId(readerId).done(function (data) {
        for (let bookReader of data) {
            html += `<option value="${bookReader.book.id}" data-bookcode="${bookReader.book.code}" data-borrowdate="${bookReader.borrowDateText}" data-givebackdate="${bookReader.giveBackDateExpectText}" data-outofdate="${bookReader.outOfDate}">${bookReader.book.code}</option>`;
        }
        newRow.find('select[name=BookId]').html(html).selectpicker('refresh');
    })
}

//Xoa dong
$(document).on('change', '#tblBookGiveBack input[type=checkbox]', function () {
    ToggleTrashButton();
})

function ToggleTrashButton() {
    let countChecked = $('#tblBookGiveBack tbody').find('input[type=checkbox]:checked').length;
    if (countChecked > 0)
        $('.btn-trash-row').removeClass('hide');
    else
        $('.btn-trash-row').addClass('hide');
}

$(document).on('click', '.btn-trash-row', function () {
    $.each($('#tblBookGiveBack tbody tr input[type=checkbox]:checked'), function (i) {
        $(this).closest('tr').remove();
    })
})
//end xoa dong

$(document).on('change', 'select[name=BookId]', function () {
    let $row = $(this).closest('tr');
    let $optionSelected = $(this).find('option:selected');
    let borrowDate = $optionSelected.data('borrowdate');
    let giveBackDate = $optionSelected.data('givebackdate');
    let outOfDate = $optionSelected.data('outofdate');
    $row.find('input.borrowdate').val(borrowDate);
    $row.find('input.givebackdate').val(giveBackDate);
})

$(document).on('click', '.btnPhieuViPham', function () {
    let $row = $(this).closest('tr');
    let readerId = _$form.find('select[name=ReaderId]').val();
    let readerName = _$form.find('select[name=ReaderId]').find('option:selected').data('name');
    let bookId = $row.find('select[name=BookId]').val();
    let bookCode = $row.find('select[name=BookId]').find('option:selected').data('bookcode');

    _$violateForm.find('[name=ReaderId]').val(readerId);
    _$violateForm.find('[name=ReaderName]').val(readerName);
    _$violateForm.find('[name=BookId]').val(bookId);
    _$violateForm.find('[name=BookCode]').val(bookCode);

    _$modal.find('.btnSave').on('click', function () {
        if (!_$violateForm.valid()) {
            return false;
        }
        let violate = _$violateForm.serializeFormToObject();
        let ipn = {};
        ipn.violate = violate;
        ipn.readerId = readerId;
        ipn.bookId = bookId;
        ipn.note = $row.find('.note').val();
        ipn.status = 1;

        if (ipn.readerId > 0 && ipn.bookId > 0) {
            dataSend.push(ipn);
            $row.find('.status').val(1);
            $row.find('.statustext').val("Lưu vi phạm");
            _$modal.modal('hide');
        }
    })
})


//submit event
$('.submit').on('click', function () {
    let data = new FormData();
    if (!_$form.valid()) {
        return false;
    }
    let giveBack = _$form.serializeFormToObject();
    let generalInfo = {};
    generalInfo.readerId = giveBack.ReaderId;
    generalInfo.employeeId = giveBack.EmployeeId;
    generalInfo.code = giveBack.Code;
    generalInfo.giveBackDate = giveBack.GiveBackDate;
    generalInfo.note = giveBack.Note;
    data.append("generalInfo", JSON.stringify(generalInfo));

    let listExist = dataSend.map(x => x.bookId);
    let listBookGiveBacks = new Array();
    $.each($('#tblBookGiveBack tbody tr'), function () {
        let ipn = {};
        ipn.id = $(this).data('id');
        ipn.readerId = _$form.find('select[name=ReaderId]').val();
        ipn.bookId = $(this).find('select[name=BookId]').val();
        ipn.note = $(this).find('input.note').val();
        ipn.status = ($(this).find('input.status').val()||0);
        if (!listExist.includes(ipn.bookId) && ipn.bookId > 0)
            listBookGiveBacks.push(ipn);
    })
    data.append("listBookGiveBackAndViolates", JSON.stringify(dataSend));
    data.append("listBookGiveBacks", JSON.stringify(listBookGiveBacks));
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/GiveBacks/CreateOrUpdate",
        data: data,
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.result == 0) {
                abp.notify.warn("Mã phiếu đã tồn tại");
            }
            else if (res.result == -1) {
                abp.notify.error("Xảy ra lỗi");
            }
            else {
                if (giveBack.Id > 0) {
                    abp.notify.success("Lưu thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('GiveBacks/Index');
                    }, 500);
                }
                else {
                    abp.notify.success("Thêm mới thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('GiveBacks/Index');
                    }, 500);
                }
            }
        }
    });
})


