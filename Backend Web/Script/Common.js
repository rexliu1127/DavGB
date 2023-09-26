var reloadValidateCount = 1;
function reloadValidatecode() {
    reloadValidateCount++;
    document.getElementById('imgValidateCode').src = 'ValidateCode.aspx?' + reloadValidateCount;
}
//--------- Base 64 function-------------
function Base64Encode(str)
{
    var base64 = new BASE64();
    return base64.encode64(str);
}

function Base64Decode(str)
{
    var base64 = new BASE64();
    return base64.decode64(str);
}

BASE64 = function()
{ 
    var keyStr = "ABCDEFGHIJKLMNOP" + 
                 "QRSTUVWXYZabcdef" + 
                 "ghijklmnopqrstuv" + 
                 "wxyz0123456789+/" + 
                 "=";

     this.encode64 = function(input) { 
       input = unicodetoBytes(input); 
       var output = ""; 
       var chr1, chr2, chr3 = ""; 
       var enc1, enc2, enc3, enc4 = ""; 
       var i = 0;

       do { 
          chr1 = input[i++]; 
          chr2 = input[i++]; 
          chr3 = input[i++];

          enc1 = chr1 >> 2; 
          enc2 = ((chr1 & 3) << 4) | (chr2 >> 4); 
          enc3 = ((chr2 & 15) << 2) | (chr3 >> 6); 
          enc4 = chr3 & 63;

          if (isNaN(chr2)) { 
             enc3 = enc4 = 64; 
          } else if (isNaN(chr3)) { 
             enc4 = 64; 
          }

          output = output + 
             keyStr.charAt(enc1) + 
             keyStr.charAt(enc2) + 
             keyStr.charAt(enc3) + 
             keyStr.charAt(enc4); 
          chr1 = chr2 = chr3 = ""; 
          enc1 = enc2 = enc3 = enc4 = ""; 
       } while (i < input.length);

       return output; 
    }

    this.decode64 = function(input) { 
       var output = ""; 
       var chr1, chr2, chr3 = ""; 
       var enc1, enc2, enc3, enc4 = ""; 
       var i = 0;

       // remove all characters that are not A-Z, a-z, 0-9, +, /, or = 
       var base64test = /[^A-Za-z0-9/+/=]/g; 
       if (base64test.exec(input)) { 
          alert("There were invalid base64 characters in the input text./n" + 
                "Valid base64 characters are A-Z, a-z, 0-9, '+', '/', and '='/n" + 
                "Expect errors in decoding."); 
       } 
       input = input.replace(/[^A-Za-z0-9/+/=]/g, ""); 
        output=new Array(); 
       do { 
          enc1 = keyStr.indexOf(input.charAt(i++)); 
          enc2 = keyStr.indexOf(input.charAt(i++)); 
          enc3 = keyStr.indexOf(input.charAt(i++)); 
          enc4 = keyStr.indexOf(input.charAt(i++));

          chr1 = (enc1 << 2) | (enc2 >> 4); 
          chr2 = ((enc2 & 15) << 4) | (enc3 >> 2); 
          chr3 = ((enc3 & 3) << 6) | enc4;

          output.push(chr1); 
          if (enc3 != 64) { 
             output.push(chr2); 
          } 
          if (enc4 != 64) { 
             output.push(chr3); 
          }

          chr1 = chr2 = chr3 = ""; 
          enc1 = enc2 = enc3 = enc4 = "";

       } while (i < input.length); 
       return bytesToUnicode(output); 
    }

      function unicodetoBytes(s) 
       { 
          var result=new Array(); 
          if(s==null || s=="") return result; 
          result.push(255); // add "FE" to head 
          result.push(254); 
          for(var i=0;i<s.length;i++) 
          { 
            var c=s.charCodeAt(i).toString(16); 
            if(c.length==1) i="000"+c; 
            else if(c.length==2) c="00"+c; 
            else if(c.length==3) c="0"+c; 
            var var1=parseInt( c.substring(2),16); 
            var var2=parseInt( c.substring(0,2),16); 
            result.push( var1); 
            result.push(var2) ; 
          } 
          return result; 
       }

       function bytesToUnicode(bs) 
       { 
         var result=""; 
          var offset=0; 
          if(bs.length>=2 && bs[0]==255 && bs[1]==254) offset=2;  // delete "FE" 
          for(var i=offset;i<bs.length;i+=2) 
          { 
                var code=bs[i]+(bs[i+1]<<8); 
                result+=String.fromCharCode(code); 
          } 
          return result; 
       } 
}
//-------------CFS Endode function---------------------------
var CFS = function (codeStr) {
    function CfsCode(nWord) {
        var result = "";
        for (var cc = 1; cc <= nWord.length; cc++) {
            result += nWord.charAt(cc - 1).charCodeAt(0);
        }
        var DecimalValue = new Number(result);
        result = DecimalValue.toString(16);
        return result;
    };

    var CodeLen = 30, CodeSpace, Been;
    CodeSpace = CodeLen - codeStr.length;
    if (CodeSpace > 1) {
        for (var cecr = 1; cecr <= CodeSpace; cecr++) {
            codeStr = codeStr + String.fromCharCode(21);
        }
    }
    var NewCode = new Number(1);

    for (var cecb = 1; cecb <= CodeLen; cecb++) {
        Been = CodeLen + codeStr.charAt(cecb - 1).charCodeAt(0) * cecb;
        NewCode = NewCode * Been;
    }

    var tmpNewCode = new Number(NewCode.toPrecision(15)); //to convert to the same precision as c# code
    codeStr = tmpNewCode.toString().toUpperCase();
    var NewCode2 = "";

    for (var cec = 1; cec <= codeStr.length; cec++) {
        NewCode2 = NewCode2 + CfsCode(codeStr.substring(cec - 1, cec + 2));
    }

    var CfsEncodeStr = "";
    for (var cec = 20; cec <= NewCode2.length - 18; cec += 2) {
        CfsEncodeStr = CfsEncodeStr + NewCode2.charAt(cec - 1);
    }
    return CfsEncodeStr.toUpperCase();
}

function userBrowser() {
    var browserName = navigator.userAgent.toLowerCase();
    if (/msie/i.test(browserName) && !/opera/.test(browserName)) {
        return "IE";
    } else if (/firefox/i.test(browserName)) {
        return "Firefox";
    } else if (/chrome/i.test(browserName) && /webkit/i.test(browserName) && /mozilla/i.test(browserName)) {
        return "Chrome";
    } else if (/opera/i.test(browserName)) {
        return "Opera";
    } else if (/webkit/i.test(browserName) && !(/chrome/i.test(browserName) && /webkit/i.test(browserName) && /mozilla/i.test(browserName))) {
        return "Safari";
    } else {
        return "unKnow";
    }
}

function SetSelectedOptionByValue(ObjID, SelValue) 
{
    var sel = document.getElementById(ObjID);

    for (var i, j = 0; i = sel.options[j]; j++) 
    {
        if (i.value == SelValue) 
        {
            sel.selectedIndex = j;
            break;
        }
    }
}

function GetSelectedOptionValue(ObjID) {
    var sel = document.getElementById(ObjID);
    return sel.options[sel.selectedIndex].value;
}


function DoPaging({PageLength, PageIndex, CurrentAgentName, CurrentAgentID, CurrentRoleID}={}) {


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
//-------------Calendar  function---------------------------

//--------Calendar function------------------
function SetStartEndDate(StartDate, EndDate) {
    document.getElementById('s_date').value = StartDate;
    document.getElementById('e_date').value = EndDate;

}
function dateChangedTos_date(calendar) {
    if (calendar.dateClicked) {
        var y = calendar.date.getFullYear();
        var m = calendar.date.getMonth(); // integer, 0..11
        var d = calendar.date.getDate(); // integer, 1..31
        document.getElementById('s_date').value = (m + 1) + "/" + d + "/" + y;
    }
}

function dateChangedToe_date(calendar) {
    if (calendar.dateClicked) {
        var y = calendar.date.getFullYear();
        var m = calendar.date.getMonth(); // integer, 0..11
        var d = calendar.date.getDate(); // integer, 1..31
        document.getElementById('e_date').value = (m + 1) + "/" + d + "/" + y;
    }
}

var sobjDate = null;
var eobjDat = null;
function CalendarSplitDate() {
    var s_date = document.getElementById("s_date");
    var e_date = document.getElementById("e_date");
    var s_dataAry = new Array(2);
    var e_dataAry = new Array(2);

    var kk1 = s_date.value.split("/");
    for (var i = 0; i < kk1.length; i++) {
        s_dataAry[i] = kk1[i];
    }
    var kk2 = e_date.value.split("/");
    for (var i = 0; i < kk2.length; i++) {
        e_dataAry[i] = kk2[i];
    }

    sobjDate = new Date(s_dataAry[2], s_dataAry[0], s_dataAry[1]);
    eobjDate = new Date(e_dataAry[2], e_dataAry[0], e_dataAry[1]);
}
