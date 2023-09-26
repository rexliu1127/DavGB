function DoInsBank() {
    if (document.getElementById('txtBankName').value == '') {
        alert(enterBankName);
        return;
    }
    if (document.getElementById('txtBankAccount').value == '') {
        alert(enterBankAccount);
        return;
    }
    document.getElementById('frmCreateBank').submit();
}

function OpenBankEdit(Url, AgentID, ToBankID) {

    OpenBPopup('divPopup', Url + '?AgentID=' + AgentID + '&ToBankID=' + ToBankID);

}

function DoEditBank() {

    if (document.getElementById('txtBankName').value == '') {
        alert(enterBankName);
        return;
    }
    if (document.getElementById('txtBankAccount').value == '') {
        alert(enterBankAccount);
        return;
    }

    var Data = $('#frmEdieBank').serialize();
    if (baseHtmlRequest('Bank/UpdBank.aspx', Data, false)) {
        alert(Res_UpdateSuccess);
        CloseBPopup();
        SwtichContent('/Bank/ListBank.aspx');
    }
}