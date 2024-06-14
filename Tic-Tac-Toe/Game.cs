namespace TicTacToe
{
    /// <summary>
    /// Game class manages the game state and turns.
    /// </summary>
    class Game
    {
        public static int x;
        public static int y;
        public static int currentPlayer;
        public static int currentBoard;
        public static int currentTurn;
        public static bool gameEnded;

        public static void Start()
        {
            ChooseBoard();

            while (!gameEnded)
            {
                char symbol = currentPlayer == 0 ? 'X' : 'O';

                MakeMove(symbol);
                ProcessMove(symbol);
            }
        }

        private static void ChooseBoard()
        {
            Console.WriteLine($"Player {currentPlayer + 1}, on what board would you like to play?");
            try
            {
                int pickedBoard = Convert.ToInt32(Console.ReadLine()) - 1;
                currentBoard = pickedBoard;
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a number");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please enter a valid board between 1 and 9");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void MakeMove(char symbol)
        {
            Console.WriteLine($"Player {currentPlayer + 1}, where would you like to play your {symbol}?");

            try
            {
                Console.WriteLine("X-axis:");
                x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Y-axis:");
                y = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a number");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please enter a valid coordinate between 0 and 2");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ProcessMove(char symbol)
        {
            if (Board.Boards[currentBoard, x, y] == ' ')
            {
                Board.Boards[currentBoard, x, y] = symbol;
                Board.DrawBoard();

                if (CheckForSmallWin(Board.Boards))
                {
                    CompleteBoard(symbol);
                    if (CheckForBigWin(Board.CompletedBoards))
                    {
                        Board.DrawBoard();
                        Console.WriteLine($"Player {currentPlayer + 1} wins the game!");
                        gameEnded = true;
                        UI.End();
                    }
                    Board.DrawBoard(); // redraw board to show who won the board
                    Console.WriteLine($"Player {currentPlayer + 1} wins board {currentBoard + 1}!");
                    SwitchTurn();
                    BoardSelection();
                }
                else
                {
                    SwitchTurn();
                    currentBoard = Board.positionToBoard[(x, y)];
                    BoardSelection();
                }
            }
            else
            {
                Console.WriteLine("This cell is already occupied, please choose another one");
            }
        }

        private static void CompleteBoard(char symbol)
        {
            for (int rows = 0; rows < Board.Boards.GetLength(1); rows++)
            {
                for (int cols = 0; cols < Board.Boards.GetLength(2); cols++)
                {
                    Board.Boards[currentBoard, rows, cols] = symbol; // fill small board with winning symbol
                }
            }

            Board.SetBoardAsCompleted(currentBoard, symbol);
        }

        private static void SwitchTurn()
        {
            currentPlayer ^= 1; // Switch turns using the exclusive OR operator
            currentTurn++;
        }

        private static void BoardSelection()
        {
            bool validBoardSelected = false;
            while (!validBoardSelected)
            {
                if (!Board.IsBoardCompleted(currentBoard))
                {
                    validBoardSelected = true; // Exit the loop as a valid, uncompleted board has been selected
                }
                else
                {
                    Console.WriteLine($"Player {currentPlayer + 1}, on what board would you like to play next? (1-9)");
                    int pickedBoard = Convert.ToInt32(Console.ReadLine()) - 1; // Convert user input to 0-based index immediately

                    if (pickedBoard >= 0 && pickedBoard < 9)
                    {
                        if (!Board.IsBoardCompleted(pickedBoard))
                        {
                            currentBoard = pickedBoard;
                            validBoardSelected = true; // Exit the loop as a valid, uncompleted board has been selected
                        }
                        else
                        {
                            Console.WriteLine("That board has already been completed, please choose another one.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid board selection. Please choose a board between 1 and 9.");
                    }
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
}
