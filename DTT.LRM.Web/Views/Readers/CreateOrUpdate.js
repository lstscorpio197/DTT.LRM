let _$form = $('form[name=ReaderForm]')

$(document).on('change', '#confirmpass', function () {
    alert()
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
            required: "Mã độc giả không được để trống",
            minlength: "Mã độc giả tối thiểu 2 ký tự",
            maxlength: "Mã độc giả tối đa 20 ký tự"
        },
        Name: {
            required: "Tên độc giả không được để trống",
            minlength: "Tên độc giả tối thiểu 2 ký tự",
            maxlength: "Tên độc giả tối đa 32 ký tự"
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
    let reader = _$form.serializeFormToObject();
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Readers/CreateOrUpdate",
        data: data,
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.result == 0) {
                abp.notify.warn("Mã nhà xuất bản đã tồn tại");
            }
            else if (res.result == -1) {
                abp.notify.error("Xảy ra lỗi");
            }
            else {
                if (reader.Id > 0) {
                    abp.notify.success("Lưu thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Readers/Index');
                    }, 500);
                }
                else {
                    abp.notify.success("Thêm mới thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Readers/Index');
                    }, 500);
                }
            }
        }
    });
})

function GetDataForm() {
    let data = new FormData();

    let reader = _$form.serializeFormToObject();
    let generalInfo = {};
    let userInfo = {};
    generalInfo.id = reader.Id;
    generalInfo.code = reader.Code;
    generalInfo.name = reader.Name;
    generalInfo.organizationUnitId = reader.OrganizationUnitId;
    generalInfo.positionId = reader.PositionId;
    generalInfo.userId = reader.UserId;
    generalInfo.dayOfBirth = reader.DayOfBirth;
    generalInfo.gender = reader.Gender;
    generalInfo.address = reader.Address;
    generalInfo.phoneNumber = reader.PhoneNumber;
    generalInfo.email = reader.Email;
    generalInfo.note = reader.Note;
    generalInfo.status = reader.Status;
    data.append("generalInfo", JSON.stringify(generalInfo));

    userInfo.id = reader.UserId;
    userInfo.userName = reader.UserName;
    userInfo.password = reader.Password;
    userInfo.email = reader.Email;
    data.append("userInfo", JSON.stringify(generalInfo));
    return data;
}
