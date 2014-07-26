(function () {
    require.config({
        paths: {
        }
    });

    require(['game'], function (Game) {
        var game = new Game();
        game.start();
    });
}());