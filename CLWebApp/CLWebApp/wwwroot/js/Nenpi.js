$(function () {

    // 今回走行距離を離れるイベント
    $('#currentMileage').change(function () {

        var frmVlalidation = $('#calcForm').valid();

        // 今回走行距離
        var inputval = $(this).val();

        // 前回走行距離
        var zenkaiVal = $('#pastMileage').val();

        // 区間走行距離
        var kukan = '';
        $('#btnCalculation').attr('disabled', true);

        // TODO:今回走行距離のviewmodelエラーチェックが問題ない場合のみ計算したい
        // asp-validation-for="currentMileage"に何か入っているかで判定できるか？
        var currentMileageError = $('#currentMileage-error').text();

        if (!currentMileageError) {
            kukan = Number(inputval) - Number(zenkaiVal);
            $('#btnCalculation').removeAttr('disabled');
            $('#btnCalculation').focus();
        }
        $('#thisMileage').val(kukan);

    });

    // TODO:初期フォーカスが設定できない
    $('#dataTimePicker').focus();
});