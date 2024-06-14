using System;
using System.Collections.Generic;

namespace TicTacToe
{
    /// <summary>
    /// Represents the game board and its state
    /// </summary>
    class Board
    {
        public static char[,,] board = new char[9, 3, 3]; // 3D array that represents the entire board, 9 small boards consisting of 3x3 cells each

        // Dictionary to map (x, y) coordinates to a board
        public static readonly Dictionary<(int, int), int> positionToBoard = new()
        {
            { (0, 0), 0 },
            { (0, 1), 1 },
            { (0, 2), 2 },
            { (1, 0), 3 },
            { (1, 1), 4 },
            { (1, 2), 5 },
            { (2, 0), 6 },
            { (2, 1), 7 },
            { (2, 2), 8 }
        };

        // Dictionary to map a board to (x, y) coordinates
        public static readonly Dictionary<int, (int, int)> boardToPosition = new()
        {
            { 0, (0, 0) },
            { 1, (0, 1) },
            { 2, (0, 2) },
            { 3, (1, 0) },
            { 4, (1, 1) },
            { 5, (1, 2) },
            { 6, (2, 0) },
            { 7, (2, 1) },
            { 8, (2, 2) }
        };

        public static void InitializeBoard()
        {
            EmptyAllBoards();
            SetAllBoardsUncompleted();
            DrawBoard();
        }

        public static void EmptyAllBoards()
        {
            // These 3 for loops together iterate over each cell in every board
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
        }

        public static void SetAllBoardsUncompleted()
        {
            // These 2 for loops together iterate over each board in the completedBoards array
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

            for (int boardRow = 0; boardRow < 3; boardRow++)
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int boardCol = 0; boardCol < 3; boardCol++)
                    {
                        int boardIndex = boardRow * 3 + boardCol;
                        Console.Write("|");
                        for (int col = 0; col < 3; col++)
                        {
                            Console.Write(board[boardIndex, row, col]);
                        }
                        Console.Write("|");
                    }
                    Console.WriteLine();
                }
                if (boardRow < 2)
                {
                    Console.WriteLine("---------------");
                }
            }
        }

        public static bool IsBoardCompleted(int boardIndex)
        {
            if (Game.completedBoards[boardToPosition[boardIndex].Item1, boardToPosition[boardIndex].Item2] != ' ')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SetBoardAsCompleted(int boardIndex, char symbol)
        {
            Game.completedBoards[boardToPosition[boardIndex].Item1, boardToPosition[boardIndex].Item2] = symbol;
        }
    }
}
