let _$form = $('form[name=ChangePasswordForm]');

$(document).on('click', '.form-line i', function () {
    $(this).toggleClass('fa fa-eye').toggleClass('fa fa-eye-slash');
    let $input = $(this).siblings('input');
    let inputType = $input.attr('type');
    inputType = (inputType == 'password') ? 'text' : 'password';
    $input.attr('type', inputType);
})

_$form.validate({
    rules: {
        OldPassword: {
            required: true,
            minlength: 5,
            maxlength: 16
        },
        NewPassword: {
            required: true,
            minlength: 5,
            maxlength: 16
        },
        ConfirmPassword: {
            equalTo: "#newpassword"
        },
    },
    messages: {
        OldPassword: {
            required: "Mật khẩu hiện tại không được để trống",
            minlength: "Mật khẩu hiện tại thiểu 5 ký tự",
            maxlength: "Mật khẩu hiện tại đa 16 ký tự"
        },
        NewPassword: {
            required: "Mật khẩu mới không được để trống",
            minlength: "Mật khẩu mới tối thiểu 5 ký tự",
            maxlength: "Mật khẩu mới tối đa 16 ký tự"
        },
        ConfirmPassword: {
            equalTo: "Mật khẩu không khớp"
        },
    }
});

$('.submit').on('click', function () {
    if (!_$form.valid()) {
        return false;
    }
    let data = new FormData();
    let obj = {};
    obj.oldPassword = $('[name=OldPassword]').val();
    obj.newPassword = $('[name=NewPassword]').val();
    data.append("data", JSON.stringify(obj));
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Account/ChangePasswordAsync",
        data: data,
        contentType: false,
        processData: false,
        success: function (res) {
            switch (res.result) {
                case -1:
                    abp.notify.error("Xảy ra lỗi");
                    break;
                case 0:
                    abp.notify.warn("Mật khẩu cũ không chính xác");
                    $('[name=OldPassword]').focus();
                    break;
                case 1:
                    abp.notify.success("Đổi mật khẩu mới thành công");
                    $.blockUI();
                    setTimeout(function () {
                        $.unblockUI()
                        window.location = abp.toAbsAppPath('Account/Login');
                    }, 500);
            }
        }
    });
})