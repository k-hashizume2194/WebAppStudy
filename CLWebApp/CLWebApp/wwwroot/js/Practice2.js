// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {

    // 引き分け数が""の時、フォーカスアウト時にゼロをセット
    $('#Draw').blur(function () {
        var DrawVal = $('#Draw').val();
        if (DrawVal == "") {
            $('#Draw').val(0)
        }
    });

    // 勝率が".000"の時、勝率テキストボックス、文字の色を変更
    var WinningVal = $('#Winning').val();
    if (WinningVal == ".000") {
        $("#Winning").css('background-color', '#6495ED');
        $("#Winning").css('color', '#770000');
    }

    var WinningNum = Number(WinningVal);
    // 勝率が"空白"の時は処理しない
    if (WinningNum != "") {
        // 勝率が".499"以下の時、勝率テキストボックス、文字の色を変更
        if (WinningNum <= 0.499) {
            $("#Winning").css('background-color', '#6495ED');
            $("#Winning").css('color', '#770000');
        }
        // 勝率が"1.000"の時、勝率テキストボックス、文字の色を変更
        if (WinningNum == 1.000) {
            $("#Winning").css('background-color', '#FF6347');
            //$("#Winning").css('color', '#770000');
        }
    }

    $('#calcAjaxBtn').on('click', function () {
        // 対象ボタンの親のForm取得
        var form = $('#calcForm');

        // ajax処理

        // 処理前に Loading 画像を表示
        dispLoading("処理中...");

        // formの内容をシリアライズ(※要name属性)
         var dataToPost = form.serialize();

        $.ajax({
            url: '/Practice2/CalcAjax',
            type: 'POST',
            //data: {
            //    'userid': $('#userid').val(),
            //    'passward': $('#passward').val()
            //}
            //data: {
            //    'Victory': $('#Victory').val(),
            //    'Defeat': $('#Defeat').val(),
            //    'Draw': $('#Draw').val(),
            //    'Winning': $('#Winning').val(),
            //    dataToPost
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
                    //location.href = 'Winning';
                    $('#Winning').val(data.winning);
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