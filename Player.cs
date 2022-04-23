using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuGame
{
    class Player
    {
        //Global Variables
        private static string _name; //This represents the players name and will be used in the game save function
        public Stack<Move> GameMoves = new Stack<Move>();
        public List<List<int>> movesList = new List<List<int>>();
        


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
            bool previousMove = false;
            int turn = 1;
            string saveName = default;
            string undoMessage = "If you would like to undo your previous move(s) press the left arrow (<---). \n\n\n" +
                                 "If you would like to redo the move you just undid press the right arrow (--->). \n\n\n" +
                                 "You can continue to do this until you are happy. Otherwise ENTER to continue...";
         
            //Player Makes their choice until board is complete
            while (containsZero)
            {
                string TurnNum = $"Turn: {turn} \n\n"; //Prints the turn num
                Console.WriteLine(TurnNum);

                if (GameMoves.Any())
                {
                    while (previousMove)
                    {
                        Console.WriteLine(undoMessage);
                        bool instructionsPrinted = true;

                        ConsoleKey key;
                        key = Console.ReadKey(false).Key;

                        while (instructionsPrinted)
                        {
                            switch (key)
                            {
                                case ConsoleKey.LeftArrow:
                                    Console.WriteLine("Undoing move");
                                    PlayerUndoesTurn();
                                    key = Console.ReadKey(false).Key;
                                    break;
                                case ConsoleKey.RightArrow:
                                    Console.WriteLine("Redoing move");

                                    key = Console.ReadKey(false).Key;
                                    break;
                                case ConsoleKey.Enter:
                                    instructionsPrinted = false;
                                    previousMove = false;
                                    break;
                            }
                        }

                    }
                } else if (GameMoves.Count > 0)
                {
                    Console.WriteLine("Continuing....");
                }


                int playerRow = playerInputRow(); //Gets the players choice of row
                int playerColumn = playerInputColumn(); //Gets the players choice of column

                alreadyFilled = Utilities.AlreadyFilled(generatedBoard, playerRow, playerColumn); //Checks if the players space is already full

                //Loops while the player space is already filled until the player selects a space that isn't
                while (alreadyFilled)
                {
                    string message = "A number is already present here. Please select a different location";
                    Console.WriteLine(message);
                    playerRow = playerInputRow();
                    playerColumn = playerInputColumn();
                    alreadyFilled = Utilities.AlreadyFilled(generatedBoard, playerRow, playerColumn);
                }                    

                int playerNum = playerInputNumber(playerRow, playerColumn); //Gets the players choice of number for the empty space
              
                generatedBoard[playerRow, playerColumn] = playerNum; //Appends the players choice of num to the generated board to their selected index
                containsZero = Utilities.ArrayContainsZero(generatedBoard); //Checks if the generated board still contains space for more numbers

                Move move = new Move(playerNum, playerRow, playerColumn);
                GameMoves.Push(move);

                //movesList.Add(new List<int> { playerNum, playerRow, playerColumn });
                
                //foreach (Move m in GameMoves)
                //{
                    //Console.WriteLine(m.ColumnLocation + " " + m.RowLocation);    
                //}
                
                turn++; //Increments the player turn
                previousMove = true;
                
                Utilities.printBoard(generatedBoard, 9);
                
            }

            _ = PlayerSavesGame(state, solvedBoard, generatedBoard, play, GameMoves, saveName); //Saves the player data as a JSON file

            return generatedBoard;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        public Move PlayerUndoesTurn()
        {
            Move previousMove = GameMoves.Pop();
           

            return previousMove;
        }

        public Stack<Move> PlayerRedoesTurn(Stack<Move> moves, Move move)
        {
            moves.Push(move);

            return moves;
        }


        /// <summary>
        /// This method saves the players current game data
        /// </summary>
        /// <param name="state"></param>
        /// <param name="solvedGrid"></param>
        /// <param name="playerGrid"></param>
        public string PlayerSavesGame(GameState state, int[,] solvedGrid, int[,] playerGrid, Player play, Stack<Move> moves, string saveName)
        {
            //Method variables
            bool saved;
            
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
                return saveName;
            }

            //If the game is not saved for whatever reason, an error message is printed to the console
            if (!saved)
            {
                string unsavedMessage = "Error saving GameData";
                Console.WriteLine(unsavedMessage);
                return null;
            }

            return null;
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
