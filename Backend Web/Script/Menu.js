function SwtichContent(Url, Data) {
   // document.getElementById('WaitingContent').style.display = '';
    //document.getElementById('content').style.display = 'none';
    var LoadingPath = SkinPuplicPath+'images/ajax-loader.gif';
    document.getElementById('content').innerHTML = '<img align="center" style="display:block; margin:auto;"  src="' + LoadingPath + '"/>'
    if (Data == null) {
        Data = '';
    }
   
    baseHtmlRequest(Url, Data, true, 'content');
    document.getElementById('WaitingContent').style.display = 'none';
    document.getElementById('content').style.display = '';
}


function SwitchTotalBets(GameTypeID, GameType) {
    var Data = { 'GameTypeID': GameTypeID, 'GameType': GameType };
    SwtichContent('/TotalBets/ListTotalBets.aspx', Data);
}