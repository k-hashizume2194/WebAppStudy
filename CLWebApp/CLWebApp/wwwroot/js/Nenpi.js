// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {

    // btnCalculationEnabledがfalseなら計算ボタンをdisabledにする
    btnCalcChange();

    // isCalclatedがfalseなら給油日、給油量、給油時総走行距離をreadonlyにする
    CalculatedChange();

    // ひと画面でボタンを切り替える
    $('.submit_button').on('click', function () {
        // 対象ボタンの親のForm取得
        var form = $(this).parents('form');
        // data属性(-action)から値を取得してクリックしたもののactionに書き換え
        form.attr('action', $(this).data('action'));
        // data属性(-btn)から値を取得してifで切り替え
        // 記録をクリックしたときはconfirm、計算をクリックしたときはそのまま
        var btn = $(this).data('btn');
        if (btn === "recBtn" || btn === "ajaxRecBtn")
        {
            // ①isCalculatedの値をとる
            var hiddenVal = $('#isCalculated').val();
            // 文字列のboolean判定
            var isnotCalculatedVal = hiddenVal.toLowerCase() === "false";
            // ②①の値をもとに区間燃費が未入力の場合、アラートを出す
            if (isnotCalculatedVal) {
                alert('記録処理は区間燃費の算出後に実行してください');
                return;
            }
            // 区間燃費が表示されている場合
            // 記録ボタンの場合
            var result = window.confirm('記録処理を実行します。よろしいですか？');
            if (!result)
            {
                return;
            }
        }
        if (btn !== "ajaxRecBtn") {
            // 対象ボタンの親のFormをsubmit
            form.submit();
        } else {
            // ajax処理

            // formの内容をシリアライズ(※要name属性)
            var dataToPost = form.serialize();

            $.ajax({
                url: '/Nenpi/RecordAjax',
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
                    location.href = '/Nenpi/Index';
                }
            })
            // Ajaxリクエストが失敗した時発動
            .fail(function (data) {
                alert('Ajaxリクエストエラーが発生しました');
            })
            // Ajaxリクエストが成功・失敗どちらでも発動
            .always(function (data) {
            });
        }
        
    });       

    // 給油時走行距離のchangeイベントを定義


    // イベントの中でやること

    ////1.給油時総走行距離の入力チェックを行う																																	
    //string kyuyuzitMileage = txtCurrentMileage.Text;
    //double zenkaiMileage = double.Parse(txtPastMileage.Text);

    ////   1 - 1.未入力チェック
    ////   ・未入力の場合：以下の処理を実行して処理終了
    //if (!string.IsNullOrWhiteSpace(kyuyuzitMileage)) {
    //    //nullではなく、かつ空文字列でもなく、かつ空白文字列でもない
    //    //	・入力がある場合：1 - 2へ
    //}
    //else {
    //    //「区間距離」テキストボックスに空白を設定
    //    //「計算」ボタンをクリック不可状態にする
    //    txtThisMileage.Text = "";
    //    btnCalculation.Enabled = false;
    //    return;
    //}
    //// 0以上の数値、整合性チェック
    //string messagekyuyuzi = CheckCurrentMileage(kyuyuzitMileage, zenkaiMileage);

    //if (!string.IsNullOrWhiteSpace(messagekyuyuzi)) {
    //    //エラーだと給油時走行距離にフォーカスを設定して空白セット、
    //    //区間距離空白と計算ボタン不可設定してメッセージ表示
    //    txtThisMileage.Text = "";
    //    btnCalculation.Enabled = false;
    //    this.ActiveControl = this.txtCurrentMileage;
    //    txtCurrentMileage.Text = "";
    //    // フォーカスイベントなのでメッセージボックスを最後に配置
    //    MessageBox.Show(messagekyuyuzi);

    //    return;
    //}




    // 区間距離を算出して「区間距離」テキストボックスに設定
    $("#currentMileage").on('change', function () {

        var currentMileageVal = $('#currentMileage').val();
        var pastMileageVal = $('#pastMileage').val();
      
        // ・未入力の場合：以下の処理を実行して処理終了
        //「計算」ボタンをクリック不可状態にする
        //「区間距離」テキストボックスに空白を設定
        if (!currentMileageVal) {

            // 計算ボタンをクリック不可にする
            $('#calcBtn').prop('disabled', true);
            $('#currentMileage').focus();
            $('#thisMileage').val("");
            // 処理終了
            return;
        }

        // 指定フォームのvalidate実行
        var formResult = $(this).parents('form').valid();
        if (!formResult) {
            // エラーがあるので処理終了
            return;
        }

        // 計算のため数値に変換
        var currentMileageNum = Number(currentMileageVal);
        var pastMileageNum = Number(pastMileageVal);

        // 給油時走行距離が前回距離より小さい場合、アラートを出す。。
        if (currentMileageNum <= pastMileageNum) {
            alert('給油時総走行距離は前回の距離より大きな値で入力してください');
            return;
        }

        // 「給油時走行距離(currentMileage)-前回給油時走行距離(pastMileage)」で、区間走行距離(thisMileage)を算出
        var result = currentMileageNum - pastMileageNum;
        $('#thisMileage').val(result);

        // 計算ボタンをクリック可能にする
        $('#calcBtn').prop('disabled', false);
        $('#calcBtn').focus();
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

//計算後、入力を制御する関数
function CalculatedChange() {
    // ①isCalculatedの値をとる
    var hiddenVal = $('#isCalculated').val();
    // 文字列のboolean判定
    var isCalculatedVal = hiddenVal.toLowerCase() === "true";
    // ②①の値をもとに給油日、給油量、給油時総走行距離の入力部品の状態を切り替える
    $('#dataTimePicker, #boxOilingQuantity, #currentMileage').prop('readonly', isCalculatedVal);

    // 給油日カレンダーのreadonly時制御
    // readonlyにする個所でカレンダーのclassを外し、
    // readonly解除時は、(カレンダーのクラスが無ければ)追加する
    if (isCalculatedVal)
    {
        $("#dataTimePicker").removeClass("datepicker");
    }
    else
    {
        $("#dataTimePicker").addClass("datepicker");
    }
}