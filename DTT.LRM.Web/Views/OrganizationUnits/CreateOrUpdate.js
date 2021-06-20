$(document).ready(function () {

    let _organizationUnitService = abp.services.app.organizationUnit;
    let _$form = $('form[name=OrganizationUnitForm]')

    $('#ischild').on('click', function () {
        $('#IsParent').toggle();
    })

    _$form.validate({
        rules: {
            Code: {
                required: true,
                minlength: 2,
                maxlength: 20
            },
            DisplayName: {
                required: true,
                minlength: 2,
                maxlength: 100
            }
        },
        messages: {
            Code: {
                required: "Mã phòng ban không được để trống",
                minlength: "Mã phòng ban tối thiểu 2 ký tự",
                maxlength: "Mã phòng ban tối đa 20 ký tự"
            },
            DisplayName: {
                required: "Tên phòng ban không được để trống",
                minlength: "Tên phòng ban tối thiểu 2 ký tự",
                maxlength: "Tên phòng ban tối đa 100 ký tự"
            }
        }
    });

    $('.submit').on('click', function () {
        if (!_$form.valid()) {
            return false;
        }
        let organizationUnit = _$form.serializeFormToObject();
        if (!organizationUnit.IsChild)
            organizationUnit.ParentId = null;
        _organizationUnitService.createOrUpdate(organizationUnit).done(function (res) {
            if (res == 0) {
                abp.notify.warn("Mã phòng ban đã tồn tại");
            }
            else {
                if (organizationUnit.Id > 0) {
                    abp.notify.success("Lưu thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('OrganizationUnits/Index');
                    }, 500);
                }
                else {
                    abp.notify.success("Thêm mới thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('OrganizationUnits/Index');
                    }, 500);
                }
            }
        })
    })

});