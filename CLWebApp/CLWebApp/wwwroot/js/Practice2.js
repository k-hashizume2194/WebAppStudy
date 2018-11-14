// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {
    //$('#calcBtn').click(function () {
    var WinningVal = $('#Winning').val();
    if (WinningVal == ".000")
    {
        $("#Winning").css('background-color', '#6495ED');
        $("#Winning").css('color', '#770000');
    }

    var WinningNum = Number(WinningVal);
    if (WinningNum == "")
    {
        return;
    }
    if (WinningNum <= 0.499 || WinningNum == 0.000)
    {
        $("#Winning").css('background-color', '#6495ED');
        $("#Winning").css('color', '#770000');
    }
    //});
});