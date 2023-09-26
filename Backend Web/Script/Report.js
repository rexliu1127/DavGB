

function OpenChangTransStatus(Status, TransactionId, UserName,Remark) {
    var Url = '';

    Url = '/Report/ChangTransStatus.aspx';
    OpenBPopup('divPopup', Url + '?ToTID=' + TransactionId + '&ToCN=' + UserName + '&Status=' + Status + '&Remark=' + Remark);
}

function DoAuditSearch() {
    document.getElementById("hidDo").value = 'Y';

    var Data = $('#frmDoAudit').serialize();
    SwtichContent('/Report/Audit.aspx', Data);
}


function DoChangeTransStatus(ToTID) {

    var sel = document.getElementById('selChangeNewStatus');
    var selValue = sel.options[sel.selectedIndex].value;
    var txtRemark = document.getElementById('txtMemo').value;
    var Data = { 'ToTID': ToTID, 'NewStatus': selValue, 'txtMemo': txtRemark };

    if (baseHtmlRequest('Report/ProcessChangeTransStatus.ashx', Data, false)) {
        alert(Res_UpdateSuccess);
        CloseBPopup();
        document.getElementById('btnAuditsearch').click();
    }
}


function DoOutstandingSearch()
{
    var selValue = '';
    var UserName = document.getElementById('txtUserName').value;

    var Data = { 'txtUserName': UserName};
    var Url = '/Report/Outstanding.aspx';
    SwtichContent(Url, Data);

}