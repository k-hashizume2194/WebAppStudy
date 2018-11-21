// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {
    $('#ajaxRecBtn').on('click', function () {
        // 対象ボタンの親のForm取得
        //var form = $('#calcForm');

        //$.ajax({
        var aaa = $('#ParkingInfoList').val();

        var bbb;


        //})
        
        // ajax処理

    //    // 処理前に Loading 画像を表示
    //    dispLoading("処理中...");

    //    // formの内容をシリアライズ(※要name属性)
    //    var dataToPost = form.serialize();

    //    $.ajax({
    //        url: '/Practice2/CalcAjax',
    //        type: 'POST',
                        //data: {
                //    'userid': $('#userid').val(),
                //    'passward': $('#passward').val()
                //}

    //        data: dataToPost
    //    })
    //        // Ajaxリクエストが成功した時発動

    //        .done(function (data) {
    //            // メッセージを表示
    //            alert(data.message);
    //            if (data.status === "success") {
    //                // 処理成功の場合は画面を再表示
    //                $('#Winning').val(data.winning);
    //            }
    //        })
    //        // Ajaxリクエストが失敗した時発動
    //        .fail(function (data) {
    //            alert('Ajaxリクエストエラーが発生しました');
    //        })
    //        // Ajaxリクエストが成功・失敗どちらでも発動
    //        .always(function (data) {
    //            // Lading 画像を消す
    //            removeLoading();
    //        });
    });
});