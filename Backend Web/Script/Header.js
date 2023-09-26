

function clock()
{
    sec++;
    if (sec == 60) {
        sec = 0;
        min++;
    }
    if (min == 60) {
        min = 0;
        hrs++;
    }
    if (hrs == 24) {
        hrs = 0;
        sec = 0;
        min = 0;
        day++;
    }
    if (min < 10) {
        strmin = "0" + min;
    }
    else {
        strmin = min;
    }
    if (sec < 10) {
        strsec = "0" + sec;
    }
    else {
        strsec = sec;
    }

    if (!isDate(year, month, day))    //to next month
    {
        month++;
        day = 1;
    }
    if (!isDate(year, month, day))    //to next year
    {
        year++;
        month = 1;
        day = 1;
    }

    if (hrs >= 12) {
        stra = lbl_PM;
    }
    else {
        stra = lbl_AM;
    }
    if (hrs >= 12) {
        strhrs = hrs - 12;
    }
    else {
        strhrs = hrs;
    }
    var str = strhrs + ":" + strmin + ":" + strsec + " " + stra + " " + strMonth[month - 1] + " " + day + ", " + year + " GMT +8";
    var clock = document.getElementById("spClock");
    if (clock != null) {
        clock.innerHTML = str;
    }
}
setInterval("clock()",1000);
function isDate(TDY,TDM,TDD) //input 3 string
{ 	
	var BegDate = new Date(parseFloat(TDY)+"/"+TDM+"/"+TDD);	
	if (BegDate.getYear()<200) 
		var TBegYear=BegDate.getYear()+1900
	else var TBegYear=BegDate.getYear()
	if (TBegYear!=parseFloat(TDY) || BegDate.getMonth()+1!=parseFloat(TDM) || BegDate.getDate()!=parseFloat(TDD)) 
		return false; 
	else return true;
}

function Synctime(p_year, p_month, p_day, p_hour, p_min, p_sec) {
    year = p_year;
    month = p_month;
    day = p_day;
    hrs = p_hour;
    min = p_min;
    sec = p_sec;
}


function LoginCheck()
{
	if (document.getElementById("txtUserName").value == '' || document.getElementById("txtUserName").value == 'Username') {
    
        alert(UsernameemptyMsg); 
        return ;
     }
	 if (document.getElementById("txtPasswd").value == '') {
    
        alert(PasswordemptyMsg); 
        return ;
     }
	 if (document.getElementById("txtValidCode").value == '' || document.getElementById("txtValidCode").value == 'Validation') {
    
        alert(ValidtionemptyMsg); 
        return ;
     }
	 obj=document.getElementById('txtPasswd'); 
	 CFSKey =   CFS(obj.value);  
     CFSLowerKey = CFS(obj.value.toLowerCase()); 
     obj.value = CFSKey; 
   
     obj=document.getElementById('txtValidCode');
     ValidCode = obj.value;
	
	 obj=document.getElementById('hidLowerCasePW'); 
     EnCryptData = CFSLowerKey +   ValidCode;        
     obj.value =  MD5(EnCryptData);  
   
     //set hidkey value is CFS(Password) and  validcode to encrypt by MD5    
     obj=document.getElementById('hidKey'); 
     EnCryptData = CFSKey +   ValidCode;
     obj.value = MD5(EnCryptData);
	 document.frmLogin.submit();
}

function InitHeadAF() {
    SetSelectedLan(UserLang)
    setDroupDownSelectedItem('selLan', UserLang);
    setSelLanDroupDown('selLan', UserLang);
   
}

function DoLogout() {
    document.frmlogout.submit();
}




