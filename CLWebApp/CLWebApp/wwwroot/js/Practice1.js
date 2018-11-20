    // Write your JavaScript code.
    // HTMLの読み込みが完了したら動く
$(function () {

    // btnCalculationEnabledがfalseなら計算ボタンをdisabledにする
    btnCalcChange();

    // isCalclatedがfalseなら身長、体重をdisabledにする
    CalculatedChange();

    //計算ボタンの押印を制御する関数
    $("#weight, #height").on('change', function () {

        var heightVal = $('#height').val();
        var weightVal = $('#weight').val();

        // ・未入力の場合：以下の処理を実行して処理終了
        //「計算」ボタンをクリック不可状態にする
        if (!heightVal || !weightVal) {
            // 計算ボタンをクリック不可にする
            $('#calcBtn, #ajaxCalcBtn').prop('disabled', true);
            //$('#weight').focus();
            // 処理終了
            return;
        }

        // TODO:数字以外を入力したときに計算ボタンを有効にしない処理

        //// 指定フォームのvalidate実行
        //var formResult = $(this).parents('form').valid();
        //if (!formResult) {
        //    // エラーがあるので処理終了
        //    return;
        //}

        // 計算ボタンをクリック可能にする
        $('#calcBtn, #ajaxCalcBtn').prop('disabled', false);
        $('#calcBtn').focus();
        
    });

    //// ひと画面でボタンを切り替える(Ajax不使用)
    //$('.submit_button').on('click', function () {
    //    // 対象ボタンの親のForm取得
    //    var form = $(this).parents('form');
    //    // data属性(-action)から値を取得してクリックしたもののactionに書き換え
    //    form.attr('action', $(this).data('action'));
    //    // data属性(-btn)から値を取得してifで切り替え
    //    // 記録をクリックしたときはconfirm、計算をクリックしたときはそのまま
    //    var btn = $(this).data('btn');
    //    if (btn == "recBtn") {
    //        // ①isCalculatedの値をとる
    //        var hiddenVal = $('#isCalculated').val();
    //        // 文字列のboolean判定
    //        var isnotCalculatedVal = hiddenVal.toLowerCase() === "false";
    //        // ②①の値をもとにBMI計算をしてなければ、アラートを出す
    //        if (isnotCalculatedVal) {
    //            alert('記録処理はBMIの算出後に実行してください');
    //            return;
    //        }
    //        // 記録ボタンの場合
    //        var result = window.confirm('記録処理を実行します。よろしいですか？');
    //        if (!result) {
    //            return;
    //        }
    //    }
    //    form.submit();
    //});         


    // 計算(Ajax)ボタンを押したとき
    $('#ajaxCalcBtn').click(function () {
        {
            // 対象ボタンの親のForm取得
            var form = $(this).parents('form');

            //TODO:Ajax前にform.valid();必要？
            // ajax処理

            // 処理前に Loading 画像を表示
            dispLoading("処理中...");

            // formの内容をシリアライズ(※要name属性)
            var dataToPost = form.serialize();

            $.ajax({
                url: '/Practice1/CalcAjax',
                type: 'POST',
                data: dataToPost
            })
                // Ajaxリクエストが成功した時発動

                .done(function (data) {
                    if (data.status === "success") {
                        // BMIの値をセット
                        $("#bmi").val(data.result);
                        // hidden(btnCalculationEnabled)の値を書き換え
                        $('#btnCalculationEnabled').val(data.hiddenCalc);
                        //計算ボタンの押印を制御する関数
                        btnCalcChange();
                        // hidden(isCalculated)の値を書き換え
                        $('#isCalculated').val(data.hiddenRec);
                        //計算後、入力を制御する関数
                        CalculatedChange();
                        // 日時変更不可にするためカレンダーを非表示
                        $('#measuringdate').datetimepicker('destroy');
                    }
                })
                    // Ajaxリクエストが失敗した時発動
                    .fail(function (data) {
                        alert('Ajaxリクエストエラーが発生しました');
                    })
                    // Ajaxリクエストが成功・失敗どちらでも発動
                    .always(function (data) {
                        // Lading 画像を消す
                        removeLoading();
                    });
        
        }
    });


    // ひと画面でボタンを切り替える(Ajax使用)
    $('.submit_button').on('click', function () {
        // 対象ボタンの親のForm取得
        var form = $(this).parents('form');
        // data属性(-action)から値を取得してクリックしたもののactionに書き換え
        form.attr('action', $(this).data('action'));
        // data属性(-btn)から値を取得してifで切り替え
        // 記録または記録(Ajax)をクリックしたときはconfirm、計算をクリックしたときはそのまま
        var btn = $(this).data('btn');
        // 記録または記録(Ajax)ボタンなら
        if (btn === "recBtn" || btn === "ajaxRecBtn")
        {
            // ①isCalculatedの値をとる
            var hiddenVal = $('#isCalculated').val();
            // 文字列のboolean判定
            var isnotCalculatedVal = hiddenVal.toLowerCase() === "false";
            // ②①の値をもとにBMI計算をしてなければ、アラートを出す
            if (isnotCalculatedVal) {
                alert('記録処理はBMIの算出後に実行してください');
                return;
            }
            // 記録ボタンの場合
            var result = window.confirm('記録処理を実行します。よろしいですか？');
            if (!result) {
                return;
            }
        }
        if (btn !== "ajaxRecBtn") {
            // 対象ボタンの親のFormをsubmit
            form.submit();
        } else {
            // ajax処理

            // 処理前に Loading 画像を表示
            dispLoading("処理中...");

            // formの内容をシリアライズ(※要name属性)
            var dataToPost = form.serialize();

            $.ajax({
                url: '/Practice1/RecordAjax',
                type: 'POST',
                //data: {
                //    'userid': $('#userid').val(),
                //    'passward': $('#passward').val()
                //}
                data: dataToPost
            })
                // Ajaxリクエストが成功した時発動

                //.done((data) => { ※アロー式">"を使用するとIE11でエラーとなる
                .done(function (data) {
                    // メッセージを表示
                    alert(data.message);
                    if (data.status === "success") {
                        // 処理成功の場合は画面を再表示
                        location.href = '/Practice1/Bmi';
                    }
                })
                // Ajaxリクエストが失敗した時発動
                .fail(function (data) {
                    alert('Ajaxリクエストエラーが発生しました');
                })
                // Ajaxリクエストが成功・失敗どちらでも発動
                .always(function (data) {
                    // Lading 画像を消す
                    removeLoading();
                });
        }
    });         

    // CalculatedChange(); より後に記述することによってカレンダーのclass外しを有効にする
    $.datetimepicker.setLocale('ja');
    $('.jqdatetimepicker').datetimepicker();
});

//計算ボタンの押印を制御する関数(false:押下不可)
function btnCalcChange() {
    // ①btnCalculationEnabledの値をとる
    var hiddenVal = $('#btnCalculationEnabled').val();
    // 文字列のboolean判定
    var btnCalculationEnabledval = hiddenVal.toLowerCase() === "true";
    // ②ボタンのdisabledを①の値をもとに切り替え]
    $('#calcBtn, #ajaxCalcBtn').prop('disabled', !btnCalculationEnabledval);
}

//計算後、入力を制御する関数(true:ResBtn押下可)
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