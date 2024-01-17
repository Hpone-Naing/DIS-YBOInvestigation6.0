$(document).ready(function () {
    $('#createBtn').click(function () {
        $('#edit').hide();
        $('#delete').hide();
        $('#create').show();
        $('#manufacturerNameCreate').val('');
        $('#manufacturerNameCreate').focus();
    });
});

$(document).ready(function () {
    $('.editBtn').click(function () {
        $('#create').hide();
        $('#delete').hide();
        $('#edit').show();

        var manufacturerName = $(this).closest('tr').find('.manufacturerName').text().trim();
        var pkId = $(this).closest('tr').find('.pkId').children(".manufacturerPkid").val();
        $('#editManufacturerPkId').val(pkId);
        $('#manufacturerNameEdit').val(manufacturerName);
    });
});

$(document).ready(function () {
    $('.deleteBtn').click(function () {
        $('#create').hide();
        $('#edit').hide();
        $('#delete').show();

        var manufacturerName = $(this).closest('tr').find('.manufacturerName').text();
        var pkId = $(this).closest('tr').find('.pkId').children(".manufacturerPkid").val();
        $('#manufacturerNameDelete').text(manufacturerName);
        $('#deleteManufacturerPkId').val(pkId);
    });
});

function hideForm(elementId) {
    $('#' + elementId).hide();
}
