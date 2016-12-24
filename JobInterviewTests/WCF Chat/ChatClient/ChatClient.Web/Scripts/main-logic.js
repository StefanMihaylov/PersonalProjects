$(document).ready(function () {
    var $container = $('.login-container');

    $container.on('click', '.login-button', function () {
        var username = $('#username').val();

        var url = main.getUrl('Home/Login');
        var data = { username: username };

        $.get(url, data, function (res) {
            $container.empty().append(res);
        }).fail(function (res) {
        }).always(function () {
        });
    });

    $container.on('click', '.start-chat', function () {
        var usernameA = $('#user').val();
        var usernameB = $('#otherUser').val();

        var url = main.getUrl('Home/OpenRoom');
        var data = { usernameA: usernameA, usernameB: usernameB };

        $.get(url, data, function (res) {
            $container.empty().append(res);
        }).fail(function (res) {
        }).always(function () {
        });
    });

    $container.on('click', '.send-message', function () {
        var userId = $('#ParticipantAId').val();
        var roomId = $('#Id').val();
        var message = $('#message').val();

        var url = main.getUrl('Home/SendMessage');
        var data = { userId: userId, roomId: roomId, message: message };

        var $container = $('.chat-window');

        $.post(url, data, function (res) {            
            $('#message').val('');
            $container.empty().append(res);            
        }).fail(function (res) {
        }).always(function () {
        });
    });

    setInterval(function () {
        var roomId = $('#Id').val();
        var url = main.getUrl('Home/RefreshMessages');
        var data = { roomId: roomId };
        var $container = $('.chat-window');

        $.post(url, data, function (res) {
            $container.empty().append(res);
            $container.scrollTop($container[0].scrollHeight);
        }).fail(function (res) {
        }).always(function () {
        });

    }, 500);
});