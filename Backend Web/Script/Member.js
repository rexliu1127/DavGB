function DoUpdDefaultMinMax()
{
    var Data = $('#frmDefMinMax').serialize();
    if (baseHtmlRequest('Member/DoUpdDefaultMinMax.ashx', Data, false)) {
        alert(Res_UpdateSuccess);
    }
}

function DoMemberSettingSearch()
{

    var UserName = document.getElementById('txtUserName').value;

    var Data = { 'txtUserName': UserName };
    var Url = '/Member/ListMemberSetting.aspx';;


    

    SwtichContent(Url, Data);
}

function DoPagingMemberSettingSearch({PageLength, PageIndex}={}) {

    var UserName = document.getElementById('txtUserName').value;

    var Data = { 'txtUserName': UserName , 'PageLength': PageLength, 'PageIndex': PageIndex };
    var Url = '/Member/ListMemberSetting.aspx';;




    SwtichContent(Url, Data);
}


function DoPagingMemberList({PageLength, PageIndex, CurrentAgentName, CurrentAgentID, CurrentRoleID}={}) {

    var selValue = '';
    var UserName = document.getElementById('txtUserName').value;

    if (document.getElementById('selSearchUserStatus') != null) {
        var sel = document.getElementById('selSearchUserStatus');
        selValue = sel.options[sel.selectedIndex].value;
    }
    var Data = { 'CurrentAgentName': CurrentAgentName, 'selStatus': selValue, 'txtUserName': UserName, 'SubAgentID': CurrentAgentID, 'SubRoleID': CurrentRoleID, 'PageLength': PageLength, 'PageIndex': PageIndex };
    var Url = '';

    Url = '/Member/ListMember.aspx';

    SwtichContent(Url, Data);
}

function OpenChangeMemberSetting(OldStatus,ProviderID,GameType, ToCID, ToCN) {
    var Url = '/Member/ChangeMemberSetting.aspx';

    OpenBPopup('divPopup', Url + '?OldStatus=' + OldStatus + '&ProviderID=' + ProviderID + '&GameType=' + GameType + '&ToCID=' + ToCID + '&ToCN=' + ToCN);
}

function DoChangeMemberSetting(ToCID, ProviderID, GameType)
{
    var sel = document.getElementById('selChangeNewStatus');
    var selValue = sel.options[sel.selectedIndex].value;
    var Data = { 'ToCID': ToCID, 'NewStatus': selValue, 'ProviderID': ProviderID, 'GameType': GameType };

    if (baseHtmlRequest('Member/ProcessChangeMemberSetting.ashx', Data, false)) {
        alert(Res_UpdateSuccess);
        CloseBPopup();
        document.getElementById('btnDoMemberSettingSearch').click();
    }
}