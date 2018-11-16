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
    if (WinningVal == ".000")
    {
        $("#Winning").css('background-color', '#6495ED');
        $("#Winning").css('color', '#770000');
    }

    var WinningNum = Number(WinningVal);
    // 勝率が"空白"の時は処理しない
    if (WinningNum == "")
    {
        return;
    }
    // 勝率が".499"以下の時、勝率テキストボックス、文字の色を変更
    if (WinningNum <= 0.499)
    {
        $("#Winning").css('background-color', '#6495ED');
        $("#Winning").css('color', '#770000');
    }
    // 勝率が"1.000"の時、勝率テキストボックス、文字の色を変更
    if (WinningNum == 1.000)
    {
        $("#Winning").css('background-color', '#FF6347');
        //$("#Winning").css('color', '#770000');
    }
});