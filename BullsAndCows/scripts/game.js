define(['highscore'], function (HighScore) {
    var Game;
    Game = (function () {
        'use strict';

        // TODO: this module shold be splitted in two parts: UI and Game Logic, but now I have no time

        // DOM elements;
        var table = document.getElementById('table');
        var input = document.getElementById('number');
        var messageBox = document.getElementById('message-box');
        var enterName = document.createElement('div');

        messageBox.style.padding = '10px 15px';
        messageBox.style.margin = '10px';
        messageBox.style.borderRadius = '20px';

        function getRandomInt(min, max) {
            return Math.floor(Math.random() * (max - min + 1)) + min;
        }

        function getRandomNumber() {
            var number = [],
                minDigit;
            for (var i = 0; i < 4; i++) {
                if (i === 0) {
                    minDigit = 1;
                }
                else {
                    minDigit = 0;
                }

                var digit = getRandomInt(minDigit, 9);
                if (isExist(number, digit)) {
                    i--;
                    continue;
                }
                else {
                    number[i] = digit;
                }
            }

            return number;
        }

        function isExist(array, digit) {
            var len = array.length;
            for (var i = 0; i < len; i++) {
                var currentDigit = array[i];
                if (currentDigit === digit) {
                    return true;
                }
            }

            return false;
        }

        function addEvent() {
            var self = this;
            input.addEventListener('change', function () {
                if (checkNumber.call(self, input.value)) {
                    input.value = '';
                    self.turns++;
                    var result = calculateSheepsAndRams.call(self);
                    addToTable.call(self, result);
                    if (result.rams === 4) {
                        showMessage('You win after ' + self.turns + ' turns!', 'green', true);
                    }
                }
            });
        }

        function addNameInput() {
            var self = this;
            var label = document.createElement('label');
            var nameInput = document.createElement('input');
            label.innerHTML = 'Enter your name:';
            nameInput.type = 'text';

            nameInput.addEventListener('keyup', function (e) {
                if (e.keyCode === 13) {
                    var result = {
                        name: this.value,
                        turns: self.turns
                    };
                    self.highScore.add(result);
                }
            });
            enterName.appendChild(label);
            enterName.appendChild(nameInput);
        }

        function checkNumber(number) {
            if (number === undefined || 1000 > number || number > 9999) {
                showMessage('Enter four digit number!', 'red');
                return false;
            }

            var newNumber = convertToArray(number);
            if (equalNumbers(newNumber)) {
                showMessage('Equal digits not allowed', 'red');
                return false;
            }

            this.currentNumber = newNumber;
            showMessage('');
            return true;
        }

        function equalNumbers(array) {
            var len = array.length;
            for (var i = 0; i < len; i++) {
                for (var j = i + 1; j < len; j++) {
                    if (array[i] === array[j]) {
                        return true;
                    }
                }
            }

            return false;
        }

        function convertToArray(number) {
            var digits = [];
            var index = 0;
            while (number) {
                digits[index] = number % 10;
                number = Math.floor(number / 10);
                index++;
            }

            return digits.reverse();
        }

        function calculateSheepsAndRams() {
            var sheeps = 0
            var rams = 0;
            var len = this.currentNumber.length;
            for (var i = 0; i < len; i++) {
                for (var j = 0; j < len; j++) {
                    if (this.currentNumber[i] === this.secretNumber[j]) {
                        if (i === j) {
                            rams++;
                        }
                        else {
                            sheeps++;
                        }

                    }
                }
            }

            return {
                sheeps: sheeps,
                rams: rams
            }
        }

        function addToTable(result) {
            var row = document.createElement('tr');
            var col = document.createElement('td');
            col.innerText = this.turns;
            row.appendChild(col.cloneNode(true));
            col.innerText = this.currentNumber.join('');
            row.appendChild(col.cloneNode(true));
            col.innerText = result.rams;
            row.appendChild(col.cloneNode(true));
            col.innerText = result.sheeps;
            row.appendChild(col.cloneNode(true));
            table.insertBefore(row, table.firstChild);
        }

        function clearTable() {
            while (table.firstChild) {
                table.removeChild(table.firstChild);
            }
        }

        function initButtons() {
            var newGameBbutton = document.getElementById('new-game');
            var self = this;
            newGameBbutton.addEventListener('click', function () {
                self.start();
            });
        }

        function showMessage(message, colour, isNameNeeded) {
            if (message === '') {
                messageBox.style.display = 'none';
            }
            else {
                messageBox.style.backgroundColor = colour;
                messageBox.style.display = '';
                messageBox.innerHTML = message;

                if (isNameNeeded) {
                    messageBox.appendChild(enterName);
                }
            }
        }

        function Game() {
            var key = 'BullAndCowHighScore';
            this.secretNumber;
            this.currentNumber;
            this.turns = 0;
            this.highScore = new HighScore(key, 'highscore');

            addEvent.call(this);
            initButtons.call(this);
            addNameInput.call(this);
        }

        Game.prototype.start = function () {
            this.secretNumber = getRandomNumber();
            this.currentNumber;
            this.turns = 0;
            clearTable()
            console.log('Secret number: ' + this.secretNumber.join('') + ' :)');
            showMessage('New game started', 'green');
            this.highScore.refresh();
        }

        return Game;
    }());
    return Game;
});