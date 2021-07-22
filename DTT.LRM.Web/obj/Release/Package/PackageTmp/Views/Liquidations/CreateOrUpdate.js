
let _$form = $('form[name=LiquidationForm]')

let rowDefault = $('#tblBookLiquidation tbody tr:first-child').clone();
let liquidationId = $('#id').val();

rowDefault.find('input').val('');

$(document).on('click', '.add-row', function () {
    let newRow = rowDefault.clone().appendTo('#tblBookLiquidation');
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
    $.each($('#tblBookLiquidation tbody tr'), function () {
        let index = $(this).index();
        $(this).find('.stt').text(index + 1);
    })
}

$(document).on('change', '#tblBookLiquidation tr th #checkbox-all', function () {
    if ($(this).is(':checked')) {
        $('input:checkbox').prop('checked', true);
    }
    else {
        $('input:checkbox').prop('checked', false);
    }
})

$(document).on('change', '#tblBookLiquidation input[type=checkbox]', function () {
    ToggleTrashButton();
})

function ToggleTrashButton() {
    let countChecked = $('#tblBookLiquidation tbody').find('input[type=checkbox]:checked').length;
    if (countChecked > 0)
        $('.btn-trash-row').removeClass('hide');
    else
        $('.btn-trash-row').addClass('hide');
}

$(document).on('click', '.btn-trash-row', function () {
    $.each($('#tblBookLiquidation tbody tr input[type=checkbox]:checked'), function (i) {
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
        Creator: {
            required: true,
            minlength: 2,
            maxlength: 32
        }
    },
    messages: {
        Code: {
            required: "Mã phiếu thanh lý không được để trống",
            minlength: "Mã phiếu thanh lý tối thiểu 2 ký tự",
            maxlength: "Mã phiếu thanh lý tối đa 20 ký tự"
        },
        Creator: {
            required: "Tên người tạo phiếu không được để trống",
            minlength: "Tên người tạo phiếu tối thiểu 2 ký tự",
            maxlength: "Tên người tạo phiếu tối đa 32 ký tự"
        }
    }
});

$('.submit').on('click', function () {
    let status = $(this).data('status');
    if (!_$form.valid()) {
        return false;
    }
    let data = GetDataForm();
    let liquidation = _$form.serializeFormToObject();
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Liquidations/CreateOrUpdate",
        data: data,
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.result == 0) {
                abp.notify.warn("Mã phiếu đã tồn tại");
            }
            else if (res.result == -1) {
                abp.notify.error("Xảy ra lỗi");
            }
            else if (res.result == -2) {
                abp.notify.error("Vui lòng chọn thông tin sách thanh lý");
            }
            else {
                if (liquidation.Id > 0) {
                    abp.notify.success("Lưu thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Liquidations/Index');
                    }, 500);
                }
                else {
                    abp.notify.success("Thêm mới thành công");
                    setTimeout(function () {
                        window.location = abp.toAbsAppPath('Liquidations/Index');
                    }, 500);
                }
            }
        }
    });
})

function GetDataForm() {
    let data = new FormData();
    let liquidation = _$form.serializeFormToObject();
    let generalInfo = {};
    generalInfo.id = liquidation.Id;
    generalInfo.code = liquidation.Code;
    generalInfo.creator = liquidation.Creator;
    generalInfo.description = liquidation.Description;
    generalInfo.createDate = liquidation.CreateDate;
    data.append("generalInfo", JSON.stringify(generalInfo));

    let listBooks = new Array();
    $.each($('#tblBookLiquidation tbody tr'), function () {
        let ipn = {};
        ipn.id = $(this).data('id');
        ipn.bookCategorieId = $(this).find('select[name=BookCategoryId]').val();
        ipn.author = $(this).find('input.author').val().trim();
        ipn.publisherId = $(this).find('select[name=PublisherId]').val();
        ipn.releaseYear = $(this).find('input.releaseyear').val().trim();
        ipn.liquidationPrice = $(this).find('input.liquidationprice').val().trim();
        if (ipn.bookCategorieId > 0)
            listBooks.push(ipn);
    })
    if (listBooks.length > 0)
        data.append("listBooks", JSON.stringify(listBooks));

    return data;
}