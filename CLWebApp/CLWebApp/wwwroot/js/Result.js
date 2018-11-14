// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {
    var yourbmiVal = $('#yourbmi').val();
    var yourbmiNum = Number(yourbmiVal);

    //18.5未満	
    if (yourbmiNum < 18.5) {
        $('#diagnosis').val('痩せてます…しっかり食べて体重を増やしましょう。');
        // 18.5以上25未満	
    } else if (yourbmiNum >= 18.5 && yourbmiNum < 25) {
        $('#diagnosis').val('健康です！！このまま健康的な生活を続けましょう。');
        // 25以上
    } else if (yourbmiNum >= 25) {
        $('#diagnosis').val('肥満です…食べすぎに注意してください。運動をお勧めします。');
    }


});