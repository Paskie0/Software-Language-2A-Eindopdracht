namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            UI.Welcome();
            Board.DrawBoard();
            Game.Turn();
        }
    }

    class UI
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome to Ultimate Tic-Tac-Toe");
            Console.WriteLine("Press any key to start...");

            Console.ReadKey();
            Console.WriteLine();
        }

        public static void End()
        {
            Console.WriteLine("Thanks for playing, press any key to play again...");
            Console.ReadKey();
            Game.gameEnded = false;
            Game.Clear();
            Board.DrawBoard();
            Game.Turn();
        }
    }

    class Board
    {
        //multi-dimensional array that stores the value of all the tic-tac-toe squares
        public static char[,] board = new char[3, 3]
        {
            { ' ', ' ', ' '},
            { ' ', ' ', ' '},
            { ' ', ' ', ' '}
        };

        public static void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("  0 1 2");

            for (int row = 0; row < board.GetLength(0); row++)
            {
                Console.Write(row + "|");

                for (int col = 0; col < board.GetLength(1); col++)
                {
                    Console.Write(board[row, col]);
                    // Prevents an extra column from being drawn, hence the -1.
                    if (col < board.GetLength(1) - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine("|");
            }
        }
    }

    class Game
    {
        public static int currentPlayer = 0;
        public static bool gameEnded = false;

        public static void Turn()
        {
            while (!gameEnded)
            {
                char symbol = currentPlayer == 0 ? 'X' : 'O';

                Console.WriteLine($"Player {currentPlayer + 1}, where would you like to place your {symbol}");

                Console.WriteLine("X-axis:");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Y-axis:");
                int y = Convert.ToInt32(Console.ReadLine());

                if (Board.board[x, y] == ' ')// Check for empty cell
                {
                    Board.board[x, y] = symbol;
                    Board.DrawBoard();

                    if (CheckForWin(Board.board))
                    {
                        Console.WriteLine($"Player {currentPlayer + 1} wins!");
                        gameEnded = true;
                    }
                    else if (CheckForTie(Board.board))
                    {
                        Console.WriteLine("The game has ended in a tie");
                        gameEnded = true;
                    }
                    else
                    {
                        currentPlayer = currentPlayer == 0 ? 1 : 0;// Switch turns
                    }

                }
                else
                {
                    Console.WriteLine("This cell is already occupied, please choose another one");
                    // currentPlayer is not changed in the else part, so the currentPlayer gets to try again
                }
            }

            UI.End();
        }

        public static bool CheckForWin(char[,] board)
        {
            // Check for straight win
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != ' ') ||
                    (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != ' '))
                {
                    return true;
                }
            }

            // Check for diagonal win
            if ((board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ' ') ||
                (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != ' '))
            {
                return true;
            }

            return false;
        }

        public static bool CheckForTie(char[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void Clear()// Iterate through the entire array and set all elements back to ' '
        {
            for (int row = 0; row < Board.board.GetLength(0); row++)
            {
                for (int col = 0; col < Board.board.GetLength(1); col++)
                {
                    Board.board[row, col] = ' ';
                }
            }
        }
    }
}