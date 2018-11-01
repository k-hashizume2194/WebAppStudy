// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {
    //テキストボックスにフォーカスが当たった時に実行
    $("input[type='text']").focus()
        .click(function () {
            $(this).select();
            return false;
        });

    $('.datepicker').datepicker({
        autoclose: 'true',
        format: 'yyyy/mm/d',
        language: 'ja',
        todayHighlight: true
    });
});