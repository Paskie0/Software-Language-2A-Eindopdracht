using System;
using System.Diagnostics;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            UI.Welcome(); // Display welcome message
            Timer.Start(); // Start the timer
            Board.InitBoard(); // Initialize the board
            Board.DrawBoard(); // Draw the board to the console
            Game.Turn(); // Start the game
            Timer.Stop(); // Stop the timer
        }
    }

    /// <summary>
    /// Represents the game board and its state
    /// </summary>
    class Board
    {
        public static char[,,] board = new char[9, 3, 3]; // 3D array that represents the entire board, 9 small boards consisting of 3x3 cells each

        public static void InitBoard()
        {
            // These 3 for loops iterate over each cell in each board
            for (int boards = 0; boards < board.GetLength(0); boards++)
            {
                for (int rows = 0; rows < board.GetLength(1); rows++)
                {
                    for (int cols = 0; cols < board.GetLength(2); cols++)
                    {
                        board[boards, rows, cols] = ' '; // Fill the board with empty cells
                    }
                }
            }
            // These 2 for loops iterate over each board in the completedBoards array
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Game.completedBoards[i, j] = ' ';  // Set all boards to not completed (neutral)
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

        public static int GetCurrentBoard()
        {
            int[] position = [Game.x, Game.y];

            switch (position)
            {
                case [0, 0]:
                    return 0;
                case [0, 1]:
                    return 1;
                case [0, 2]:
                    return 2;
                case [1, 0]:
                    return 3;
                case [1, 1]:
                    return 4;
                case [1, 2]:
                    return 5;
                case [2, 0]:
                    return 6;
                case [2, 1]:
                    return 7;
                case [2, 2]:
                    return 8;
                default:
                    return 9;
            }
        }

        public static int[] GetCurrentBoardReversed(int board)
        {
            switch (board)
            {
                case 0:
                    return [0, 0];
                case 1:
                    return [0, 1];
                case 2:
                    return [0, 2];
                case 3:
                    return [1, 0];
                case 4:
                    return [1, 1];
                case 5:
                    return [1, 2];
                case 6:
                    return [2, 0];
                case 7:
                    return [2, 1];
                case 8:
                    return [2, 2];
                default:
                    return [-1, -1];
            }
        }
    }

    /// <summary>
    /// Game class manages the game state and turns.
    /// </summary>
    class Game
    {
        public static int x = 0;
        public static int y = 0;
        public static int currentPlayer = 0;
        public static int currentBoard = 0;
        public static int currentTurn = 0;
        public static char[,] completedBoards = new char[3, 3];
        public static bool gameEnded = false;

        public static void Turn()
        {
            while (!gameEnded)
            {
                char symbol = currentPlayer == 0 ? 'X' : 'O';

                if (currentTurn < 1)
                {
                    Console.WriteLine($"Player {currentPlayer + 1}, on what board would you like to play?");
                    try
                    {
                        int pickedBoard = Convert.ToInt32(Console.ReadLine()) - 1;
                        currentBoard = pickedBoard;
                        Console.WriteLine($"Where would you like to place your {symbol}");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a number");
                        continue;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Please enter a valid number between 1 and 9");
                        continue;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine($"Player {currentPlayer + 1}, where would you like to play your {symbol}?");
                }

                Console.WriteLine("X-axis:");
                x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Y-axis:");
                y = Convert.ToInt32(Console.ReadLine());

                try
                {
                    if (Board.board[currentBoard, x, y] == ' ')
                    {
                        Board.board[currentBoard, x, y] = symbol;
                        Board.DrawBoard();

                        if (CheckForBigWin(completedBoards))
                        {
                            Board.DrawBoard();
                            Console.WriteLine($"Player {currentPlayer + 1} wins the game!");
                            gameEnded = true;
                            UI.End();
                        }
                        else if (CheckForSmallWin(Board.board))
                        {
                            for (int rows = 0; rows < Board.board.GetLength(1); rows++)
                            {
                                for (int cols = 0; cols < Board.board.GetLength(2); cols++)
                                {
                                    Board.board[currentBoard, rows, cols] = symbol; // fill small board with winning symbol
                                }
                            }

                            Board.DrawBoard(); // redraw board to show who won the board.
                            completedBoards[Board.GetCurrentBoardReversed(currentBoard)[0], Board.GetCurrentBoardReversed(currentBoard)[1]] = symbol; // set currentBoard to completed


                            Console.WriteLine($"Player {currentPlayer + 1} wins board {currentBoard + 1}!");
                            currentPlayer = currentPlayer == 0 ? 1 : 0; // Switch turns
                            currentTurn++;

                            bool validBoardSelected = false;
                            while (!validBoardSelected)
                            {
                                Console.WriteLine($"Player {currentPlayer + 1}, on what board would you like to play next? (1-9)");
                                int pickedBoard = Convert.ToInt32(Console.ReadLine()) - 1; // Convert user input to 0-based index immediately

                                // Check if the picked board is within range and not completed
                                if (pickedBoard >= 0 && pickedBoard < 9 &&
                                    completedBoards[Board.GetCurrentBoardReversed(pickedBoard)[0], Board.GetCurrentBoardReversed(pickedBoard)[1]] != 'X' && completedBoards[Board.GetCurrentBoardReversed(pickedBoard)[0], Board.GetCurrentBoardReversed(pickedBoard)[1]] != 'O')
                                {
                                    currentBoard = pickedBoard;
                                    validBoardSelected = true; // Exit the loop as a valid, uncompleted board has been selected
                                }
                                else
                                {
                                    Console.WriteLine("That board has already been completed or is invalid, please choose another one.");
                                }
                            }
                        }
                        else
                        {
                            currentPlayer = currentPlayer == 0 ? 1 : 0;// Switch turns
                            currentTurn++;
                            currentBoard = Board.GetCurrentBoard();

                            bool validBoardSelected = false;
                            while (!validBoardSelected)
                            {
                                if (completedBoards[Board.GetCurrentBoardReversed(currentBoard)[0], Board.GetCurrentBoardReversed(currentBoard)[1]] != 'X' && completedBoards[Board.GetCurrentBoardReversed(currentBoard)[0], Board.GetCurrentBoardReversed(currentBoard)[1]] != 'O')
                                {
                                    validBoardSelected = true; // Exit the loop as a valid, uncompleted board has been selected
                                }
                                else
                                {
                                    Console.WriteLine("That board has already been completed or is invalid, please choose another one.");
                                    int pickedBoard = Convert.ToInt32(Console.ReadLine()) - 1; // Convert user input to 0-based index immediately
                                    currentBoard = pickedBoard;
                                    validBoardSelected = true; // Exit the loop as a valid, uncompleted board has been selected
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("This cell is already occupied, please choose another one");
                        // currentPlayer is not changed in the else statement, so the currentPlayer gets to try again
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Please enter a valid number between 1 and 9");
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number");
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        public static bool CheckForSmallWin(char[,,] board)
        {
            // Check for straight win
            for (int i = 0; i < 3; i++)
            {
                if ((board[currentBoard, i, 0] == board[currentBoard, i, 1] && board[currentBoard, i, 1] == board[currentBoard, i, 2] && board[currentBoard, i, 0] != ' ') ||
                    (board[currentBoard, 0, i] == board[currentBoard, 1, i] && board[currentBoard, 1, i] == board[currentBoard, 2, i] && board[currentBoard, 0, i] != ' '))
                {
                    return true;
                }
            }

            // Check for diagonal win
            if ((board[currentBoard, 0, 0] == board[currentBoard, 1, 1] && board[currentBoard, 1, 1] == board[currentBoard, 2, 2] && board[currentBoard, 0, 0] != ' ') ||
                (board[currentBoard, 0, 2] == board[currentBoard, 1, 1] && board[currentBoard, 1, 1] == board[currentBoard, 2, 0] && board[currentBoard, 0, 2] != ' '))
            {
                return true;
            }

            return false;
        }

        public static bool CheckForBigWin(char[,] board)
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
    }

    /// <summary>
    /// UI class manages the user interface and messages
    /// </summary>
    class UI
    {
        public static void Welcome()
        {
            string topBorder = "\u2554" + new string('\u2550', 36) + "\u2557";
            string middleBorder = "\u2560" + new string('\u2550', 36) + "\u2563";
            string bottomBorder = "\u255A" + new string('\u2550', 36) + "\u255D";
            string spaceInsideBox = new string(' ', 36);

            Console.WriteLine(topBorder);
            Console.WriteLine("\u2551  Welcome to Ultimate Tic-Tac-Toe   \u2551");
            Console.WriteLine(middleBorder);
            Console.WriteLine("\u2551" + spaceInsideBox + "\u2551");
            Console.WriteLine("\u2551      Press any key to start...     \u2551");
            Console.WriteLine("\u2551" + spaceInsideBox + "\u2551");
            Console.WriteLine(bottomBorder);

            Console.ReadKey();
            Console.WriteLine();
        }

        public static void End()
        {
            string topBorder = "\u2554" + new string('\u2550', 36) + "\u2557";
            string middleBorder = "\u2560" + new string('\u2550', 36) + "\u2563";
            string bottomBorder = "\u255A" + new string('\u2550', 36) + "\u255D";
            string spaceInsideBox = new string(' ', 36);

            Console.WriteLine(topBorder);
            Console.WriteLine("\u2551       Thank you for playing        \u2551");
            Console.WriteLine(middleBorder);
            Console.WriteLine("\u2551" + spaceInsideBox + "\u2551");
            Console.WriteLine("\u2551    Press any key to play again...  \u2551");
            Console.WriteLine("\u2551" + spaceInsideBox + "\u2551");
            Console.WriteLine(bottomBorder);
            Console.ReadKey();
            Game.gameEnded = false;
            Welcome();
            Board.InitBoard();
            Board.DrawBoard();
            Game.Turn();
        }
    }

    class Timer
    {
        private static Stopwatch stopwatch = new Stopwatch();
        private static Thread timerThread;

        /// <summary>
        /// Starts the stopwatch and the timer thread.
        /// </summary>
        public static void Start()
        {
            stopwatch.Start();
            timerThread = new Thread(DisplayElapsedTime) { IsBackground = true };
            timerThread.Start();
        }

        /// <summary>
        /// Displays the elapsed time every second.
        /// </summary>
        private static void DisplayElapsedTime()
        {
            while (!Game.gameEnded)
            {
                Console.SetCursorPosition(0, Console.WindowTop + Console.WindowHeight - 1);
                Console.Write($"Time elapsed: {stopwatch.Elapsed.ToString(@"hh\:mm\:ss")} ");
                Thread.Sleep(1000); // Update the elapsed time every second
            }
        }

        /// <summary>
        /// Stops the stopwatch and the timer thread.
        /// </summary>
        public static void Stop()
        {
            stopwatch.Stop();
        }
    }
}