// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {

   // btnCalculationEnabledがfalseなら計算ボタンをdisabledにする

   // ①btnCalculationEnabledの値をとる
    var hiddenVal = $('#btnCalculationEnabled').val();
   // 文字列のboolean判定
   var btnCalculationEnabledval = hiddenVal.toLowerCase() === "true";

   // ②ボタンのdisabledを①の値をもとに切り替え]
   $('#calcBtn').prop('disabled', !btnCalculationEnabledval);

});
