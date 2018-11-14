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

    $('.datepicker').datepicker({
        autoclose: 'true',
        format: 'yyyy/mm/dd',
        language: 'ja',
        todayHighlight: true
    });

    // jQueryのDateTimePicker
    $.datetimepicker.setLocale('ja');
    $('.jqdatetimepicker').datetimepicker();
});