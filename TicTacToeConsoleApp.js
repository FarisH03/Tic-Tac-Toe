//to run an application in js terminal in vsc: node TicTacToeConsoleApp.js
const readline = require('readline');

class TicTacToe {
    constructor() {
        this.xWins = 0;
        this.oWins = 0;
        this.play();
    }

    //method to ask players if they want to play another round
    play() {
        while (true) {
            this.initializeGame();
            this.playGame(() => {
                console.log(`\nScore: X - ${this.xWins}, O - ${this.oWins}`);
                this.ask(`\nWould you like to play another round? (y/n): `, (response) => {
                    if (response.toLowerCase() !== 'y') {
                        process.exit();
                    } else {
                        this.play();
                    }
                });
            });
            break;
        }
    }

    //initializes or resets the game
    initializeGame() {
        this.board = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
        this.currentPlayer = 'X';
        this.numberOfMoves = 9;
    }

    //method to start the game
    playGame(callback) {
        this.displayBoard();
        if (this.numberOfMoves > 0) {
            this.handleTurn(() => {
                if (this.checkWinner()) {
                    const winner = this.getLastPlayer();
                    if (winner === 'X') this.xWins++;
                    else this.oWins++;
                    console.log(`\n${winner} wins!`);
                    callback();
                } else if (this.numberOfMoves === 0) {
                    console.log("It's a draw!");
                    callback();
                } else {
                    this.playGame(callback);
                }
            });
        }
    }

    //method to handle player input and validate moves
    handleTurn(callback) {
        console.log(`\n${this.currentPlayer}'s turn`);
        this.ask("Select a field from 1-9: ", (input) => {
            const position = parseInt(input);

            if (input && !isNaN(position) && position >= 1 && position <= 9 && !isNaN(this.board[position - 1])) {
                this.board[position - 1] = this.currentPlayer;
                this.numberOfMoves--;
                this.displayBoard();
                //switches players
                this.currentPlayer = this.currentPlayer === 'X' ? 'O' : 'X';
                callback();
            } else {
                console.log("Invalid input! Please enter a valid field number.");
                this.displayBoard();
                this.handleTurn(callback);
            }
        });
    }

    //method that displays the game board
    displayBoard() {
        console.clear();
        console.log("Current board layout:\n");
        for (let i = 0; i < this.board.length; i++) {
            if (i % 3 === 0 && i !== 0) {
                console.log("\n-+-+-");
            }
            if (i % 3 !== 0) {
                process.stdout.write("|");
            }
            process.stdout.write(this.board[i] + "");
        }
        console.log();
    }

    //method that checks if there's a winner
    checkWinner() {
        const winningCombinations = [
            [0, 1, 2], [3, 4, 5], [6, 7, 8], 
            [0, 3, 6], [1, 4, 7], [2, 5, 8], 
            [0, 4, 8], [2, 4, 6]             
        ];

        for (let combo of winningCombinations) {
            const [a, b, c] = combo;
            if (this.board[a] === this.board[b] && this.board[b] === this.board[c]) {
                return true;
            }
        }

        return false;
    }

    //method that gets the last player to see if they won or not
    getLastPlayer() {
        return this.currentPlayer === 'X' ? 'O' : 'X';
    }

    //helper method to handle input
    ask(question, callback) {
        const rl = readline.createInterface({
            input: process.stdin,
            output: process.stdout
        });
        rl.question(question, (answer) => {
            rl.close();
            callback(answer);
        });
    }
}

//entry point of the program
new TicTacToe();