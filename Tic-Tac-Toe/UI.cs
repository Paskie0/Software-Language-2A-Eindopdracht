namespace TicTacToe
{
    /// <summary>
    /// UI class manages the user interface and messages
    /// </summary>
    class UI
    {
        public static void Welcome()
        {
            Console.CursorVisible = false;
            int selectedOption = 0;

            while (true)
            {
                Console.Clear();
                DisplayMenu(selectedOption);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedOption = (selectedOption - 1 + 3) % 3;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedOption = (selectedOption + 1) % 3;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (selectedOption == 0)
                    {
                        Console.CursorVisible = true;
                        Board.InitializeBoard();
                        Game.Start();
                        break;
                    }
                    else if (selectedOption == 1)
                    {
                        ShowRules();
                    }
                    else if (selectedOption == 2)
                    {
                        Console.CursorVisible = true;
                        Environment.Exit(0);
                    }
                }
            }
        }

        private static void DisplayMenu(int selectedOption)
        {
            string topBorder = "\u2554" + new string('\u2550', 36) + "\u2557";
            string middleBorder = "\u2560" + new string('\u2550', 36) + "\u2563";
            string bottomBorder = "\u255A" + new string('\u2550', 36) + "\u255D";
            string spaceInsideBox = new string(' ', 36);

            Console.WriteLine(topBorder);
            Console.WriteLine("\u2551  Welcome to Ultimate Tic-Tac-Toe   \u2551");
            Console.WriteLine(middleBorder);
            Console.WriteLine("\u2551" + spaceInsideBox + "\u2551");

            string[] options = { "Start Game", "View Rules", "Exit" };
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedOption)
                {
                    Console.WriteLine($"\u2551 > {options[i].PadRight(33)}\u2551");
                }
                else
                {
                    Console.WriteLine($"\u2551   {options[i].PadRight(33)}\u2551");
                }
            }

            Console.WriteLine("\u2551" + spaceInsideBox + "\u2551");
            Console.WriteLine(bottomBorder);
        }

        private static void ShowRules()
        {
            Console.Clear();

            string topBorder = "\u2554" + new string('\u2550', 36) + "\u2557";
            string middleBorder = "\u2560" + new string('\u2550', 36) + "\u2563";
            string bottomBorder = "\u255A" + new string('\u2550', 36) + "\u255D";
            string spaceInsideBox = new string(' ', 36);

            Console.WriteLine(topBorder);
            Console.WriteLine("\u2551             Game Rules             \u2551");
            Console.WriteLine(middleBorder);
            Console.WriteLine("\u2551" + spaceInsideBox + "\u2551");
            Console.WriteLine("\u2551  1. The game is played on 9 small  \u2551");
            Console.WriteLine("\u2551     boards within a large board.   \u2551");
            Console.WriteLine("\u2551  2. The objective is to win 3 small\u2551");
            Console.WriteLine("\u2551     boards in a row, column, or    \u2551");
            Console.WriteLine("\u2551     diagonal on the large board.   \u2551");
            Console.WriteLine("\u2551  3. Players take turns placing X or\u2551");
            Console.WriteLine("\u2551     O on the small boards.         \u2551");
            Console.WriteLine("\u2551  4. The move must be made in the   \u2551");
            Console.WriteLine("\u2551     corresponding small board of   \u2551");
            Console.WriteLine("\u2551     the previous move's position.  \u2551");
            Console.WriteLine("\u2551  5. If a small board is completed, \u2551");
            Console.WriteLine("\u2551     the next player can choose any \u2551");
            Console.WriteLine("\u2551     uncompleted board.             \u2551");
            Console.WriteLine("\u2551" + spaceInsideBox + "\u2551");
            Console.WriteLine(bottomBorder);

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        public static void End()
        {
            Console.Clear();

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
        }
    }
}
