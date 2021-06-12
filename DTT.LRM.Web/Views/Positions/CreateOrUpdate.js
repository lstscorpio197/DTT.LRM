let _$form = $('form[name=PositionForm]')
let rowQuotaDefault = $('#tblPositionQuota tbody tr:first-child').clone();

$(document).on('click', '.add-row', function () {
    let newRow = rowQuotaDefault.clone().appendTo('#tblPositionQuota');
    newRow.find('select').selectpicker('refresh');
    let index = newRow.index();
    newRow.find('input[type=checkbox]').attr('id', 'checkbox-' + index);
    newRow.find('label.btn-checkbox').attr('for', 'checkbox-' + index);
    OrderIndex();
})

function OrderIndex() {
    $.each($('#tblPositionQuota tbody tr'), function () {
        let index = $(this).index();
        $(this).find('.stt').text(index+1);
    })
}

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
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Positions/CreateOrUpdate",
        data: data,
        contentType: false,
        processData: false,
        success: function (res) { }
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
        ipn.bookClassifyId = $(this).find('select[name=BookClassifyId]').val();
        ipn.amount = $(this).find('input[name=Amount]').val();
        if (ipn.bookClassifyId > 0)
            listQuotas.push(ipn);
    })
    if (listQuotas.length > 0)
        data.append("listQuotas", JSON.stringify(listQuotas));

    return data;
}
