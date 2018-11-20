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
// DataTables日本語設定
$.extend($.fn.dataTable.defaults, {
    language: {
        "sEmptyTable": "テーブルにデータがありません",
        "sInfo": " _TOTAL_ 件中 _START_ から _END_ まで表示",
        "sInfoEmpty": " 0 件中 0 から 0 まで表示",
        "sInfoFiltered": "（全 _MAX_ 件より抽出）",
        "sInfoPostFix": "",
        "sInfoThousands": ",",
        "sLengthMenu": "_MENU_ 件表示",
        "sLoadingRecords": "読み込み中...",
        "sProcessing": "処理中...",
        "sSearch": "検索:",
        "sZeroRecords": "一致するレコードがありません",
        "oPaginate": {
            "sFirst": "先頭",
            "sLast": "最終",
            "sNext": "次",
            "sPrevious": "前"
        },
        "oAria": {
            "sSortAscending": ": 列を昇順に並べ替えるにはアクティブにする",
            "sSortDescending": ": 列を降順に並べ替えるにはアクティブにする"
        }
    }
}); 