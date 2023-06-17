
$('#checkbox1').change(function() {
    $('#textbox1').val($(this).is(':checked'));
  });

$(function () {
        var rowcount = '@rowCount';
        console.log($('#selectedStatusId').val());
        if ($('#selectedStatusId').val() == "S") {
            if (rowcount > 0) {
                $('#btnReceiptSelected').attr('disabled', true);
            } else {
                $('#btnReceiptSelected').attr('disabled', false);
            }
        }
 });

