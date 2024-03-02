namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            //UI.Welcome();
            //Board.DrawBoard();
            //Game.Turn();
            BigBoard.InitBoard();
            BigBoard.DrawBoard();
            BigGame.Turn();
        }
    }

    class BigBoard
    {
        public static char[,,] board = new char[9, 3, 3];

        public static void InitBoard()
        {
            for (int boards = 0; boards < board.GetLength(0); boards++)
            {
                for (int rows = 0; rows < board.GetLength(1); rows++)
                {
                    for (int cols = 0; cols < board.GetLength(2); cols++)
                    {
                        board[boards, rows, cols] = '-';
                    }
                }
            }
        }

        public static void DrawBoard()
        {
            Console.Clear();

            // There are 9 small boards in a 3x3 grid
            int boardsPerRow = 3; // 3 boards per row in the big board

            // Iterate over each row of boards
            for (int boardRow = 0; boardRow < boardsPerRow; boardRow++)
            {
                // For each of the 3 rows in each small board
                for (int row = 0; row < 3; row++)
                {
                    // Iterate over each board in the row
                    for (int boardCol = 0; boardCol < boardsPerRow; boardCol++)
                    {
                        int boardIndex = boardRow * boardsPerRow + boardCol; // Calculate the current board's index
                        Console.Write("|"); // Start of a new board in the row

                        // Print each cell in this row of the current board
                        for (int col = 0; col < 3; col++)
                        {
                            Console.Write(board[boardIndex, row, col]);
                        }
                        Console.Write("|"); // End of the current board's row
                    }
                    Console.WriteLine(); // End of this row across all boards in the current big board row
                }

                // Print a dividing line between rows of boards, if not the last row
                if (boardRow < boardsPerRow - 1)
                {
                    Console.WriteLine("---------------");
                }
            }
        }
    }

    class BigGame
    {
        public static int currentPlayer = 0;
        public static int currentBoard = 0;
        public static bool gameEnded = false;

        public static void Turn()
        {
            while (!gameEnded)
            {
                char symbol = currentPlayer == 0 ? 'X' : 'O';

                Console.WriteLine($"Player {currentPlayer + 1}, on what board would you like to play?");
                int pickedBoard = Convert.ToInt32(Console.ReadLine());

                currentBoard = pickedBoard - 1;

                Console.WriteLine($"Where would you like to place your {symbol}");

                Console.WriteLine("X-axis:");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Y-axis:");
                int y = Convert.ToInt32(Console.ReadLine());

                if (BigBoard.board[currentBoard, x, y] == '-')
                {
                    BigBoard.board[currentBoard, x, y] = symbol;
                    BigBoard.DrawBoard();
                    currentPlayer = currentPlayer == 0 ? 1 : 0;// Switch turns
                }
                else
                {
                    Console.WriteLine("This cell is already occupied, please choose another one");
                    // currentPlayer is not changed in the else part, so the currentPlayer gets to try again
                }
            }
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