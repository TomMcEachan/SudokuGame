using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    class AutoPlay
    {
        //Variables
        public GameState AutoPlayState { get; set; }

        public Stack<int[,]> GameBoardByTurn { get; set; }

        
        //AutoPlay Constructor
        public AutoPlay (GameState autoPlayState)
        {
            AutoPlayState = autoPlayState;           
        }

        //Empty AutoPlay Constructor
        public AutoPlay() { }

        //------------------------------------------------------METHODS------------------------------------------------------------------//

        /// <summary>
        /// Gets previous game data from memory creates an AutoPlay object from it. 
        /// </summary>
        /// <returns>
        /// An autoplay object with data from memory
        /// </returns>
        public AutoPlay GetAutoPlayData()
        {
            List<string> fileList = new List<String>();
            GameState loadedState = new GameState();
            int numberOfFilesInDirectory = loadedState.ReadNumberOfFilesInDirectory();
            fileList = loadedState.ReadSavesInDirectory();

            Console.WriteLine("Which game would you like to replay? Select the corresponding number and press ENTER. (e.g, 9).");
            string selectedNumString = Console.ReadLine();
            Int32.TryParse(selectedNumString, out int selectedNumInt);

            if (selectedNumInt > 0)
            {
                selectedNumInt--;
            }

            string path = @"C:\SudokuGame\SaveData\" + fileList.ElementAt(selectedNumInt);
            loadedState = loadedState.LoadGame(path);

            AutoPlay autoPlay = new AutoPlay(loadedState);

            return autoPlay;
        }


        public void AutoPlayGame(AutoPlay autoplay)
        {
            string autoPlayMessage = "";
            Console.WriteLine(autoPlayMessage);



        }




    }
}
