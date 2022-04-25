using System;


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
            Console.Title = "Sudoku Game";
            //Creates a new Instance of Sudoku
            Sudoku sudoku = new Sudoku();
            Console.ForegroundColor = ConsoleColor.White;


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

            //Creates an empty instance of Timer
            Timer time = new Timer();
            
            //Prints the intro and gets the player name
            string name = game.printIntro();
            Player player = new Player(name);

            int answerNum;

            Console.WriteLine("\nMAIN MENU\n" +
                              "-------------------------------------------------------------\n" +
                              "1. PLAY THE GAME\n" +
                              "2. AUTOREPLAY A PREVIOUS GAME (requires a previous save file)\n\n\n\n" +
                              "Please select an option to continue by typing either 1 or 2");

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
                    
                    if (!playGame && !saveData)
                    {
                        game.End();
                    } else if (playGame)
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

    