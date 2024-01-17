function FillYBSTypesByYBSCompany(lstYBSCompanyCtrl, lstYBSTypeId) {

    var lstYBSTypes = $("#" + lstYBSTypeId);
    lstYBSTypes.empty();

    var selectedYBSCompany = lstYBSCompanyCtrl.options[lstYBSCompanyCtrl.selectedIndex].value;

    if (selectedYBSCompany != null && selectedYBSCompany != '') {
        $.getJSON("/VehicleData/GetYBSTypeByYBSCompanyId", { ybsCompanyId: selectedYBSCompany }, function (ybsTypes) {
            if (ybsTypes != null && !jQuery.isEmptyObject(ybsTypes)) {
                $.each(ybsTypes, function (index, ybsType) {
                    lstYBSTypes.append($('<option/>',
                        {
                            value: ybsType.value,
                            text: ybsType.text
                        }));
                });
            };
        });
    }

    return;
}


$(document).ready(function () {

    var alertBox = $(".alert");

    if (alertBox.length > 0) {
        setTimeout(function () {
            alertBox.fadeOut(500);
        }, 3000);
    }
});

$(document).ready(function () {
    $('#createBtn').click(function () {
        $('#edit').hide();
        $('#delete').hide();
        $('#create').show();
        
        $('#nameCreate').val('');
        $('#nameCreate').focus();
    });
});

$(document).ready(function () {
    $(document).on('click', '.editBtn', function () {
        $('#create').hide();
        $('#delete').hide();
        $('#edit').show();
        
        var name = $(this).closest('tr').find('.name').text().trim();
        var pkId = $(this).closest('tr').find('.pkId').children(".Pkid").val();
        $('#editPkId').val(pkId);
        $('#nameEdit').val(name);
    });
});

$(document).ready(function () {
    $(document).on('click', '.deleteBtn', function () {
        $('#create').hide();
        $('#edit').hide();
        $('#delete').show();
       
        var name = $(this).closest('tr').find('.name').text();
        var pkId = $(this).closest('tr').find('.pkId').children(".Pkid").val();
        $('#nameDelete').text(name);
        $('#deletePkId').val(pkId);
    });
});



