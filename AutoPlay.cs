using System;
using System.Collections.Generic;
using System.Linq;


namespace SudokuGame
{
    class AutoPlay
    {
        //Variables
        public GameState AutoPlayState { get; set; }

        public Stack<int[,]> GameBoardByTurn { get; set; }


        //AutoPlay Constructor
        public AutoPlay(GameState autoPlayState)
        {
            AutoPlayState = autoPlayState;
        }

        //Empty AutoPlay Constructor
        public AutoPlay() { }

        //------------------------------------------------------METHODS------------------------------------------------------------------//


        /// <summary>
        /// Main Auto Replay logic
        /// </summary>
        /// <returns>
        /// Whether or not there is any save data available
        /// </returns>
        public Tuple<bool, bool> AutoPlayGame()
        {
            string autoPlayMessage = "\n\nThis mode will auto replay a previous game saved in data, showing you the moves made in the game one by one.\n";
            Console.WriteLine(autoPlayMessage);

            Tuple<GameState, bool> t = GetAutoPlayData();
            GameState state = t.Item1;
            bool saveData = t.Item2;
            bool playGame = true;
            List<Move> moveList = new List<Move>();
            List<int[,]> turnList = new List<int[,]>();
            Stack<Move> moveStack = state.GameMoves;

            if (saveData)
            {
                foreach (var m in moveStack)
                {
                    moveList.Add(m);
                }

                Console.WriteLine("*Printing GameBoard at Start of Game*");
                Utilities.printBoard(Utilities.Convert1DArrayTo2D(state.GameBoardAtStartArray, 9, 9), 9);

                foreach (Move m in moveList)
                {
                    turnList.Add(Utilities.Convert1DArrayTo2D(m.GameBoard, 9, 9));
                }

                playGame = TakeTurnByTurn(turnList);
            }

            return new Tuple<bool, bool>(saveData, playGame);
        }


        /// <summary>
        /// Gets previous game data from memory creates an AutoPlay object from it. 
        /// </summary>
        /// <returns>
        /// An autoplay object with data from memory
        /// </returns>
        public Tuple<GameState, bool> GetAutoPlayData()
        {
            GameState loadedState = new GameState();
            List<string> fileList = loadedState.ReadSavesInDirectory();
            int numberOfFilesInDirectory = loadedState.ReadNumberOfFilesInDirectory();
            bool saveData = true;
            int selectedNumInt;

            if (numberOfFilesInDirectory != 0)
            {
                Console.WriteLine("\nWhich game would you like to replay? Select the corresponding number and press ENTER. (e.g, 9).");
                string selectedNumString = Console.ReadLine();

                while (!int.TryParse(selectedNumString, out selectedNumInt))
                {
                    Console.WriteLine("\nThat is not a valid selection. Please enter a corresponding number and press ENTER.");
                    selectedNumString = Console.ReadLine();
                }

                Console.WriteLine($"\n\nReplaying save number {selectedNumString}...\n\n");
                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();

                if (selectedNumInt > 0)
                {
                    selectedNumInt--;
                }

                string path = AppDomain.CurrentDomain.BaseDirectory + @"\SudokuGame\SaveData\" + fileList.ElementAt(selectedNumInt);
                loadedState = loadedState.LoadGame(path);
                saveData = true;

            }
            else if (numberOfFilesInDirectory == 0)
            {
                Console.WriteLine("Nice try - there are no save files available. Defaulting to play the game....");
                saveData = false;
            }

            return new Tuple<GameState, bool>(loadedState, saveData);
        }


        /// <summary>
        /// This method allows the user to see previous game play out turn by turn
        /// </summary>
        /// <param name="turnList"></param>
        /// <returns>
        /// True or False (whether or not the player wants to play a game)
        /// </returns>
        public bool TakeTurnByTurn(List<int[,]> turnList)
        {
            Queue<int[,]> TurnStack = new Queue<int[,]>();
            int turnNum = 1;

            foreach (int[,] item in turnList)
            {
                TurnStack.Enqueue(item);
            }

            for (int i = TurnStack.Count - 1; i >= 0; i--)
            {
                int[,] move = TurnStack.Dequeue();
                Console.WriteLine($"Printing move {turnNum}....\n");
                Utilities.printBoard(move, 9);
                turnNum++;
                Console.ReadKey();
            }

            Console.WriteLine("All moves complete!\n");
            Console.WriteLine("Would you like to play a game? (Y/N)");
            string answer = Console.ReadLine();
            bool playGame;

            switch (answer)
            {
                case "Y":
                case "y":
                case "Yes":
                case "yes":
                    playGame = true;
                    break;
                case "N":
                case "n":
                case "No":
                case "no":
                    playGame = false;
                    break;
                default:
                    Console.WriteLine("Not a valid selection. Defaulting to playing a new game...");
                    playGame = true;
                    break;
            }

            return playGame;
        }
    }
}
