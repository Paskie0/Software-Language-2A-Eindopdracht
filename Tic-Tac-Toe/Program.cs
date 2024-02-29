namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            //UI.Welcome();
            Board.DrawBoard();
        }
    }

    class UI
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome to Ultimate Tic-Tac-Toe");
            Console.WriteLine("Press any key to start...");

            Console.ReadKey();
        }
    }

    class Board
    {
        static char[,] board = new char[3, 3]
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
}