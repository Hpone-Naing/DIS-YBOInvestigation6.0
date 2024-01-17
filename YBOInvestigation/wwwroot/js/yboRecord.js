function FillYBSTypesByYBSCompany(lstYBSCompanyCtrl, lstYBSTypeId) {

    var lstYBSTypes = $("#" + lstYBSTypeId);
    lstYBSTypes.empty();

    var selectedYBSCompany = lstYBSCompanyCtrl.options[lstYBSCompanyCtrl.selectedIndex].value;

    if (selectedYBSCompany != null && selectedYBSCompany != '') {
        $.getJSON("/YBORecord/GetYBSTypeByYBSCompanyId", { ybsCompanyId: selectedYBSCompany }, function (ybsTypes) {
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

