﻿// Write your JavaScript code.
// HTMLの読み込みが完了したら動く
$(function () {

   // btnCalculationEnabledがfalseなら計算ボタンをdisabledにする
    btnCalcChange();

    // 給油時走行距離のchangeイベントを定義



    // イベントの中でやること

    ////1.給油時総走行距離の入力チェックを行う																																	
    //string kyuyuzitMileage = txtCurrentMileage.Text;
    //double zenkaiMileage = double.Parse(txtPastMileage.Text);

    ////   1 - 1.未入力チェック
    ////   ・未入力の場合：以下の処理を実行して処理終了
    //if (!string.IsNullOrWhiteSpace(kyuyuzitMileage)) {
    //    //nullではなく、かつ空文字列でもなく、かつ空白文字列でもない
    //    //	・入力がある場合：1 - 2へ
    //}
    //else {
    //    //「区間距離」テキストボックスに空白を設定
    //    //「計算」ボタンをクリック不可状態にする
    //    txtThisMileage.Text = "";
    //    btnCalculation.Enabled = false;
    //    return;
    //}
    //// 0以上の数値、整合性チェック
    //string messagekyuyuzi = CheckCurrentMileage(kyuyuzitMileage, zenkaiMileage);

    //if (!string.IsNullOrWhiteSpace(messagekyuyuzi)) {
    //    //エラーだと給油時走行距離にフォーカスを設定して空白セット、
    //    //区間距離空白と計算ボタン不可設定してメッセージ表示
    //    txtThisMileage.Text = "";
    //    btnCalculation.Enabled = false;
    //    this.ActiveControl = this.txtCurrentMileage;
    //    txtCurrentMileage.Text = "";
    //    // フォーカスイベントなのでメッセージボックスを最後に配置
    //    MessageBox.Show(messagekyuyuzi);

    //    return;
    //}

    ////区間距離を算出して「区間距離」テキストボックスに設定
    //double kyuyuzidouble = double.Parse(kyuyuzitMileage);
    //double kukankyori = KukanCul(kyuyuzidouble, zenkaiMileage);
    //txtThisMileage.Text = kukankyori.ToString();
    ////計算ボタンにフォーカス
    //this.ActiveControl = this.btnCalculation;

    ////2 - 2.計算ボタンをクリック可能にする
    //btnCalculation.Enabled = true;




});

//ボタンの押印を制御する関数
function btnCalcChange() {
    // ①btnCalculationEnabledの値をとる
    var hiddenVal = $('#btnCalculationEnabled').val();
    // 文字列のboolean判定
    var btnCalculationEnabledval = hiddenVal.toLowerCase() === "true";

    // ②ボタンのdisabledを①の値をもとに切り替え]
    $('#calcBtn').prop('disabled', !btnCalculationEnabledval);
}
