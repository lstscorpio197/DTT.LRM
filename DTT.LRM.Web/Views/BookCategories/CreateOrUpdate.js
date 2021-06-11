$(document).ready(function () {

    let _bookCategoryService = abp.services.app.bookCategory;
    let _$form = $('form[name=BookCategoryForm]')

    _$form.validate({
        rules: {
            Code: {
                required: true,
                minlength: 2,
                maxlength: 20
            },
            Name: {
                required: true,
                minlength: 2,
                maxlength: 100
            },
            BookFieldId: {
                required: true
            }
        },
        messages: {
            Code: {
                required: "Mã lĩnh vực không được để trống",
                minlength: "Mã lĩnh vực tối thiểu 2 ký tự",
                maxlength: "Mã lĩnh vực tối đa 20 ký tự"
            },
            Name: {
                required: "Tên lĩnh vực không được để trống",
                minlength: "Tên lĩnh vực tối thiểu 2 ký tự",
                maxlength: "Tên lĩnh vực tối đa 100 ký tự"
            },
            BookFieldId: {
                required: "Vui lòng chọn lĩnh vực sách"
            }
        }
    });

    $('.submit').on('click', function () {
        if (!_$form.valid()) {
            return false;
        }
        let bookCategory = _$form.serializeFormToObject();
        _bookCategoryService.createOrUpdate(bookCategory).done(function (res) {
            if (res == 0) {
                abp.notify.warn("Mã loại sách đã tồn tại");
            }
            else {
                if (bookCategory.Id > 0) {
                    abp.notify.success("Lưu thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('BookCategories/Index');
                    }, 500);
                }
                else {
                    abp.notify.success("Thêm mới thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('BookCategories/Index');
                    }, 500);
                }
            }
        })
    })

});