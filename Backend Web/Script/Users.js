function SetSelectedLan(UserLan) {
    var selLan = document.getElementById('selLan');
    for (var i = 0; i < selLan.length; i++) {
        if (selLan[i].value == UserLan) {
            selLan[i].selected = true;
        }
    }
}

function ChangeLan(selLan) {
 //   setCookie('UserLan', selLan, 30, '', document.domain);
    document.getElementById('hidSelLang').value = selLan;
    document.frmChangeLang.submit();
}

function DoLogin() {
    var UserName = document.getElementById('txtUserName').value;
    var PWD = document.getElementById('txtPWD').value;
    var VCode = document.getElementById('txtValidateCode').value;
    if (UserName == '') {
        document.getElementById('txtUserName').focus();
        alert(err_username);
        return;
    }
    if (PWD == '') {
        document.getElementById('txtPWD').focus();
        alert(err_pwd);
        return;
    }
    if (VCode == '') {
        document.getElementById('txtValidateCode').focus();
        alert(err_validation);
        return;
    }
    var  PostData = { 'UserName': UserName, 'PWD': PWD, 'VCode': VCode };
    if (baseHtmlRequest('ProcessLogin.aspx', PostData, false) ) {
        window.location.href = 'Main.aspx';
    }

}

function DoLoginReset() {
    document.getElementById('txtUserName').value='';
    document.getElementById('txtPWD').value='';
    document.getElementById('txtValidateCode').value='';
}

function DoInsUser()
{ 
    if (document.getElementById('txtUserName').value == '')
    {
        alert(Res_UserNameErr);
        return;
    }
    if (document.getElementById('txtUserPwd').value == '')
    {
        alert(err_enter_pw);
        return;
    }
    if(document.getElementById("txtUserPwd").value.length > 10
        || document.getElementById("txtUserPwd").value.length < 6 ) 
    {
        alert(err_execPassword);
        return;
    }
    
    document.getElementById('txthidUserpwd').value = CFS(document.getElementById('txtUserPwd').value);
    document.getElementById('frmCreateUser').submit();
}

function OpenSubEdit(Url, SubAgentID, ToUserID) {

    OpenBPopup('divPopup', Url + '?SubAgentID=' + SubAgentID + '&ToUserID=' + ToUserID);

}
function OpenChangeSubPWD(ToCID, ToCN, SubAgentID, SubRoleID, Mode) {
    var Url = '';
    switch (Mode) {
        case 1:
            Url = '/User/ChangeSubPWD.aspx';
            break;
        case 2:
            Url = '/Member/ChangeSubPWD.aspx';
            break;
    }
    OpenBPopup('divPopup', Url+'?ToCID=' + ToCID + '&ToCN=' + ToCN + '&SubAgentID=' + SubAgentID + '&SubRoleID=' + SubRoleID);
}

function DoUserSearch(CurrentAgentName, CurrentAgentID, CurrentRoleID, Mode) {


    var selValue = '';
    var UserName = document.getElementById('txtUserName').value;

    if (document.getElementById('selSearchUserStatus') != null) {
        var sel = document.getElementById('selSearchUserStatus');
        selValue = sel.options[sel.selectedIndex].value;
    }
    var Data = { 'CurrentAgentName': CurrentAgentName, 'selStatus': selValue, 'txtUserName': UserName, 'SubAgentID': CurrentAgentID, 'SubRoleID': CurrentRoleID };
    var Url = '';

    switch (Mode) {
        case 1:
            Url = '/User/ListUser.aspx';
            break;
        case 2:
            Url = '/Member/ListMember.aspx';
            break;
    }
    
    SwtichContent(Url, Data);
}



function DoEditUser() {



    var Data = $('#frmEdieUser').serialize();
    if (baseHtmlRequest('User/UpdUser.aspx', Data, false)) {
        alert(Res_UpdateSuccess);
        CloseBPopup();
        document.getElementById('btnGetSubAccSearch').click();

    }
}

function DoEditMember() {


    
    var Data = $('#frmEdieMember').serialize();
    if (baseHtmlRequest('Member/UpdMember.aspx', Data, false)) {
        alert(Res_UpdateSuccess);
        CloseBPopup();
        document.getElementById('btnGetSubAccSearch').click();

    }
}
function OpenChangeSubStatus(OldStatus, ToCID, ToCN, SubAgentID, SubRoleID, Mode) {
    var Url = '';
    switch (Mode) {
        case 1:
            Url = '/User/ChangeSubStatus.aspx';
            break;
        case 2:
            Url = '/Member/ChangeSubStatus.aspx';
            break;
    }
    OpenBPopup('divPopup', Url+'?SubAgentID=' + SubAgentID + '&SubRoleID=' + SubRoleID + '&ToCID=' + ToCID + '&ToCN=' + ToCN + '&OldStatus=' + OldStatus);
}

function DoChangeSubStatus(ToCID, SubAgentID, SubRoleID) {

    var sel = document.getElementById('selChangeNewStatus');
    var selValue = sel.options[sel.selectedIndex].value;
    var Data = { 'ToCID': ToCID, 'NewStatus': selValue, 'SubAgentID': SubAgentID, 'SubRoleID': SubRoleID };

    if (baseHtmlRequest('User/ProcessChangeSubStatus.ashx', Data, false)) {
        alert(Res_UpdateSuccess);
        CloseBPopup();
        document.getElementById('btnGetSubAccSearch').click();
    }
}

function DoChangeSubPWD(ToCID, NewPWD, ConPWD, SubAgentID, SubRoleID) {
    var Data = { 'ToCID': ToCID, 'NewPWD': NewPWD, 'ConPWD': ConPWD, 'SubAgentID': SubAgentID, 'SubRoleID': SubRoleID };
    if (baseHtmlRequest('User/ProcessChangeSubPWD.ashx', Data, false)) {
        alert(Res_UpdateSuccess);
        CloseBPopup();
    }
}