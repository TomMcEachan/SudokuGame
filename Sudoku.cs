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
            Print p = new Print();
            Player player = new Player();
            Solve solve = new Solve();
            Game game = new Game();

            //Variables
            bool goAgain = true;


            //Plays the Game while the player wants to go again
            while(goAgain)
            {
                goAgain = game.gameplay(p, player, solve);
            }
               
            //Ends the game for the player
            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}

    