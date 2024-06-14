using System;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            // Start the game with the welcome message, all other methods are trigerred from there on out, based on user input
            UI.Welcome();
        }

        public static void TimerThread()
        {
            int elapsedTime = 0; // Elapsed time in seconds

            while (!Game.gameEnded)
            {
                Thread.Sleep(1000); // Sleep for 1 second
                elapsedTime++; // Effectively increment by 1 every second
            }

            Console.WriteLine($"Elapsed time: {elapsedTime} seconds"); // Game ended, display elapsed time
        }
    }
}
