// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {
    //テキストボックスにフォーカスが当たった時に実行
    //$("input[type='text']").focus()
    //    .click(function () {
    //        $(this).select();
    //        return false;
    //    });
    // ※↑この書き方ではfocusが当たった箇所を自動的にクリックしてしまう

    // テキストボックスにフォーカスが当たった時に実行
    $("input[type='text']").on('click focus', function () {
        $(this).select();
        return false;
    });

    // bootstrap-datepicker
    var bootstrapDatepicker = $.fn.datepicker.noConflict();
    $.fn.bootstrapDP = bootstrapDatepicker;
    $('.datepicker').bootstrapDP({
        autoclose: 'true',
        format: 'yyyy/mm/dd',
        language: 'ja',
        todayHighlight: true
    });

    //// jQueryのDateTimePicker
    //$.datetimepicker.setLocale('ja');
    //$('.jqdatetimepicker').datetimepicker();
});

$(window).load(function () {
    // コンテンツ全体の読み込み完了
    // Lading 画像を消す
    removeLoading();
});

// ロード中gifを削除
function removeLoading() {
    $("#loading").remove();
}

// ロード中gifを画面中央に表示
function dispLoading(msg) {
    if (msg === undefined) {
        msg = "";
    }
    // 画面表示メッセージ
    var dispMsg = "<div class='loadingMsg'>" + msg + "</div>";
    // ローディング画像が表示されていない場合のみ出力
    if ($("#loading").length === 0) {
        $("body").append("<div id='loading' style='z-index: 9999;'>" + dispMsg + "</div>");
    }
}