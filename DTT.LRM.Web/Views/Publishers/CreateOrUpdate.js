$(document).ready(function () {

    let _publisherService = abp.services.app.publisher;
    let _$form = $('form[name=PublisherForm]')

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
            }
        },
        messages: {
            Code: {
                required: "Mã nhà xuất bản không được để trống",
                minlength: "Mã nhà xuất bản tối thiểu 2 ký tự",
                maxlength: "Mã nhà xuất bản tối đa 20 ký tự"
            },
            Name: {
                required: "Tên nhà xuất bản không được để trống",
                minlength: "Tên nhà xuất bản tối thiểu 2 ký tự",
                maxlength: "Tên nhà xuất bản tối đa 100 ký tự"
            }
        }
    });

    $('.submit').on('click', function () {
        if (!_$form.valid()) {
            return false;
        }
        let publisher = _$form.serializeFormToObject();
        _publisherService.createOrUpdate(publisher).done(function (res) {
            if (res == 0) {
                abp.notify.warn("Mã nhà xuất bản đã tồn tại");
            }
            else {
                if (publisher.Id > 0) {
                    abp.notify.success("Lưu thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Publishers/Index');
                    }, 500);
                }
                else {
                    abp.notify.success("Thêm mới thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Publishers/Index');
                    }, 500);
                }
            }
        })
    })

});