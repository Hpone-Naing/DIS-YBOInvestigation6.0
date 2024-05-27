function FillDriverLicenseByDriverId(lstDriverName, driverLicenseId, driverIdNumber, totalRecord, ControllerName) {

    var driverLicenseTestBox = $("#" + driverLicenseId);
    var driverIdNumberTestBox = $("#" + driverIdNumber);
    driverLicenseTestBox.empty();
    var selectedDriverId = lstDriverName.options[lstDriverName.selectedIndex].value;
    if (selectedDriverId == 0) {
        driverLicenseTestBox.val("");
        driverIdNumberTestBox.val("");
        $("#" + totalRecord).val("1");
    }
    if (selectedDriverId != null && selectedDriverId != '') {
        $.getJSON("/YBOInvestigate/" + ControllerName + "/GetDriverLicenseByDriverId", { driverPkId: selectedDriverId }, function (data) {
            driverLicenseTestBox.val(data.license);
            driverIdNumberTestBox.val(data.idNumber);
            if (totalRecord !== '') {
                $("#" + totalRecord).val(data.totalRecord);
            }
        });
    }

    return;
}

function FillDriverInfoByDriverId(lstDriverName) {
    var driverIdNumberTestBox = $("#driverIdNumber");
    var driverLicenseTestBox = $("#driverLicense");
    var totalRecordTestBox = $("#totalRecord");
    var driverAgeTestBox = $("#age");
    var driverAddressTestBox = $("#address");
    var driverFatherNameTestBox = $("#fatherName");
    var driverPhoneTestBox = $("#phone");
    var driverEducationLevelTestBox = $("#educationLevel");
    totalRecordTestBox.val("");
    driverIdNumberTestBox.val("");
    driverLicenseTestBox.val("");
    driverAgeTestBox.val("");
    driverAddressTestBox.val("");
    driverFatherNameTestBox.val("");
    driverPhoneTestBox.val("");
    driverEducationLevelTestBox.val("");
    var selectedDriverId = lstDriverName.options[lstDriverName.selectedIndex].value;

    if (selectedDriverId != null && selectedDriverId != '') {
        $.getJSON("/YBOInvestigate/YBSDriverCourseDelivery/GetDriverInfoByDriverId", { driverPkId: selectedDriverId }, function (data) {
            var license = data.license;
            var idNumber = data.idNumber;
            var totalRecord = data.totalRecord;
            var trainedDriverInfo = data.trainedDriverInfo;

            driverLicenseTestBox.val(license);
            driverIdNumberTestBox.val(idNumber);
            totalRecordTestBox.val(totalRecord);
            if (trainedDriverInfo != null) {
                driverAgeTestBox.val(trainedDriverInfo.age);
                driverAddressTestBox.val(trainedDriverInfo.address);
                driverFatherNameTestBox.val(trainedDriverInfo.fatherName);
                driverPhoneTestBox.val(trainedDriverInfo.phone);
                driverEducationLevelTestBox.val(trainedDriverInfo.educationLevel);
            }
        });
    }

    return;
}

$(document).ready(function () {
    $('#add').click(function () {
        $('#lstDriverName').prop('selectedIndex', 0);
        $('#lstDriverNameDiv').hide();
        $('#add').hide();
        $('#driverLicenseLbl').show();
        $('#driverNameLbl').show();
        $('#ageLbl').show();
        $('#addressLbl').show();
        $('#fatherNameLbl').show();
        $('#educationLevelLbl').show();
        $('#phoneLbl').show();

        $('#newDriverDiv').show(); 
        $('#remove').show();
        $('#driverLicense').prop('readonly', false);
        $('#driverIdNumber').prop('readonly', false); 
        $('#totalRecord').val("1"); 
        $('#driverLicense').val('');
        $('#driverIdNumber').val('');
        $('#newDriverName').val('');
        $('#address').val('');
        $('#age').val('');
        $('#phone').val('');
        $('#educationLevel').val('');
        $('#fatherName').val('');
    });

    $('#remove').click(function () {
        $('#lstDriverNameDiv').show();
        $('#lstDriverName').prop('selectedIndex', 0);
        $('#add').show();
        $('#newDriverName').val('');
        $('#newDriverDiv').hide();
        $('#remove').hide();
        $('#driverLicenseLbl').hide();
        $('#driverNameLbl').hide();
        $('#ageLbl').hide();
        $('#addressLbl').hide();
        $('#fatherNameLbl').hide();
        $('#educationLevelLbl').hide();
        $('#phoneLbl').hide();
        $('#driverLicense').prop('readonly', true);
        $('#driverIdNumber').prop('readonly', true); 
        $('#totalRecord').val("1"); 
    });
});
