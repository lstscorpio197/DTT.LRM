let _bookFieldService = abp.services.app.bookField;
let _bookCategoryService = abp.services.app.bookCategory;
let _$form = $('form[name=BookForm]')

let classifyCode = 'xxx';
let fieldCode = 'xxx';
let categoryCode = 'xxx';

function AutoRenderBookCode() {
    let bookCode = classifyCode + fieldCode + categoryCode + 'xxxx';
    $('#code').val(bookCode);
}

function AutoRenderBookName() {
    if ($('#isnewcategory').is(':checked')) {
        $('#name').prop('readonly', false);
        $('#name').prop('placeholder', 'Nhập tên sách');
    }
    else {
        $('#name').prop('readonly', true);
        $('#name').prop('placeholder', 'Tên sách tự động thay đổi theo loại sách');
        let bookName = $('select[name=BookCategoryId]').find('option:selected').attr('name');
        $('#name').val(bookName);
    }
}


$('select[name=BookClassifyId]').on('change', function () {
    let classifyId = $(this).val();
    let html = `<option value="" code="xxx" selected>-- Chọn lĩnh vực sách --</option>`;
    if (classifyId > 0) {
        _bookFieldService.getListForSelectByBookClassifyId(classifyId).done(function (data) {
            for (let bookField of data) {
                html += `<option value="${bookField.id}" code="${bookField.codeText}">${bookField.name}</option>`;
                $('select[name=BookFieldId]').html(html).selectpicker('refresh');
            }
        })
    }
    else
        $('select[name=BookFieldId]').html(html).val('').selectpicker('refresh');
    $('select[name=BookFieldId]').parent().siblings("button").remove();
    $('select[name=BookFieldId]').trigger('change');
    classifyCode = $(this).find('option:selected').attr('code');
    if (!classifyCode)
        classifyCode = 'xxx';
    AutoRenderBookCode();
})

$('select[name=BookFieldId]').on('change', function () {
    let fieldId = $(this).val();
    let html = `<option value="" code="xxx" name="" selected>-- Chọn loại sách --</option>`;
    if (fieldId > 0) {
        _bookCategoryService.getListForSelectByBookFieldId(fieldId).done(function (data) {
            for (let bookCategory of data) {
                html += `<option value="${bookCategory.id}" code="${bookCategory.codeText}" name="${bookCategory.name}">${bookCategory.name}</option>`;
            }
            $('select[name=BookCategoryId]').html(html).val('').selectpicker('refresh');
        })
    }
    else
        $('select[name=BookCategoryId]').html(html).val('').selectpicker('refresh');
    $('select[name=BookCategoryId]').parent().siblings("button").remove();
    $('select[name=BookCategoryId]').trigger('change');
    fieldCode = $(this).find('option:selected').attr('code');
    if (!fieldCode)
        fieldCode = 'xxx';
    AutoRenderBookCode();
})

$('select[name=BookCategoryId]').on('change', function () {
    categoryCode = $(this).find('option:selected').attr('code');
    if (!categoryCode)
        categoryCode = 'xxx';
    AutoRenderBookCode();
    AutoRenderBookName();
})


$(document).on('click', '#isnewcategory', function () {
    $('#ToggleSelect').toggle();
    if ($(this).is(':checked')) {
        $('#TotalBorrowTime').removeClass('hide');
    }
    else {
        $('#TotalBorrowTime').addClass('hide');
    }
    AutoRenderBookName();
})

_$form.validate({
    rules: {
        Name: {
            required: true
        },
        BookClassifyId: {
            required: true
        },
        BookFieldId: {
            required: true
        },
        PublisherId: {
            required: true
        },
        Price: {
            required: true
        },
        Amount: {
            required: function () {
                if ($('#id').val() <= 0)
                    return true;
            }
        }
    },
    messages: {
        Name: {
            required: "Tên sách không được để trống"
        },
        BookClassifyId: {
            required: "Vui lòng chọn phân loại sách"
        },
        BookFieldId: {
            required: "Vui lòng chọn lĩnh vực sách"
        },
        PublisherId: {
            required: "Vui lòng chọn nhà xuất bản"
        },
        Price: {
            required: "Vui lòng nhập giá sách"
        },
        Amount: {
            required: "Vui lòng nhập số lượng sách"
        }
    },
});


$(document).on('click', '.submit', function () {
    //if (!_$form.valid()) {
    //    return false;
    //}
    let data = new FormData();
    let book = _$form.serializeFormToObject();
    console.log(book);
    let bookIpn = {};
    bookIpn.id = book.Id;
    bookIpn.code = book.Code;
    bookIpn.name = book.Name;
    bookIpn.publisherId = book.PublisherId;
    bookIpn.author = book.Author;
    bookIpn.releaseYear = book.ReleaseYear;
    bookIpn.language = book.Language;
    bookIpn.pageCount = book.PageCount;
    bookIpn.price = book.Price;
    bookIpn.amount = book.Amount;
    bookIpn.note = book.Note;
    bookIpn.status = book.Status;
    
    if ($('#isnewcategory').is(':checked')) {
        bookIpn.bookCategoryId = 0;
        let newCategory = {};
        newCategory.id = 0;
        newCategory.code = 0;
        newCategory.name = book.Name;
        newCategory.bookFieldId = book.BookFieldId;
        newCategory.totalBorrowTime = book.TotalBorrowTime;
        newCategory.status = true;
        if (!newCategory.totalBorrowTime > 0 || newCategory.totalBorrowTime <= 0) {
            abp.notify.warn("Thời gian mượn tối thiểu là 1 ngày");
            return false;
        }
        data.append('book', JSON.stringify(bookIpn));
        data.append('newBookCategory', JSON.stringify(newCategory));
        console.log(newCategory.totalBorrowTime);
    }
    else {
        bookIpn.bookCategoryId = book.BookCategoryId;
        data.append('book', JSON.stringify(bookIpn));
    }
    //$.ajax({
    //    dataType: "json",
    //    type: "POST",
    //    url: "/Books/CreateOrUpdate",
    //    data: data,
    //    contentType: false,
    //    processData: false,
    //    success: function (res) {
    //        if (res.result == 0) {
    //            abp.notify.warn("Mã sách đã tồn tại");
    //        }
    //        else if (res.result == -1) {
    //            abp.notify.error("Xảy ra lỗi");
    //        }
    //        else {
    //            if (book.Id > 0) {
    //                abp.notify.success("Lưu thành công");
    //                setTimeout(function () {
    //                    window.location = abp.toAbsAppPath('Books/Index');
    //                }, 500);
    //            }
    //            else {
    //                abp.notify.success("Thêm mới thành công");
    //                setTimeout(function () {
    //                    window.location = abp.toAbsAppPath('Books/Index');
    //                }, 500);
    //            }
    //        }
    //    }
    //});
})

