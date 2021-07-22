$(document).ready(function () {

    let _bookFieldService = abp.services.app.bookField;
    let _$form = $('form[name=BookFieldForm]')

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
                maxlength: 32
            },
            BookClassifyId: {
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
                maxlength: "Tên lĩnh vực tối đa 32 ký tự"
            },
            BookClassifyId: {
                required: "Vui lòng chọn phân loại sách"
            }
        }
    });

    $('.submit').on('click', function () {
        if (!_$form.valid()) {
            return false;
        }
        let bookField = _$form.serializeFormToObject();
        _bookFieldService.createOrUpdate(bookField).done(function (res) {
            if (res == 0) {
                abp.notify.warn("Mã lĩnh vực đã tồn tại");
            }
            else {
                if (bookField.Id > 0) {
                    abp.notify.success("Lưu thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('BookFields/Index');
                    }, 500);
                }
                else {
                    abp.notify.success("Thêm mới thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('BookFields/Index');
                    }, 500);
                }
            }
        })
    })

});