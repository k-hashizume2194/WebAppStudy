$(document).ready(function () {
    // 対象：打数, 安打数テキストボックス
    $('#Bats, #Hits')
        // フォーカス時,クリック時に全選択にする
        .focus(function () {
            $(this).select();
        })
        .click(function () {
            $(this).select();
            return false;
        });
}); 