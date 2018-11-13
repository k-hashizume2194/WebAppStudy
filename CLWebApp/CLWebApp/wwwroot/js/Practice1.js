    // Write your JavaScript code.
    // HTMLの読み込みが完了したら動く
$(function () {

    // btnCalculationEnabledがfalseなら計算ボタンをdisabledにする
    btnCalcChange();

     $('#calcBtn').on('click', function () {
         var result = window.confirm('計算処理を実行します。よろしいですか？');
         if (!result) {
             return;
         }
     });


    $("#weight, #height").on('change', function () {

        var heightVal = $('#height').val();
        var weightVal = $('#weight').val();

        // ・未入力の場合：以下の処理を実行して処理終了
        //「計算」ボタンをクリック不可状態にする
        if (!heightVal || !weightVal) {
            // 計算ボタンをクリック不可にする
            $('#calcBtn').prop('disabled', true);
            //$('#weight').focus();
            // 処理終了
            return;
        }
        else {
            // 計算ボタンをクリック可能にする
            $('#calcBtn').prop('disabled', false);
            $('#calcBtn').focus();
        }
    });


});

//計算ボタンの押印を制御する関数
function btnCalcChange() {
    // ①btnCalculationEnabledの値をとる
    var hiddenVal = $('#btnCalculationEnabled').val();
    // 文字列のboolean判定
    var btnCalculationEnabledval = hiddenVal.toLowerCase() === "true";
    // ②ボタンのdisabledを①の値をもとに切り替え]
    $('#calcBtn').prop('disabled', !btnCalculationEnabledval);
}