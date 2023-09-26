// JScript 檔
// 檢查日期是否正確
function isdate(strDate){
   var strSeparator = "/"; //日期分隔符
   var strDateArray;
   var intYear;
   var intMonth;
   var intDay;
   var boolLeapYear;
   
   strDateArray = strDate.split(strSeparator);
   
   if(strDateArray.length!=3) return false;
   
   intYear = parseInt(strDateArray[2],10);
   intMonth = parseInt(strDateArray[0],10);
   intDay = parseInt(strDateArray[1],10);
   
   if(isNaN(intYear)||isNaN(intMonth)||isNaN(intDay)) return false;
   
   if(intMonth>12||intMonth<1) return false;
   
   if((intMonth==1||intMonth==3||intMonth==5||intMonth==7||intMonth==8||intMonth==10||intMonth==12)&&(intDay>31||intDay<1)) return false;
   
   if((intMonth==4||intMonth==6||intMonth==9||intMonth==11)&&(intDay>30||intDay<1)) return false;
   
   if(intMonth==2){
      if(intDay<1) return false;
      
      boolLeapYear = false;
      if((intYear%100)==0){
         if((intYear%400)==0) boolLeapYear = true;
      }
      else{
         if((intYear%4)==0) boolLeapYear = true;
      }
      
      if(boolLeapYear){
         if(intDay>29) return false;
      }
      else{
         if(intDay>28) return false;
      }
   }
   
   return true;
}
//檢查年紀 
function CheckAge(strCheckDate,Checkage) 
{
        if (strCheckDate == '') return false ; 
        if (Checkage == '') return false;  
        var currentTime = new Date();
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear() - Checkage ; 
        var LimitDate =  month + '/' + day + '/' + year;
        //alert(LimitDate); 
        if (Date.parse( strCheckDate).valueOf() < Date.parse(LimitDate))
       {
            return true; 
       }
       else
       {
            return false; 
       }    
}