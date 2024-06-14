namespace TicTacToe
{
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
                            Board.SetBoardAsCompleted(currentBoard, symbol);

                            Console.WriteLine($"Player {currentPlayer + 1} wins board {currentBoard + 1}!");
                            currentPlayer ^= 1; // Switch turns using the exclusive OR operator (^), which works by returning 0 if 2 bits are the same and 1 if 2 bits are different (e.g. 0 (0000) ^ 1 (0001) = 1 & 1 (0001) ^ 1 (0001) = 0)
                            currentTurn++;

                            bool validBoardSelected = false;
                            while (!validBoardSelected)
                            {
                                Console.WriteLine($"Player {currentPlayer + 1}, on what board would you like to play next? (1-9)");
                                int pickedBoard = Convert.ToInt32(Console.ReadLine()) - 1; // Convert user input to 0-based index immediately

                                // Check if the picked board is within range and not completed
                                if (pickedBoard >= 0 && pickedBoard < 9 && !Board.IsBoardCompleted(pickedBoard))
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
                            currentBoard = Board.positionToBoard[(x, y)];

                            bool validBoardSelected = false;
                            while (!validBoardSelected)
                            {
                                if (!Board.IsBoardCompleted(currentBoard))
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
}