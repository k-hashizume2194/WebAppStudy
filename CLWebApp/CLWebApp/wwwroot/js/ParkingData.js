// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {
    $('#execBtn').click(function () {
    // ajax処理

        //// 処理前に Loading 画像を表示
        //dispLoading("処理中...");

        $.ajax({
            url: '/Practice1/SelectData',
            type: 'GET',
            data: {
                'id': $('#ParkingSelect').val(),
            }
        })
            // Ajaxリクエストが成功した時発動

            //.done((data) => { ※アロー式">"を使用するとIE11でエラーとなる
            .done(function (data) {
                if (data.status === "success") {
                    // ラベルにJSONで帰ってきたものをセット
                    $('#parkingName').val(data.pInfo.parkingName);
                    $('#timeRate').val(data.pInfo.timeRate);
                    $('#fee').val(data.pInfo.fee);
                    $('#maxFee').val(data.pInfo.maxFee);
                    $('#location').val(data.pInfo.location);
                }
            })
            // Ajaxリクエストが失敗した時発動
            .fail(function (data) {
                alert('Ajaxリクエストエラーが発生しました');
            })
            // Ajaxリクエストが成功・失敗どちらでも発動
            .always(function (data) {
                //// Lading 画像を消す
                //removeLoading();
            });

            })

});