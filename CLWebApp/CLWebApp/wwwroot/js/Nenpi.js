$(function () {

    // 今回走行距離を離れるイベント
    $('#currentMileage').blur(function () {
        $inputval = $(this).val();
        alert($inputval);

        var frmVlalidation = $('#projectCreateForm').valid();

    });
});