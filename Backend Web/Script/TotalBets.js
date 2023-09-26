function DoTotalBetsSearch(GameTypeID, GameType)
{
    CalendarSplitDate();
    var eroMesg = document.getElementById("eroMeg").value;

    if (eobjDate < sobjDate) {
        alert(eroMesg);
    }
    else {
        var s_date = document.getElementById("s_date").value;
        var e_date = document.getElementById("e_date").value;
        var Data = { 's_date': s_date, 'e_date': e_date, 'GameTypeID': GameTypeID, 'GameType': GameType };
        SwtichContent('/TotalBets/ListTotalBets.aspx', Data);
    }
}

