let _readerService = abp.services.app.reader;
let _userService = abp.services.app.user;

let _$form = $('form[name=EmployeeForm]');

$(document).on('change', '#confirmpass', function () {

})

function ToggleTrashButton() {
    let countChecked = $('#tblPositionQuota tbody').find('input[type=checkbox]:checked').length;
    if (countChecked > 0)
        $('.btn-trash-row').removeClass('hide');
    else
        $('.btn-trash-row').addClass('hide');
}

$(document).on('click', '.btn-trash-row', function () {
    $.each($('#tblPositionQuota tbody tr input[type=checkbox]:checked'), function (i) {
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
        Name: {
            required: true,
            minlength: 2,
            maxlength: 32
        },
        OrganizationUnitId: {
            required: true
        },
        PositionId: {
            required: true
        },
        DayOfBirth: {
            required: true
        },
        Email: {
            required: true
        },
        UserName: {
            required: true,
            minlength: 5,
            maxlength: 32
        },
        Password: {
            required: true,
            minlength: 8,
            maxlength: 32
        },
    },
    messages: {
        Code: {
            required: "Mã nhân viên không được để trống",
            minlength: "Mã nhân viên tối thiểu 2 ký tự",
            maxlength: "Mã độc nhân viên đa 20 ký tự"
        },
        Name: {
            required: "Tên nhân viên không được để trống",
            minlength: "Tên nhân viên tối thiểu 2 ký tự",
            maxlength: "Tên nhân viên tối đa 32 ký tự"
        },
        OrganizationUnitId: {
            required: "Vui lòng chọn phòng ban"
        },
        PositionId: {
            required: "Vui lòng chọn chức vụ"
        },
        DayOfBirth: {
            required: "Vui lòng chọn ngày sinh"
        },
        Email: {
            required: "Email không được để trống"
        },
        UserName: {
            required: "Tên đăng nhập không được để trống",
            minlength: "Tên đăng nhập tối thiểu 5 ký tự",
            maxlength: "Tên đăng nhập tối đa 32 ký tự"
        },
        Password: {
            required: "Mật khẩu không được để trống",
            minlength: "Mật khẩu tối thiểu 8 ký tự",
            maxlength: "Mật khẩu tối đa 32 ký tự"
        },
    }
});

$('.submit').on('click', function () {
    if (!_$form.valid()) {
        return false;
    }
    let data = GetDataForm();
    let employee = _$form.serializeFormToObject();

    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Employees/CreateOrUpdate",
        data: data,
        contentType: false,
        processData: false,
        success: function (res) {
            switch (res.result) {
                case -1:
                    abp.notify.error("Xảy ra lỗi");
                    break;
                case -2:
                    abp.notify.warn("Mã độc giả đã tồn tại");
                    break;
                case -3:
                    abp.notify.warn("Email đã tồn tại");
                    break;
                case -4:
                    abp.notify.warn("Tài khoản đã tồn tại");
                    break;
                default:
                    if (employee.Id > 0) {
                        abp.notify.success("Lưu thành công");
                        setTimeout(function () {
                            window.location = abp.toAbsAppPath('Employees/Index');
                        }, 500);
                    }
                    else {
                        abp.notify.success("Thêm mới thành công");
                        setTimeout(function () {
                            window.location = abp.toAbsAppPath('Employees/Index');
                        }, 500);
                    }
            }
        }
    });
})


function GetDataForm() {
    let data = new FormData();

    let employee = _$form.serializeFormToObject();
    let generalInfo = {};
    let userInfo = {};
    generalInfo.id = employee.Id;
    generalInfo.code = employee.Code;
    generalInfo.name = employee.Name;
    generalInfo.organizationUnitId = employee.OrganizationUnitId;
    generalInfo.positionId = employee.PositionId;
    generalInfo.userId = employee.UserId;
    generalInfo.dayOfBirth = employee.DayOfBirth;
    generalInfo.gender = employee.Gender;
    generalInfo.address = employee.Address;
    generalInfo.phoneNumber = employee.PhoneNumber;
    generalInfo.email = employee.Email;
    generalInfo.note = employee.Note;
    generalInfo.status = employee.Status;
    data.append("generalInfo", JSON.stringify(generalInfo));

    userInfo.id = employee.UserId;
    userInfo.userName = employee.UserName;
    userInfo.password = employee.Password;
    userInfo.emailAddress = employee.Email;
    userInfo.name = employee.Name;
    userInfo.surname = employee.Name;
    userInfo.isActive = true;

    userInfo.roleNames = [];
    var _$roleCheckboxes = $("input[name='role']:checked");
    if (_$roleCheckboxes) {
        for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
            var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
            userInfo.roleNames.push(_$roleCheckbox.attr('data-role-name'));
        }
    }

    data.append("userInfo", JSON.stringify(userInfo));

    return data;
}
