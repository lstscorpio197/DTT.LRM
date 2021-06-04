$(document).ready(function () {

    let _bookClassifyService = abp.services.app.bookClassify;
    let _$form = $('form[name=BookClassifyForm]')

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
            }
        },
        messages: {
            Code: {
                required: "Mã phân loại không được để trống",
                minlength: "Mã phân loại tối thiểu 2 ký tự",
                maxlength: "Mã phân loại tối đa 20 ký tự"
            },
            Name: {
                required: "Tên phân loại không được để trống",
                minlength: "Tên phân loại tối thiểu 2 ký tự",
                maxlength: "Tên phân loại tối đa 32 ký tự"
            }
        }
    });

    $('.submit').on('click', function () {
        if (!_$form.valid()) {
            return false;
        }
        let bookClassify = _$form.serializeFormToObject();
        _bookClassifyService.createOrUpdate(bookClassify).done(function () {
            alert("Ok");
        })
    })

});