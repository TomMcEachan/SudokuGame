using System;
using System.Collections.Generic;
using System.Linq;
using Figgle;

namespace SudokuGame
{
    class Sudoku
    {
        /// <summary>
        /// The main method of the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Instantiate Classes
            Solve solve = new Solve();
            Game game = new Game();
            GameState state = new GameState();
          

            //Variables
            bool goAgain = true;

            //Plays the Game while the player wants to go again
            while(goAgain)
            {
                goAgain = game.Gameplay(solve, state);
            }
               
            //Ends the game for the player
            Console.WriteLine("\n\nThanks for playing!");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}

    