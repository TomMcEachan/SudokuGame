using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    class Player
    {
        //Global Variables
        private static string _name; //This represents the players name and will be used in the game save function

        //Getters & Setters
        public string Name { get => _name; set => _name = value; }
        /// <summary>
        /// Player Constructor
        /// </summary>
        /// <param name="name"></param>
        public Player(string name) 
        {
            Name = name;
        }

        /// <summary>
        /// Empty Player Constructor
        /// </summary>
        public Player () { }

        //--------------------------------------------------------------------------//

        /// <summary>
        /// This method finds out which column the player wants to place a number on
        /// </summary>
        /// <returns>
        /// This returns the players chosen column as an integer
        /// </returns>
        public int playerInputColumn()
        {
            bool colNumValid = false; 

            while (!colNumValid)
            {
                string playerPlacesColumn = "Which column would you like to place the number? (Select a number between 1 & 9) \n\n ";
                Console.WriteLine(playerPlacesColumn);
                string playerColumn = Console.ReadLine();
                int playerColumnInt;

                while (!int.TryParse(playerColumn, out playerColumnInt))
                {
                    Console.WriteLine("Not a valid selection - please try enter a number between 1 & 9");
                    playerColumn = Console.ReadLine();
                }

                if (Enumerable.Range(1, 9).Contains(playerColumnInt))
                {
                    colNumValid = true;
                    playerColumnInt--; //Removes one from the value to ensure it works with 2d matrix
                    return playerColumnInt;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That number is not valid. Please type a number between 1 and 9\n\n", Console.ForegroundColor);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;

            return 0;
        }


        /// <summary>
        /// This method finds out which row the player wants to place a number on
        /// </summary>
        /// <returns>
        /// This returns the players row as an Integer
        /// </returns>
        public int playerInputRow()
        {
            bool rowNumValid = false;

            while (!rowNumValid)
            {
                string playerPlacesRow = "Which row would you like to place the number?  (Select a number between 1 & 9)\n\n ";
                Console.WriteLine(playerPlacesRow);
                string playerRow = Console.ReadLine();
                int playerRowInt;
               
                while (!int.TryParse(playerRow, out playerRowInt))
                {
                    Console.WriteLine("Not a valid selection - please try enter a number between 1 & 9");
                    playerRow = Console.ReadLine();
                }


                if (Enumerable.Range(1, 9).Contains(playerRowInt))
                {
                    rowNumValid = true;
                    playerRowInt--; //Removes one from the value to ensure it works with the 2d matrix
                    return playerRowInt;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That number is not valid. Please type a number between 1 and 9\n\n", Console.ForegroundColor);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;

            return 0;
        }

        /// <summary>
        /// This method asks the player which number they would like to play at their selected point
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>
        /// The players selected number
        /// </returns>
        public int playerInputNumber(int row, int column)
        {
            row++;
            column++;
            string playerPlacesNumber = "Which number would you like to play at [" + row + "," + column + "] ? (Select a number between 1 & 9) \n\n";
            bool playerNumValid = false;

            while (!playerNumValid)
            {
                Console.WriteLine(playerPlacesNumber);
                string playerChoice = Console.ReadLine();
                int playerChoiceInt;

                while (!int.TryParse(playerChoice, out playerChoiceInt))
                {
                    Console.WriteLine("Not a valid selection - please try enter a number between 1 & 9");
                    playerChoice = Console.ReadLine();
                }
                Console.WriteLine();

                if (Enumerable.Range(1, 9).Contains(playerChoiceInt))
                {
                    playerNumValid = true;
                    return playerChoiceInt;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That number is not valid. Please type a number between 1 and 9\n\n", Console.ForegroundColor);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;

            return 0;
        }


        /// <summary>
        /// This takes a sudoku game board, flattens it into a 1d array and checks that if it contains any zeros. 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns>
        ///  True or False
        /// </returns>
        static bool ArrayContainsZero(int[,] grid)
        {
            int[] flatArray = { };
            var nums = grid.Cast<int>();

            foreach (int i in nums)
            {
                flatArray = flatArray.Append(i).ToArray();
            }


            if (flatArray.Contains(0))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// This method checks whether or not the player specified array is already filled or not
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="playerRow"></param>
        /// <param name="playerColumn"></param>
        /// <returns>
        /// True or False
        /// </returns>
        static bool AlreadyFilled(int[,] grid, int playerRow, int playerColumn)
        {
            int value = grid[playerRow, playerColumn];

            if (value == 0)
            {
                return false;
            }

            return true;
        }
       

        /// <summary>
        /// This method takes the generated board and allows the player to play the Sudoku game, turn by turn
        /// </summary>
        /// <param name="generatedBoard"></param>
        /// <returns>
        /// The players completed Sudoku board
        /// </returns>
        public int[,] playerInput(int[,] generatedBoard, int[,] solvedBoard, GameState state , Player play)
        {
            bool containsZero = true;
            bool alreadyFilled;
            int turn = 1;

            //Player Makes their choice until board is complete
            while (containsZero)
            {
                string TurnNum = $"Turn: {turn} \n\n";
                Console.WriteLine(TurnNum);

                int playerRow = playerInputRow();
                int playerColumn = playerInputColumn();

                alreadyFilled = AlreadyFilled(generatedBoard, playerRow, playerColumn);

                while (alreadyFilled)
                {
                    string message = "A number is already present here. Please select a different location";
                    Console.WriteLine(message);
                    playerRow = playerInputRow();
                    playerColumn = playerInputColumn();
                    alreadyFilled = AlreadyFilled(generatedBoard, playerRow, playerColumn);
                }             
                
                int playerNum = playerInputNumber(playerRow, playerColumn);
              
                generatedBoard[playerRow, playerColumn] = playerNum;
                containsZero = ArrayContainsZero(generatedBoard);

                Stack<Moves> gameMoves = new Stack<Moves>();

                gameMoves = playerTakesTurn(gameMoves, playerRow, playerColumn, playerNum);
                turn++;

                PlayerSavesGame(state, solvedBoard, generatedBoard, play, gameMoves);
                //TODO: PlayerLoadsGame(state);

                Utilities.printBoard(generatedBoard, 9);
                
            }

            return generatedBoard;
        }


        /// <summary>
        /// This method adds the player turn to the GameStack
        /// </summary>
        /// <param name="moves"></param>
        /// <param name="playerRow"></param>
        /// <param name="playerColumn"></param>
        /// <param name="playerNum"></param>
        /// <returns></returns>
        public Stack<Moves> playerTakesTurn(Stack<Moves> moves, int playerRow, int playerColumn, int playerNum)
        {

            int stackSize = moves.Count(); //Gets the current stack size
            stackSize++; //Increments the stack size by one

            Moves move = new Moves(playerNum, playerRow, playerColumn, stackSize);
           
            moves.Push(move); //Ads the new move and its data to the Stack of moves
            
            return moves; //Returns the stack of moves
        }


        public Stack<Moves> playerUndoTurn(Stack<Moves> moves)
        {
            
            



            return moves;
        }

        public Stack<Moves> playerRedoTurn(Stack<Moves> moves)
        {
            return moves;
        }

        /// <summary>
        /// This method saves the players current game data
        /// </summary>
        /// <param name="state"></param>
        /// <param name="solvedGrid"></param>
        /// <param name="playerGrid"></param>
        public void PlayerSavesGame(GameState state, int[,] solvedGrid, int[,] playerGrid, Player play, Stack<Moves> moves)
        {
            //Method variables
            bool saved;
            string saveName = default;
            
            //Converting 2D Matrix into 1D Array for Save
            int[] solved = Utilities.Convert2DArrayTo1D(solvedGrid);
            int[] player = Utilities.Convert2DArrayTo1D(playerGrid);

            //Prints a save message
            string saveMessage = "Saving.....\n\n";
            Console.WriteLine(saveMessage);

            //Prompts the player for a save name if none exists
            saveName = PlayerNamesSave(saveName);

            //Saves the game with the data provided
            saved = state.SaveGame(solved, player, play, moves, saveName);


            //If the game is saved a message is printed to the console
            if (saved)
            {
                string savedMessage = "Game Data Saved\n\n";
                Console.WriteLine(savedMessage);
            }

            //If the game is not saved for whatever reason, an error message is printed to the console
            if (!saved)
            {
                string unsavedMessage = "Error saving GameData";
                Console.WriteLine(unsavedMessage);
            }

        }

        public void PlayerLoadsGame(GameState state)
        {
            string path = "";
            state.LoadGame(path);
        }



        /// <summary>
        /// This method allows the player name their save if their save is not currently null
        /// </summary>
        /// <param name="saveName"></param>
        /// <returns>
        /// The players desired save name as a string
        /// </returns>
        public string PlayerNamesSave(string saveName)
        {
            
            if(String.IsNullOrEmpty(saveName))
            {
                string message = "What would you like to name your save?\n\n";
                Console.WriteLine(message);

                saveName = Console.ReadLine();
                return saveName;

            } 
            else
            {
                return saveName;
            }
                   
        }

    }


    
}
