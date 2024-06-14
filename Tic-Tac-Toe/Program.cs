namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            UI.Welcome(); // Display welcome message
            Board.InitializeBoard(); // Initialize the board
            Game.Start(); // Start the game
        }
    }
}