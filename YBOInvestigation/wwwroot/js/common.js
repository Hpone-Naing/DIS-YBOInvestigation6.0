function GetUrl() {
    $.getJSON("/Common/GetUrl", function (TestUrl) {
        return "/" + TestUrl;
    });

}