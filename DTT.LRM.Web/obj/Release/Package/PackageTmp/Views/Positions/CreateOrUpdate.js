let _$form = $('form[name=PositionForm]')

let rowQuotaDefault = $('#tblPositionQuota tbody tr:first-child').clone();
rowQuotaDefault.find('input[name=Amount]').val(0);

$(document).on('click', '.add-row', function () {
    let newRow = rowQuotaDefault.clone().appendTo('#tblPositionQuota');
    newRow.find('select').selectpicker('refresh');
    let index = newRow.index();
    newRow.find('input[type=checkbox]').attr('id', 'checkbox-' + index);
    newRow.find('label.btn-checkbox').attr('for', 'checkbox-' + index);
    newRow.find("select").selectpicker('val', '').parent().siblings("button").remove();
    newRow.data('id', 0);
    $(".bootstrap-select").removeClass("open").find("*").attr("aria-expanded", false);
    OrderIndex();
})

function OrderIndex() {
    $.each($('#tblPositionQuota tbody tr'), function () {
        let index = $(this).index();
        $(this).find('.stt').text(index+1);
    })
}

$(document).on('change', '#tblPositionQuota tr th #checkbox-all', function () {
    if ($(this).is(':checked')) {
        $('input:checkbox').prop('checked', true);
    }
    else {
        $('input:checkbox').prop('checked', false);
    }
})

$(document).on('change', '#tblPositionQuota input[type=checkbox]', function () {
    ToggleTrashButton();
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
        }
    },
    messages: {
        Code: {
            required: "Mã chức vụ không được để trống",
            minlength: "Mã chức vụ tối thiểu 2 ký tự",
            maxlength: "Mã chức vụ tối đa 20 ký tự"
        },
        Name: {
            required: "Tên chức vụ không được để trống",
            minlength: "Tên chức vụ tối thiểu 2 ký tự",
            maxlength: "Tên chức vụ tối đa 32 ký tự"
        }
    }
});

$('.submit').on('click', function () {
    if (!_$form.valid()) {
        return false;
    }
    let data = GetDataForm();
    let position = _$form.serializeFormToObject();
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Positions/CreateOrUpdate",
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
                if (position.Id > 0) {
                    abp.notify.success("Lưu thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Positions/Index');
                    }, 500);
                }
                else {
                    abp.notify.success("Thêm mới thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Positions/Index');
                    }, 500);
                }
            }
        }
    });
})

function GetDataForm() {
    let data = new FormData();

    let position = _$form.serializeFormToObject();
    let generalInfo = {};
    generalInfo.id = position.Id;
    generalInfo.code = position.Code;
    generalInfo.name = position.Name;
    generalInfo.status = position.Status;
    data.append("generalInfo", JSON.stringify(generalInfo));

    let listQuotas = new Array();
    $.each($('#tblPositionQuota tbody tr'), function () {
        let ipn = {};
        ipn.id = $(this).data('id');
        ipn.bookClassifyId = $(this).find('select[name=BookClassifyId]').val();
        ipn.amount = $(this).find('input[name=Amount]').val();
        if (ipn.bookClassifyId > 0)
            listQuotas.push(ipn);
    })
    if (listQuotas.length > 0)
        data.append("listQuotas", JSON.stringify(listQuotas));

    return data;
}
