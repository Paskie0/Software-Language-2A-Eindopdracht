namespace TicTacToe
{
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
            Board.InitializeBoard();
            Board.DrawBoard();
            Game.Turn();
        }
    }
}