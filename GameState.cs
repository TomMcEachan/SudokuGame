using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections;


namespace SudokuGame
{
    /// <summary>
    /// This class represents the game state. It provides methods for saving game data, undoing and redoing moves, and developing 
    /// </summary>
    class GameState
    {
        //Global Variables
        private int[] _solvedBoardArray;
        private int[] _playerBoardArray;
        private string _playerName;
        private string _gameName;
        private const string DATE_FORMAT = "dd-MM-yy  HH.mm.ss";

        //Getters and Setters
        public int[] solvedBoardArray { get=> _solvedBoardArray; set => _solvedBoardArray = value; }
        public int[] playerBoardArray { get => _playerBoardArray; set => _playerBoardArray = value; }
        public string playerName { get => _playerName; set => _playerName = value; }
        public string gameName { get => _gameName; set => _gameName = value; }
        
        
        /// <summary>
        /// Constructor for the GameState object
        /// </summary>
        /// <param name="solvedArray"></param>
        /// <param name="playerArray"></param>
        /// <param name="player"></param>
        public GameState (int[] solvedArray, int[] playerArray, Player player, string saveName)
        {
            solvedBoardArray = solvedArray;
            playerBoardArray = playerArray;
            playerName = player.Name;
            gameName = saveName;
        }
        
        /// <summary>
        /// Empty constructor for the GameState object
        /// </summary>
        public GameState () { }

        //----------------------------------------------------------------------------------------------//
 
        /// <summary>
        /// This method takes game data and passes it to a Json serializer method
        /// </summary>
        /// <param name="solved"></param>
        /// <param name="player"></param>
        /// <param name="user"></param>
        /// <returns>
        /// True or False
        /// </returns>
        public bool SaveGame (int[] solved, int[] player, Player user)
        {
            string message = "What would you like to name your save?";
            Console.WriteLine(message);
            string saveName = Console.ReadLine();
            
            var SaveGame = new GameState(solved, player, user, saveName);
     
            string folderName = @"C:\SudokuGame\SaveData";
            System.IO.Directory.CreateDirectory(folderName);
            string fileName = user.Name + "  " + saveName +  DateTime.Now.ToString(DATE_FORMAT) +".json";
            string pathString = System.IO.Path.Combine(folderName, fileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string data = JsonSerializer.Serialize(SaveGame, options);
            File.WriteAllText(pathString, data);
       
            return true;
        }

        
       

       
        
        /// <summary>
        /// Returns the specified JSON file
        /// </summary>
        /// <param name="loadPath"></param>
        /// <returns></returns>
        public GameState LoadGame (string loadPath)
        {
            GameState state = Utilities.ReadSaveFile<GameState>(loadPath);

            return state;
           
        }

    }
}
