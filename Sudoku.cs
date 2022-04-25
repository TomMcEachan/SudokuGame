﻿using System;


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
            //Creates a new Instance of Sudoku
            Sudoku sudoku = new Sudoku();
            Console.Title = "Sudoku Game";

            System.IO.Directory.CreateDirectory(@"SudokuGame\SaveData");
            
            

            sudoku.SudokuStart();
        }

        //---------------------------------------------METHODS-------------------------------------------------------------------------------//

        /// <summary>
        /// This method is the main logic for the Sudoku Game
        /// </summary>
        public void SudokuStart()
        {
            //Creates a new instance of Game 
            Game game = new Game();

            //Creates a new instance of AutoPlay
            AutoPlay autoPlay = new AutoPlay();

            Timer time = new Timer();
            time.hello();

            //Prints the intro and gets the player name
            string name = game.printIntro();
            Player player = new Player(name);

            int answerNum;

            Console.WriteLine("1. Play the Game\n" +
                              "2. Auto replay a previous Game\n");

            string answer = Console.ReadLine();
            
            while (!int.TryParse(answer, out answerNum))
            {
                Console.WriteLine("That is not a valid selection. Please enter either 1 or 2 to select your mode.");
                answer = Console.ReadLine();
            }

            bool saveData = false;
            bool playGame = false;

            switch (answerNum)
            {
                case 1:
                    //Starts the Game
                    game.Start(player, time);
                    break;
                case 2:
                    //Starts the Auto Replay Feature
                    Tuple<bool, bool> t = autoPlay.AutoPlayGame();
                    saveData = t.Item1;
                    playGame = t.Item2;
                    
                    if (!playGame)
                    {
                        game.End();
                    }
                    if (saveData)
                    {
                        game.Start(player, time);
                    }

                    break;
                default:
                    Console.WriteLine("No valid number selected. Defaulting to play the game...");
                    game.Start(player, time);
                    break;
            }
         
            //Ends the Game
            game.End();
        }



    }
}

    