$(document).ready(function () {
    var $mainContainer = $('.main-container');
    var $loginContainer = $('.login-container');    
    var $messagesContainer = $('.messages');
    var $chatroomsContainer = $('.chat-rooms');

    $mainContainer.on('click', '.login-button', login);
    $mainContainer.on('change', '#username', login);

    function login() {
        var username = $('#username').val();

        var url = main.getUrl('Home/Login');
        var data = { username: username };

        $.get(url, data, function (res) {
            $loginContainer.empty().append(res);
        }).fail(function (res) {
        }).always(function () {
        });
    }

    $mainContainer.on('click', '.start-chat', function () {
       
        var usernameA = $('#user').val();
        var usernameB = $(this).data('username');

        var usernames = [];
        usernames.push(usernameA);
        usernames.push(usernameB);

        var url = main.getUrl('Home/OpenRoom');
        var data = { usernames: usernames };

        //var data = usernames[0]: usernameA, usernameB: usernameB };

        $.post(url, data, function (res) {
            $messagesContainer.empty().append(res);
        }).fail(function (res) {
        }).always(function () {
        });
    });

    $mainContainer.on('click', '.open-chat-room', function () {
        var chatRoomId = $(this).data('chat-room');
        var url = main.getUrl('Home/OpenRoomById');
        var data = { chatRoomId: chatRoomId };

        $.get(url, data, function (res) {
            $messagesContainer.empty().append(res);
        }).fail(function (res) {
        }).always(function () {
        });
    });

    $mainContainer.on('click', '.close-chat-room', function () {
        var roomId = $('#chatRoomId').val();

        var url = main.getUrl('Home/CloseRoom');
        var data = { chatRoomId: roomId };

        $.post(url, data, function (res) {
            $messagesContainer.empty();
        }).fail(function (res) {
        }).always(function () {
        });
    });

    $mainContainer.on('click', '.send-message', sendMessage);
    $mainContainer.on('change', '#message', sendMessage);

    function sendMessage() {
        var userId = $('#userId').val();
        var roomId = $('#chatRoomId').val();
        var message = $('#message').val();

        var url = main.getUrl('Home/SendMessage');
        var data = { ParticipantId: userId, ChatRoomId: roomId, Content: message };

        $.post(url, data, function (res) {
            $('#message').val('');
            $('.chat-window').empty().append(res);
        }).fail(function (res) {
        }).always(function () {
        });
    }

    setInterval(function () {
        var currentUser = $('#user').val();
        if (currentUser) {
            var $contactListContainer = $loginContainer.find('.contacts');

            var url = main.getUrl('Home/Contacts');
            var data = { username: currentUser };

            $.post(url, data, function (res) {
                $contactListContainer.empty().append(res);
            }).fail(function (res) {
            }).always(function () {
            });
        }
    }, 1000);

    setInterval(function () {
        var currentUser = $('#user').val();
        if (currentUser) {
            var url = main.getUrl('Home/GetAllOpenChatRooms');
            var data = { username: currentUser };

            $.post(url, data, function (res) {
                $chatroomsContainer.empty().append(res);
            }).fail(function (res) {
            }).always(function () {
            });
        }
    }, 1000);

    setInterval(function () {
        var roomId = $('#chatRoomId').val();
        if (roomId) {
            var url = main.getUrl('Home/RefreshMessages');
            var data = { roomId: roomId };
            var $chatContainer = $('.chat-window');

            $.post(url, data, function (res) {
                $chatContainer.empty().append(res);
                $chatContainer.scrollTop($chatContainer[0].scrollHeight);
            }).fail(function (res) {
            }).always(function () {
            });
        }
    }, 500);
});