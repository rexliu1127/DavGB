
var tmpKeep = new Array();
function AJAXIFY() {

    //this.DisplayMode = 0; //0:into element, 1:redirect, 2:pop window
    this.Url = "";
    this.UrlData = new Object();
    this.UrlType = "POST"; //POST, GET
    this.UrlAsync = false; //true, false
    this.UrlAlwaysReload = false; //true, false P.S:no support DisplayMode=2(pop window)
    this.UrlContent = "";
    this.fromAjaxify = false;
    var SupportHTML5 = false; //(typeof (history.pushState) != "undefined"); //true:using html5 history, false:html4
    var _self = this;
    //var _first = true;
    //key = { "content":"", "url":"", "urldata":"", "urlreload":"" }
    //value = ajax result data
    function getDateTime() {
        var now = new Date();
        return now.getFullYear().toString() + now.getMonth().toString() + now.getDate().toString() + now.getHours().toString() + now.getMinutes().toString() + now.getSeconds().toString();
    }
    function genkey(JsonData) {
        return JSON.stringify(JsonData);
    }
    function setHash(key) {
        location.hash = "#s" + key;
    }
    function getSession(key) {
        return tmpKeep[key];
        //return window.sessionStorage.getItem(key);
    }

    function setSession(key, value) {
        tmpKeep[key] = value;
       // window.sessionStorage.setItem(key, value);
   }

    if (SupportHTML5) {
        $(window).bind("popstate", function () {
            if (history.state != null) {
                if (history.state.urlreload) {
                    baseAjaxifyHtmlRequest(history.state.url, history.state.urldata, history.state.async, history.state.content, history.state.urlreload, true);
                }
                else {
                    $('#' + history.state.content).html(history.state.data);
                }
            }
        });
    }
    else {
        $(window).bind("hashchange", function () {
            var s = location.hash.replace("#s", "");
            if (s != "") {
                //var jSonData = JSON.parse(Base64Decode(s));
                var jSonData = JSON.parse(s);
                if (jSonData.urlreload) {
                    baseAjaxifyHtmlRequest(jSonData.url, jSonData.urldata, jSonData.async, jSonData.content, jSonData.urlreload, true);
                    
                }
                else {
                    var data = getSession(s);
                    $('#' + jSonData.content).html(data);
                }
            }
            else {
                var key = genkey("firstpage");
                $("body").html(getSession(key));
            }
        });
    }
    /*
    this.init = function ()
    {
    if (_first)
    {
    history.pushState({ "content": _self.UrlContent, "data": $("#" + _self.UrlContent).html() }, "", document.URL);
    }
    _first = false;
    }
    */
    this.HtmlRequest = function () {
        var Result = false;
        $.ajax
        (
            {
                type: _self.UrlType,
                url: _self.Url,
                data: _self.UrlData,
                dataType: "html",
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                cache: false,
                async: _self.UrlAsync,
                alwaysreload: _self.UrlAlwaysReload,
                error: function (jqXHR, exception) {
                    if (jqXHR.status == 0) {
                        alert('Not connect.Please verify network.');
                    }
                    else
                        if (jqXHR.status == 404) {
                            alert('Requested page not found.');
                        }
                        else
                            if (exception == 'parsererror') {
                                alert('Requested JSON parse failed.');
                            }
                            else
                                if (exception == 'timeout') {
                                    alert('Time out error.');
                                }
                                else
                                    if (exception == 'abort') {
                                        alert('Ajax request aborted.');
                                    }
                                    else {
                                        alert('System Error.');
                                    }
                },
                success: function (result) {
                    if (result != '') {
                        if (_self.UrlContent != null) {
                            if (SupportHTML5) {
                                if (history.state != null) {
                                    history.replaceState({ "content": _self.UrlContent, "data": $("#" + _self.UrlContent).html(), "url": location.href, "urldata": history.state.urldata, "urlreload": history.state.urlreload, "async": history.state.async }, "", location.href);
                                }
                                else {
                                    history.replaceState({ "content": _self.UrlContent, "data": $("#" + _self.UrlContent).html(), "url": location.href, "urldata": "", "urlreload": false, "async": false }, "", location.href);
                                }
                            }
                            else {
                                if (location.hash != "") {
                                    var key = location.hash.replace("#s", "");
                                    setSession(key, $("#" + _self.UrlContent).html());
                                }
                                else {
                                    var key = genkey("firstpage");
                                    setSession(key, $("body").html());
                                }
                            }
                            $('#' + _self.UrlContent).html(result);
                            if (SupportHTML5) {
                                if (_self.fromAjaxify) {
                                    history.replaceState({ "content": _self.UrlContent, "data": $("#" + _self.UrlContent).html(), "url": this.url, "urldata": history.state.urldata, "urlreload": history.state.urlreload, "async": history.state.async }, "", this.url);
                                }
                                else {
                                    history.pushState({ "content": _self.UrlContent, "data": $("#" + _self.UrlContent).html(), "url": this.url, "urldata": this.data, "urlreload": this.alwaysreload, "async": this.async }, "", this.url);
                                }
                            }
                            else {
                                var JsonData = { "content": _self.UrlContent, "url": this.url, "urldata": this.data, "urlreload": this.alwaysreload, "async": this.async };
                                var key = genkey(JsonData);
                                setSession(key, $("#" + _self.UrlContent).html());
                                setHash(key);
                            }
                        }

                    }
                }
            }
        );
        return Result;
    }
}


function GetGoto()
{
    var url = window.location.toString();
    var str = "";
    var str_value = "";
    if (url.indexOf("?") != -1)
    {
        var ary = url.split("?")[1].split("&");
        for (var i in ary)
        {
            str = ary[i].split("=")[0];
            if (str == "goto")
            {
                str_value = decodeURI(ary[i].split("=")[1]);
            }
        }
    }
    return str_value;
}
function Goto()
{
    if (location.hash=="")
    {
        var goto = GetGoto();
        if (goto != "")
        {
            baseAjaxifyHtmlRequest(goto, "", "content");
        }
    }
    else
    {
        var s = location.hash.replace("#s", "");
        if (s != "")
        {
            //var jSonData = JSON.parse(Base64Decode(s));
            var jSonData = JSON.parse(s);
            baseAjaxifyHtmlRequest(jSonData.url, jSonData.urldata, jSonData.content,jSonData.async, jSonData.urlreload, true);
        }
    }
}

