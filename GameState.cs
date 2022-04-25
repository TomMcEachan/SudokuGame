using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Reflection;

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
        private int[] _gameBoardAtStartArray;
        private string _playerName;
        private string _gameName;
        private const string DATE_FORMAT = "dd-MM-yy  HH.mm.ss";
        private Stack<Move> _gameMoves;
        private string location = AppDomain.CurrentDomain.BaseDirectory + @"\SudokuGame\SaveData";
        private TimeSpan _timeTaken;

        //Getters and Setters
        public int[] SolvedBoardArray { get => _solvedBoardArray; set => _solvedBoardArray = value; }
        public int[] PlayerBoardArray { get => _playerBoardArray; set => _playerBoardArray = value; }
        public int[] GameBoardAtStartArray { get => _gameBoardAtStartArray; set => _gameBoardAtStartArray = value; }
        public string PlayerName { get => _playerName; set => _playerName = value; }
        public string GameName { get => _gameName; set => _gameName = value; }
        public Stack<Move> GameMoves { get => _gameMoves; set => _gameMoves = value; }
        public TimeSpan TimeTakenToComplete { get => _timeTaken; set => _timeTaken = value; }


        /// <summary>
        /// Constructor for the GameState object
        /// </summary>
        /// <param name="solvedArray"></param>
        /// <param name="playerArray"></param>
        /// <param name="player"></param>
        public GameState(int[] solvedArray, int[] playerArray, int[] gameBoardAtStart, Player player, string saveName, Stack<Move> moves, TimeSpan timeTakenToComplete)
        {
            SolvedBoardArray = solvedArray;
            PlayerBoardArray = playerArray;
            GameBoardAtStartArray = gameBoardAtStart;
            PlayerName = player.Name;
            GameName = saveName;
            GameMoves = moves;
            TimeTakenToComplete = timeTakenToComplete;
            
        }

        /// <summary>
        /// Empty constructor for the GameState object
        /// </summary>
        public GameState() { }

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
        public bool SaveGame(int[] solved, int[] player, int[] gameBoardAtStart, Player user, Stack<Move> moves, string saveName, TimeSpan timeTakenToComplete)
        {
            var SaveGame = new GameState(solved, player, gameBoardAtStart, user, saveName, moves, timeTakenToComplete);
            
            string folderName = location; //TODO: MAKE THIS LOCATION DYNAMIC
            System.IO.Directory.CreateDirectory(folderName);
            string fileName = user.Name + "  " + saveName + DateTime.Now.ToString(DATE_FORMAT) + ".json";
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
        public GameState LoadGame(string loadPath)
        {
            GameState state = Utilities.ReadSaveFile<GameState>(loadPath);

            return state;

        }

        public List<String> ReadSavesInDirectory()
        {
            int num = 1;
            DirectoryInfo directory = new DirectoryInfo(location);
            FileInfo[] files = directory.GetFiles("*.json");
            List<string> fileList = new List<string>();
            Console.WriteLine("\n");

            foreach (FileInfo f in files)
            {
                Console.WriteLine(num.ToString() + ": " + f.Name + "");
                fileList.Add(f.Name);
                num++;
            }

            return fileList;
        }

        public int ReadNumberOfFilesInDirectory()
        {
            int num = 1;
            DirectoryInfo directory = new DirectoryInfo(location);
            FileInfo[] files = directory.GetFiles("*.json");
            List<string> fileList = new List<string>();

            foreach (FileInfo f in files)
            {
                fileList.Add(f.Name);
                num++;
            }

            int numOfFiles = fileList.Count;

            return numOfFiles;

        }
    }

}
