// Script files for the GameLobby.

function clearGameLobby() {
    $('#gameLobbyList').empty();
    $('#myGames').css("background-color", gameBlur());
    $('#allGames').css("background-color", gameBlur());
    $('#newGame').css("background-color", gameBlur());
}

function getMyGames() {
    var checkedOptions = $('#openPlayingCompletedForm').serialize();
    $('#gameTypeOption').val('my');
    clearGameLobby();
    $('#myGames').css("background-color", gameFocus());
    $('#gameLobbyList').text('Loading My Games List ...');
    var inputData = {
        "playerName": $('#loggedInUser').val(),
        "checkedOptions": checkedOptions
    };

    alert('Debug1');
    ajaxCallGet('GameLobby/GetMyGameList', inputData, function (data) { $('#gameLobbyList').html(data); });
}

function getAllGames() {
    var checkedOptions = $('#openPlayingCompletedForm').serialize();
    $('#gameTypeOption').val('all');
    clearGameLobby();
    $('#allGames').css("background-color", gameFocus());
    $('#gameLobbyList').text('Loading All Games List ...');
    var inputData = {
        "checkedOptions": checkedOptions
    };
    ajaxCallGet('GameLobby/GetGameList', inputData, function (data) { $('#gameLobbyList').html(data); });
}

function getNewGame() {
    $('#gameTypeOption').val('new');
    clearGameLobby();
    $('#newGame').css("background-color", gameFocus());
    $('#gameLobbyList').text('Loading New Game Form ...');
    ajaxCallGet('GameLobby/CreateNewGame', null, function (data) { $('#gameLobbyList').html(data); });
}

function getGameDetails(gameId) {
    $('.game-list-item').css("background-color", gameBlur());
    $('#game-' + gameId).css("background-color", gameFocus());
    var inputData = { "id": gameId };
    ajaxCallGet('GameLobby/GetGameDetails', inputData, function (data) {
        $('#gameLobbyList').empty();
        $('#gameLobbyList').html(data);
    });
}

function addPlayerToGame() {
    var inputData = $('#addPlayerToGameForm').serialize();
    ajaxCallPost('GameLobby/AddPlayerToGame', inputData, function (data) {
        $('#gameLobbyList').empty();
        $('#gameLobbyList').html(data);
    });
}

function createNewGame() {
    var inputData = $('#createNewGameForm').serialize();
    ajaxCallPost('GameLobby/CreateNewGame', inputData, function (data) {
        $('#gameLobbyList').empty();
        $('#gameLobbyList').html(data);
    });
}

//-------------------------------------------------------------------------------------------------------------------------
// Set up events at document ready.
$(document).ready(function () {
    $('#myGames').on('click', function () {
        getMyGames();
    });

    $('#allGames').on('click', function () {
        getAllGames();
    });

    $('#newGame').on('click', function () {
        getNewGame();
    });

    $('#gameLobbyList').on('click', '.game-list-item', function () {
        getGameDetails($(this).find('#gameId').val());
    });

    $('#gameLobbyDisplay').on('click', '#gameCloseX', function () {
        switch ($('#gameTypeOption').val()) {
            case "all":
                getAllGames();
                break;
            default:
                getMyGames();
        }
    });

    // Handle check box game options
    $('#openPlayingCompletedForm').on('change', 'input', function () {
        switch ($('#gameTypeOption').val()) {
            case "my":
                getMyGames();
                break;
            case "all":
                getAllGames();
                break;
        }
    });

    getMyGames();
});

//$('#rorBanner').on('click', function () { window.location.href = '/Home/Index' });
//$('#homePage').on('click', '#openMyGames', function () { openMyGames(); });
//$('#homePage').on('click', '#openGameLobby', function () { openGameLobby(); });
//$('#homePage').on('click', '#openNewGame', function () { openGameNew(); });
//$('#homePage').on('mouseenter mouseleave', '#openMyGames, #openGameLobby, #openNewGame', function () {
//    $(this).toggleClass('mouse-over');
//
//});
//function getMyGames2() {
//    $.ajax(
//        {
//            type: "GET",
//            url: apiURL() + "GameLobby/MyGames",
//            data: null,
//            timeout: 5000,
//            datatype: 'json',
//            success: function (data) {
//                $('#myGamesDisplay').html(data);
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                alert('showNewGame - Ajax error status: ' + jqXHR.status + ' Ajax Error Text: ' + jqXHR.responseText);
//            },
//            always: function (data) {
//                // Tidy up operation.0
//            }
//        });
//};

//-------------------------------------------------------------------------------------------------------------------------