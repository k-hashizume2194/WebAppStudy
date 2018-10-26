// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {
    $(document).ready(function () {
        //テキストボックスにフォーカスが当たった時に実行
        $("input[type='text']").focus()
            .click(function () {
                $(this).select();
                return false;
            });
    });
});