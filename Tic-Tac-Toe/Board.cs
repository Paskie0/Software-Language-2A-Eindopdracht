using System;
using System.Collections.Generic;

namespace TicTacToe
{
    /// <summary>
    /// Represents the game board and its state
    /// </summary>
    class Board
    {
        public static char[,,] Boards = new char[9, 3, 3]; // 3D array that represents the entire board, 9 small boards consisting of 3x3 cells each
        public static char[,] CompletedBoards = new char[3, 3]; // 2D array that represents the completed boards

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

        // Get the board ready for the game
        public static void InitializeBoard()
        {
            EmptyAllBoards();
            SetAllBoardsUncompleted();
            DrawBoard();
        }

        public static void EmptyAllBoards()
        {
            // These 3 for loops together iterate over each cell in every board
            for (int boards = 0; boards < Boards.GetLength(0); boards++)
            {
                for (int rows = 0; rows < Boards.GetLength(1); rows++)
                {
                    for (int cols = 0; cols < Boards.GetLength(2); cols++)
                    {
                        Boards[boards, rows, cols] = ' '; // Fill the board with empty cells
                    }
                }
            }
        }

        public static void SetAllBoardsUncompleted()
        {
            // These 2 for loops together iterate over each board in the CompletedBoards array
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CompletedBoards[i, j] = ' ';  // Set all boards to not completed (neutral)
                }
            }
        }

        public static void DrawBoard()
        {
            Console.Clear(); // To make sure the board is cleared before drawing

            // Loop through each small board row
            for (int boardRow = 0; boardRow < 3; boardRow++)
            {
                // Loop through each cell row within the small boards
                for (int row = 0; row < 3; row++)
                {
                    // Loop through each small board column
                    for (int boardCol = 0; boardCol < 3; boardCol++)
                    {
                        Console.Write("|"); // Draw a vertical divider between boards

                        // Loop through each cell column within the small boards
                        for (int col = 0; col < 3; col++)
                        {
                            // Write the cell value to the console
                            Console.Write(Boards[positionToBoard[(boardRow, boardCol)], row, col]);
                        }
                        Console.Write("|"); // Draw a vertical divider between cells
                    }
                    Console.WriteLine(); // Move to the next line for the next row of cells
                }

                // Draw horizontal dividers between rows of small boards, except for the last row
                if (boardRow < 2)
                {
                    Console.WriteLine("---------------");
                }
            }
        }

        public static bool IsBoardCompleted(int boardIndex) // Check if a small board is completed by getting the position from the current board and checking the cell values
        {
            return CompletedBoards[boardToPosition[boardIndex].Item1, boardToPosition[boardIndex].Item2] != ' ';
        }

        public static void SetBoardAsCompleted(int boardIndex, char symbol) // Set a small board as completed by getting the position from the current board and setting the cell values
        {
            CompletedBoards[boardToPosition[boardIndex].Item1, boardToPosition[boardIndex].Item2] = symbol;
        }
    }
}
