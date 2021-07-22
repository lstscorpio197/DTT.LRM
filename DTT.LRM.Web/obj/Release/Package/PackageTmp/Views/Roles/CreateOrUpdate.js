$(document).ready(function () {

    var _roleService = abp.services.app.role;
    var _$form = $('form[name=RoleForm]');

    _$form.validate({
        rules: {
            Name: {
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
            Name: {
                required: "Mã nhóm quyền không được để trống",
                minlength: "Mã nhóm quyền bản tối thiểu 2 ký tự",
                maxlength: "Mã nhóm quyền bản tối đa 20 ký tự"
            },
            DisplayName: {
                required: "Tên nhóm quyền bản không được để trống",
                minlength: "Tên nhóm quyền bản tối thiểu 2 ký tự",
                maxlength: "Tên nhóm quyền bản tối đa 100 ký tự"
            }
        }
    });

    $('.submit').on('click', function (e) {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var role = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        role.grantedPermissions = [];
        var _$permissionCheckboxes = $("input[name='permission']:checked:visible");
        if (_$permissionCheckboxes) {
            for (var permissionIndex = 0; permissionIndex < _$permissionCheckboxes.length; permissionIndex++) {
                var _$permissionCheckbox = $(_$permissionCheckboxes[permissionIndex]);
                role.grantedPermissions.push(_$permissionCheckbox.val());
            }
        }

        let data = new FormData();
        let ipn = {};
        ipn.id = role.Id;
        ipn.name = role.Name;
        ipn.displayName = role.DisplayName;
        ipn.description = role.Description;
        ipn.grantedPermissions = role.grantedPermissions;
        ipn.isStatic = role.IsStatic;
        if ($('#isdefault').is(':checked'))
            ipn.isDefault = true;
        else
            ipn.isDefault = false;

        data.append('role', JSON.stringify(ipn));
        $.ajax({
            dataType: "json",
            type: "POST",
            url: "/Roles/CreateOrUpdate",
            data: data,
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.result == 0) {
                    abp.notify.warn("Mã đã tồn tại");
                }
                else if (res.result == -1) {
                    abp.notify.error("Xảy ra lỗi");
                }
                else {
                    if (role.Id > 0) {
                        abp.notify.success("Lưu thành công");
                        setTimeout(function () {
                            window.location = abp.toAbsAppPath('Roles/Index');
                        }, 500);
                    }
                    else {
                        abp.notify.success("Thêm mới thành công");
                        setTimeout(function () {
                            window.location = abp.toAbsAppPath('Roles/Index');
                        }, 500);
                    }
                }
            }
        });
        //if (role.Id == 0) {
        //    abp.ui.setBusy(_$modal);
        //    _roleService.create(role).done(function () {
                
        //    }).always(function () {
        //        abp.ui.clearBusy(_$modal);
        //    });
        //}
        //else {
        //    abp.ui.setBusy(_$form);
        //    _roleService.update(role).done(function () {
                
        //    }).always(function () {
        //        abp.ui.clearBusy(_$modal);
        //    });
        //}
    });
});