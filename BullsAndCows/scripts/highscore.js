define([], function () {
    var Highscore;
    Highscore = (function () {
        var container;
        var olist = document.createElement('ol');
        var liElement = document.createElement('li');

        function render(score) {
            while (container.firstChild) {
                container.removeChild(container.firstChild);
            }

            var currentOl = olist.cloneNode(true);
            for (var i = 0, len = score.length; i < len; i++) {
                var currentLi = liElement.cloneNode(true);
                var currentPlayer = score[i];
                currentLi.innerHTML = currentPlayer.name + ' - ' + currentPlayer.turns + ' turn(s)'
                currentOl.appendChild(currentLi);
            }

            container.appendChild(currentOl);
        }

        function readStorage() {
            var scoreAsString = localStorage.getItem(this.key)
            return JSON.parse(scoreAsString);
        }

        function writeStorage(array) {
            array.sort(function (a, b) {
                return a.turns - b.turns;
            });

            // get only first 10 elements
            array = array.slice(0, 10);

            var arrayAsString = JSON.stringify(array)
            localStorage.setItem(this.key, arrayAsString);
            var score = readStorage.call(this);
            render(score);
        }

        function indexOf(array, newPlayer) {
            for (var i = 0, len = array.length; i < len; i++) {
                var currentPlayer = array[i];
                if (currentPlayer.name === newPlayer.name) {
                    return i;
                }
            }

            return -1;
        }

        function Highscore(key, elementID) {
            this.key = key;
            this.id = elementID;

            container = document.getElementById(this.id);

            this.refresh();
        }

        Highscore.prototype.add = function (newRow) {
            var score = readStorage.call(this);

            var index = indexOf(score, newRow);
            if (index >= 0) {
                var newScore = newRow.turns;
                var currentScore = score[index].turns;
                if (currentScore > newScore) {
                    score[index].turns = newScore;
                }
            }
            else {
                score.push(newRow);
            }
            writeStorage.call(this, score);
        }

        Highscore.prototype.refresh = function () {
            var score = readStorage.call(this);
            if (score) {
                writeStorage.call(this, score);
            }
            else {
                var seedPlayers = [{
                    name: 'Thorin Oakenshield',
                    turns: 14
                }, {
                    name: 'Aragorn, son of Arathorn',
                    turns: 10
                }, {
                    name: 'Gandalf the Grey',
                    turns: 5 // he is a wizard, he knows the secret number, but gives you a chance to win :) 
                }];
                writeStorage.call(this, seedPlayers);
            }
        }

        return Highscore;
    }());

    return Highscore;
});