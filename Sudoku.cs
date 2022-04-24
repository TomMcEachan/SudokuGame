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
            //Creates a new Instance of Sudoku
            Sudoku sudoku = new Sudoku();
            Console.Title = "Sudoku Game";

            System.IO.Directory.CreateDirectory(@"SudokuGame\SaveData");

            sudoku.SudokuStart();
        }


        public void SudokuStart()
        {
            //Creates a new instance of Game 
            Game game = new Game();

            //Creates a new instance of AutoPlay
            AutoPlay autoPlay = new AutoPlay();

            //Prints the intro and gets the player name
            string name = game.printIntro();
            Player player = new Player(name);

            bool playGame = false;

            Console.WriteLine("1. Play the Game\n" +
                              "2. Auto replay a previous Game\n" +
                              "3. Change Colour Settings\n");
            string answer = Console.ReadLine();

            
            
            //Starts the Game
            game.Start(player);

            //Ends the Game
            game.End();
        }



    }
}

    