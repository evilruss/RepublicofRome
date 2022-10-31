var apiURL = (function () {
    //var url = 'https://republicofrome.azurewebsites.net/';
    var url = 'http://localhost:50666/';
    return function () { return url; };
})();

var gameFocus = (function () {
    var gameFocus = 'khaki';
    return function () { return gameFocus; };
})();

var gameBlur = (function () {
    var gameBlur = 'palegoldenrod';
    return function () { return gameBlur; };
})();

function ajaxCallGet(url, inputData, successFunction) {
    $.ajax(
        {
            type: "GET",
            url: apiURL() + url,
            data: inputData,
            timeout: 50000,
            datatype: 'json',
            success: function (data) {
                successFunction(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(url + ' - Ajax Get error status: ' + jqXHR.status + ' Ajax Error Text: ' + jqXHR.responseText);
            }
        });
}

function ajaxCallPost(url, inputData, successFunction) {
    $.ajax(
        {
            type: "POST",
            url: apiURL() + url,
            data: inputData,
            timeout: 50000,
            datatype: 'json',
            success: function (data) {
                successFunction(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(url + ' - Ajax Post error status: ' + jqXHR.status + ' Ajax Error Text: ' + jqXHR.responseText);
            }
        });
}
