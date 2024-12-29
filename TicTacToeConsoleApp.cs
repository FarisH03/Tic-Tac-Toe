using System;

namespace TicTacToeConsoleApp
{
    public class TicTacToe
    {
        //board is an array representing the Tic-Tac-Toe board;
        //currentPlayer tracks whose turn it is,'X' or 'O';
        //numberOfMoves counts the remaining moves;
        //xWins and oWins tracks the number of wins for each player

        private char[] board = new char[9];
        private char currentPlayer;
        private int numberOfMoves;
        private int xWins = 0;
        private int oWins = 0;

        //asks the players if they want to play another round until they say 'no'; 
        //Initializes and starts the game each time
        public TicTacToe()
        {
            while (true)
            {
                InitializeGame();
                PlayGame();
                Console.WriteLine($"\nScore: X - {xWins}, O - {oWins}");
                Console.WriteLine("\nWould you like to play another round? (y/n): ");
                string? response = Console.ReadLine();
                if (response?.ToLower() != "y")
                {
                    break;
                }
            }
        }

        //sets up the game board with numbers 1 to 9; Sets the starting player to 'X' adn resets the move count to 9
        private void InitializeGame()
        {
            board = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            currentPlayer = 'X';
            numberOfMoves = 9;
        }

        //displays the board and handles player turns; checks for a winner or a draw after each move and
        //checks for a winner or a draw after each move
        private void PlayGame()
        {
            DisplayBoard();
            while (numberOfMoves > 0)
            {
                HandleTurn();
                if (CheckWinner())
                {
                    char winner = GetLastPlayer();
                    if (winner == 'X') xWins++;
                    else oWins++;

                    Console.WriteLine($"\n{winner} wins!");
                    break;
                }
                else if (numberOfMoves == 0)
                {
                    Console.WriteLine("It's a draw!");
                }
            }
        }

        //handles the current player's move and input validation, updates the board and the number of moves left and
        //switches the current player after a valid move
        private void HandleTurn()
        {
            Console.WriteLine($"\n{currentPlayer}'s turn");
            int position;

            while (true)
            {
                Console.WriteLine("Select a field from 1-9: ");
                string? input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out position) && position >= 1 && position <= 9 && char.IsDigit(board[position - 1]))
                {
                    board[position - 1] = currentPlayer;
                    numberOfMoves--;
                    DisplayBoard();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid field number.");
                    DisplayBoard();
                }
            }

            currentPlayer = currentPlayer == 'X' ? 'O' : 'X'; //switches the player

            Console.WriteLine();
        }

        //Clears the console and prints the current state of the game board and adds
        //horizontal lines between rows for better readability
        private void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine("Current board layout:\n");
            for (int i = 0; i < board.Length; i++)
            {
                if (i % 3 == 0 && i != 0)
                {
                    Console.WriteLine("\n-+-+-");
                }
                if (i % 3 != 0 && i != 0)
                {
                    Console.Write("|");
                }
                Console.Write(board[i]);
            }
            Console.WriteLine();
        }

        //checks for a winner by checking all possible winning combinations (rows, columns, and diagonals)
        private bool CheckWinner()
        {
            int[,] winningCombinations = new int[,] {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, //rows
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, //columns
                {0, 4, 8}, {2, 4, 6}             //diagonals
            };

            for (int i = 0; i < winningCombinations.GetLength(0); i++)
            {
                int a = winningCombinations[i, 0];
                int b = winningCombinations[i, 1];
                int c = winningCombinations[i, 2];

                if (board[a] == board[b] && board[b] == board[c])
                {
                    return true;
                }
            }

            return false;
        }

        //returns the player who made the last move (helpful for declaring the correct winner)
        private char GetLastPlayer()
        {
            return currentPlayer == 'X' ? 'O' : 'X';
        }

        //entry point of the program that creates a new instance of the TicTacToe game and starts it
        public static void Main(string[] args)
        {
            new TicTacToe();
        }
    }
}