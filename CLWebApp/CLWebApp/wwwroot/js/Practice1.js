    // Write your JavaScript code.
    // HTMLの読み込みが完了したら動く
$(function () {



    // btnCalculationEnabledがfalseなら計算ボタンをdisabledにする
    btnCalcChange();

    // isCalclatedがfalseなら身長、体重をdisabledにする
    CalculatedChange();

     //$('#calcBtn').on('click', function () {
     //    var result = window.confirm('計算処理を実行します。よろしいですか？');
     //    if (!result) {
     //        return;
     //    }
     //});

    //計算ボタンの押印を制御する関数
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

    //計算後、入力を制御する関数
    $('.submit_button').on('click', function () {
        // 対象ボタンの親のForm取得
        var form = $(this).parents('form');
        // ①isCalculatedの値をとる
        var hiddenVal = $('#isCalculated').val();
        // 文字列のboolean判定
        var isnotCalculatedVal = hiddenVal.toLowerCase() === "false";
        // ②①の値をもとに区間燃費が未入力の場合、アラートを出す
        if (isnotCalculatedVal) {
            alert('記録処理はBMIの算出後に実行してください');
            return;
        }
        var result = window.confirm('記録処理を実行します。よろしいですか？');
        if (!result) 
        {
            return;
        }
        form.submit();
    });       

    ////すぐにダイアログが開かないようにautoOpen:falseを指定
    //$("#dialog").dialog({ autoOpen: false });
    ////ボタンがクリックされたらダイアログを開く
    //$("#opener").click(function () {
    //    $("#dialog").dialog("open");
    //});

    $.datetimepicker.setLocale('ja');
    $('.jqdatetimepicker').datetimepicker();
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

//計算後、入力を制御する関数
function CalculatedChange() {
    // ①isCalculatedの値をとる
    var hiddenVal = $('#isCalculated').val();
    // 文字列のboolean判定
    var isCalculatedVal = hiddenVal.toLowerCase() === "true";
    // ②①の値をもとに給油日、給油量、給油時総走行距離の入力部品の状態を切り替える
    $('#measuringdate, #height, #weight').prop('readonly', isCalculatedVal);
    $('#ResBtn').prop('disabled', !isCalculatedVal);

    // 測定日時カレンダーのreadonly時制御
    // readonlyにする個所でカレンダーのclassを外し、
    // readonly解除時は、(カレンダーのクラスが無ければ)追加する
    if (isCalculatedVal) {
        $("#measuringdate").removeClass("jqdatetimepicker");
    }
    else {
        $("#measuringdate").addClass("jqdatetimepicker");
    }
}