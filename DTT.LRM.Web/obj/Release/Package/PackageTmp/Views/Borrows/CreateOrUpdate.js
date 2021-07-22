let _bookService = abp.services.app.book;
let _$form = $('form[name=BorrowForm]')

let rowDefault = $('#tblBookBorrow tbody tr:first-child').clone();
let borrowId = $('#id').val();

rowDefault.find('input').val('');

$(document).on('click', '.add-row', function () {
    let newRow = rowDefault.clone().appendTo('#tblBookBorrow');
    newRow.find('select').selectpicker('refresh');
    let index = newRow.index();
    newRow.find('input[type=checkbox]').attr('id', 'checkbox-' + index);
    newRow.find('label.btn-checkbox').attr('for', 'checkbox-' + index);
    newRow.find("select").selectpicker('val', '').parent().siblings("button").remove();
    newRow.data('id', 0);
    $(".bootstrap-select").removeClass("open").find("*").attr("aria-expanded", false);
    OrderIndex();
})

function OrderIndex() {
    $.each($('#tblBookBorrow tbody tr'), function () {
        let index = $(this).index();
        $(this).find('.stt').text(index + 1);
    })
}

$(document).on('change', '#tblBookBorrow tr th #checkbox-all', function () {
    if ($(this).is(':checked')) {
        $('input:checkbox').prop('checked', true);
    }
    else {
        $('input:checkbox').prop('checked', false);
    }
})

$(document).on('change', '#tblBookBorrow input[type=checkbox]', function () {
    ToggleTrashButton();
})

function ToggleTrashButton() {
    let countChecked = $('#tblBookBorrow tbody').find('input[type=checkbox]:checked').length;
    if (countChecked > 0)
        $('.btn-trash-row').removeClass('hide');
    else
        $('.btn-trash-row').addClass('hide');
}

$(document).on('click', '.btn-trash-row', function () {
    $.each($('#tblBookBorrow tbody tr input[type=checkbox]:checked'), function (i) {
        $(this).closest('tr').remove();
    })
})


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
            required: "Mã phiếu mượn không được để trống",
            minlength: "Mã phiếu mượn tối thiểu 2 ký tự",
            maxlength: "Mã phiếu mượn tối đa 20 ký tự"
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

$('.submit, .do').on('click', function () {
    let status = $(this).data('status');
    if (!_$form.valid()) {
        return false;
    }
    let data = GetDataForm(status);
    let borrow = _$form.serializeFormToObject();
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Borrows/CreateOrUpdate",
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
            else if (res.result == -2) {
                abp.notify.warn("Bạn cần điền đẩy đủ thông tin mã sách");
            }
            else {
                if (borrow.Id > 0) {
                    abp.notify.success("Lưu thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Borrows/Index');
                    }, 500);
                }
                else {
                    abp.notify.success("Thêm mới thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Borrows/Index');
                    }, 500);
                }
            }
        }
    });
})

function GetDataForm(status) {
    let data = new FormData();

    let borrow = _$form.serializeFormToObject();
    let generalInfo = {};
    generalInfo.id = borrow.Id;
    generalInfo.code = borrow.Code;
    generalInfo.creator = borrow.Creator;
    generalInfo.readerId = borrow.ReaderId;
    generalInfo.note = borrow.Note;
    generalInfo.borrowDate = borrow.BorrowDate;
    generalInfo.status = status;
    data.append("generalInfo", JSON.stringify(generalInfo));

    let listBooks = new Array();
    $.each($('#tblBookBorrow tbody tr'), function () {
        let ipn = {};
        ipn.id = $(this).data('id');
        ipn.bookCategoryId = $(this).find('select[name=BookCategoryId]').val();
        ipn.bookId = $(this).find('select[name=BookId]').val();
        ipn.author = $(this).find('input.author').val();
        ipn.publisherId = $(this).find('input.publisherid').val();
        ipn.releaseYear = $(this).find('input.releaseyear').val();
        ipn.giveBackDate = $(this).find('input.givebackdate').val();
        if (ipn.bookCategoryId > 0)
            listBooks.push(ipn);
    })
    if (listBooks.length > 0)
        data.append("listBooks", JSON.stringify(listBooks));

    return data;
}

$(document).on('change', 'select[name=BookCategoryId]', function () {
    let html = '<option value="">-- Chọn mã sách --</option>';
    let $row = $(this).closest('tr');
    let ipn = {}
    ipn.BookCategoryId = ($(this).val()||0);
    ipn.Author = $row.find('.author').val();
    ipn.PublisherId = ($row.find('.publisherid').val()||0);
    ipn.ReleaseYear = ($row.find('.releaseyear').val() || 0);
    if (borrowId == 0) {
        let giveBackDate = $(this).find('option:selected').data('borrowtime');
        $row.find('input.givebackdate').val(giveBackDate || '');
    }
    if (ipn.BookCategoryId > 0) {
        _bookService.getListBookForBorrow(ipn).done(function (data) {
            for (let book of data) {
                html += `<option value="${book.id}">${book.code}</option>`;
            }
            $row.find('select[name=BookId]').html(html).selectpicker('refresh');
        })
    }
    else {
        $row.find('select[name=BookId]').html(html).selectpicker('refresh');
    }
    $row.find('select[name=BookId]').parent().siblings("button").remove();
})

if (borrowId > 0) {
    $.each($('#tblBookBorrow tbody tr'), function () {
        $(this).find('select[name=BookCategoryId]').trigger('change');
    })
}