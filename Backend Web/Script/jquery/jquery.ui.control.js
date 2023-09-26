//---------Base Ajax Request function
//var Data = { "what" : $("#what").val(), "when" : $("#when").val(), ... }
function baseHtmlRequest(Url, Data, Async, ObjID) {
    var Result = false;    
    $.ajax
  (
    {
        type: 'post',
        url: Url,
        data: Data == '' ? {} : Data,
        dataType: "html",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        cache: false,
        async: Async,
        error: function (jqXHR, exception) {
            if (jqXHR.status == 0) {
                alert('Not connect.Please verify network.');
            } else if (jqXHR.status == 404) {
                alert('Requested page not found.');
            } else if (exception == 'parsererror') {
                alert('Requested JSON parse failed.');
            } else if (exception == 'timeout') {
                alert('Time out error.');
            } else if (exception == 'abort') {
                alert('Ajax request aborted.');
            } else {
                alert('System Error.');
            }
        },
        success: function (result) {
     
            if (result != '') {
                if (ObjID != null) {
                    $('#' + ObjID).html(result);
                }
                else if (result.toLowerCase() == 'ok') {
                    Result = true;
                }
                else {
                    alert(result);
                }
            }
        }
    }
  );
    return Result;
}





function setDroupDownSelectedItem(DroupDownID, SelectedValue) {
    $('#' + DroupDownID + ' option').filter(function () {
        return $(this).val() == SelectedValue;
    }).attr('selected', true);
}

function setSelLanDroupDown(DroupDownID) {
    $("#" + DroupDownID).selectbox
    ({
        onOpen: function (inst) {
        },
        onClose: function (inst) {
        },
        onChange: function (val, inst) {
            ChangeLan(val);
        },
        useDefaultOptionCss: true,
        speed: 300,
        classHolder: 'sbHolder_len',
        classOptions: 'sbOptions_len',
        classSelector: 'sbSelector_len'
    });
}
//--------- Init Function ------------------------
function Iinitialize() {
    baseHtmlRequest('Head.aspx', '', true, 'header');
    baseHtmlRequest('Menu.aspx', '', true, 'left');
    baseHtmlRequest('Content.aspx', '', true, 'content');

 //   baseAjaxifyHtmlRequest('MyInfo.aspx', '', false, 'content');

   // Goto();
}
//----------- Bpop function---------------------
var objBPopup = null;
function OpenBPopup(ObjID, Path) {
    objBPopup = $('#' + ObjID).bPopup({
        contentContainer: '.content',
        loadUrl: Path
    });
}

function CloseBPopup() {
    objBPopup.close();
}

//-----------Fixed Element function---------------------------
function FixedDiv(DivID, TopHeight) {
    var $objDiv = $('#' + DivID);
    if ($(window).scrollTop() > 100) {
        $objDiv.css({ 'position': 'fixed', 'top': TopHeight + 'px' });

    }
    else {
  
        $objDiv.css({ 'position': 'relative', 'top': 'auto' });

    }
}

function FixedLeft() {
    FixedDiv('left', 20);
}
$(window).scroll(FixedLeft);
FixedLeft();