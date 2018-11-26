// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {
    // 実行ボタンクリック処理
    $('#ajaxRecBtn').on('click', function () {

        // 駐車場情報リストの値を取得
        var parkingIdVal = $('#ParkingInfoList').val();

        if (!parkingIdVal) {
            alert('駐車場を選択してください');
            return;
        }

        // 処理前に Loading 画像を表示
        dispLoading("処理中...");

        //ajax処理
        $.ajax({
            url: '/Practice2/ParkingInfoDisplayAjax',
            type: 'POST',
            data: {
                // ParkingInfoListのId情報
                'userid': parkingIdVal
            }
        })

        // Ajaxリクエストが成功した時発動
        .done(function (data) {
            if (data.status === "success") {
                // 処理成功の場合はフォームに取得したパーキング情報から各情報をセット
                $('.dataForm1').val(data.parkingInfomation.parkingName);
                $('.dataForm2').val(data.parkingInfomation.timeRate);
                $('.dataForm3').val(data.parkingInfomation.fee);
                $('.dataForm4').val(data.parkingInfomation.maxFee);
                $('.dataForm5').val(data.parkingInfomation.location);
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
    });


    // ボタン押下処理(最大、最小、平均の各ボタン)
    $('.CalcProcessing').on('click', function () {
         //ajax処理
        $.ajax({
            url: '/Practice2/ParkingCalc',
            type: 'POST',
            data: {
                // 選択しているボタンの要素を取得
                'btn' : $(this).data('btn')
            }
        })
        // Ajaxリクエストが成功した時発動
        .done(function (data) {
            if (data.status === "success") {
                // 処理成功の場合はフォームに取得したパーキング情報から各情報をセット
                $('.dataForm1').val(data.parkingInfomation.parkingName);
                $('.dataForm2').val(data.parkingInfomation.timeRate);
                $('.dataForm3').val(data.parkingInfomation.fee);
                $('.dataForm4').val(data.parkingInfomation.maxFee);
                $('.dataForm5').val(data.parkingInfomation.location);
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
    });


});