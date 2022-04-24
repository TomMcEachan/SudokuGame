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
        public int NumOfMoves;
        public Move DiscardedMove { get; set; }
        
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

        //--------------------------------------------------METHODS---------------------------------------------------------------//

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
        /// This method takes the generated board and allows the player to play the Sudoku game, turn by turn. It saves the game each turn too. 
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
            string undoMessage = "> If you would like to undo your previous move press the left arrow key (<---). \n\n" +
                                 "> If you would like to redo your previous undo press the right arrow key (--->). \n\n" +
                                 "> Otherwise press the ENTER key to continue...\n\n";
         
            //Player Makes their choice until board is complete
            while (containsZero)
            {
                string TurnNum = $"Turn: {turn}\n\n"; //Prints the turn num
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
                                    Console.WriteLine("*Undoing last move*");
                                    NumOfMoves = GameMoves.Count();
                                    if (NumOfMoves >= 0)
                                    {
                                        Tuple<int[,], Move> t = PlayerUndoesTurn(generatedBoard);

                                        generatedBoard = t.Item1;
                                        DiscardedMove = t.Item2;

                                        Utilities.printBoard(generatedBoard, 9);
                                        NumOfMoves = GameMoves.Count();
                                    }
                                    
                                    Console.WriteLine("Press the right arrow to redo the move (--->)\n\n" +
                                                       "Otherwise press the ENTER key to continue...\n\n");

                                    ConsoleKey keyDown;
                                    keyDown = Console.ReadKey(false).Key;

                                    if (keyDown == ConsoleKey.RightArrow)
                                    {
                                        Console.WriteLine("*Redoing last move*");
                                        generatedBoard = PlayerRedoesTurn(GameMoves, DiscardedMove, generatedBoard);
                                        Utilities.printBoard(generatedBoard, 9);
                                    }

                                    instructionsPrinted = false;
                                    previousMove = false;

                                    Console.WriteLine("Continuing....\n\n");
                                    break;
                                case ConsoleKey.RightArrow:
                                    if (DiscardedMove != null)
                                    {
                                        Console.WriteLine("*Redoing last move*");
                                        generatedBoard = PlayerRedoesTurn(GameMoves, DiscardedMove, generatedBoard);
                                        Utilities.printBoard(generatedBoard, 9);
                                        instructionsPrinted = false;
                                        previousMove = false;
                                    } else
                                    {
                                        Console.WriteLine("No move to redo!");
                                        instructionsPrinted = false;
                                        previousMove = false;
                                    }                                   
                                    break;
                                case ConsoleKey.Enter:
                                    instructionsPrinted = false;
                                    previousMove = false;
                                    Console.WriteLine("Continuing....\n\n");
                                    Utilities.printBoard(generatedBoard, 9);
                                    break;
                                default:
                                    instructionsPrinted = false;
                                    previousMove = false;
                                    Console.WriteLine("Continuing....\n\n");
                                    Utilities.printBoard(generatedBoard, 9);
                                    break;
                            }
                        }

                    }
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

                Move move = new Move(playerNum, playerRow, playerColumn, generatedBoard);
                GameMoves.Push(move);
        
                turn++; //Increments the player turn
                previousMove = true;
                
                if(saveName == null)
                {
                  saveName = PlayerSavesGameWithName(state, solvedBoard, generatedBoard, play, GameMoves, saveName); //Asks the player what they want to name their files and saves the data as a JSON file
                } else
                {
                    PlayerSavesGameNoName(state, solvedBoard, generatedBoard, play, GameMoves, saveName); //Saves the data as a JSON file with the save name previously selected
                }

                Utilities.printBoard(generatedBoard, 9);               
            }

            return generatedBoard;
        }

        
        /// <summary>
        /// This method undoes the players most recent turn
        /// </summary>
        /// <param name="generatedBoard"></param>
        /// <returns>
        /// This returns a tuple of <int[,], Move>
        /// </returns>
        public Tuple <int[,], Move> PlayerUndoesTurn(int[,] generatedBoard)
        {
            List<Move> tempList = new List<Move>();
            List<int> rowList = new List<int>();
            List<int> columnList = new List<int>();
            
            foreach (var m in GameMoves)
            {
                tempList.Add(m);
            }

            for (int i = 0; i < tempList.Count(); i++)
            {
                rowList.Add(tempList.ElementAt(i).RowLocation);
            }

            for (int i = 0; i < tempList.Count(); i++)
            {
                columnList.Add(tempList.ElementAt(i).ColumnLocation);
            }

            int playerRow = rowList.ElementAt(0);
            int playerColumn = columnList.ElementAt(0);

            generatedBoard[playerRow, playerColumn] = 0;

            Move move = GameMoves.Pop();
            return new Tuple<int[,], Move>(generatedBoard, move);

        }

        /// <summary>
        /// This method redoes the players most recent undo
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="move"></param>
        /// <param name="generatedBoard"></param>
        /// <returns>
        /// A reformatted player board
        /// </returns>
        public int[,] PlayerRedoesTurn(Stack<Move> stack, Move move, int[,] generatedBoard)
        {
            stack.Push(move);

            List<Move> tempList = new List<Move>();
            List<int> rowList = new List<int>();
            List<int> columnList = new List<int>();
            List<int> playerNumList = new List<int>();

            foreach (var m in stack)
            {
                tempList.Add(m);
            }

            for (int i = 0; i < tempList.Count(); i++)
            {
                rowList.Add(tempList.ElementAt(i).RowLocation);
            }

            for (int i = 0; i < tempList.Count(); i++)
            {
                columnList.Add(tempList.ElementAt(i).ColumnLocation);
            }

            for (int i = 0; i < tempList.Count(); i++)
            {
                playerNumList.Add(tempList.ElementAt(i).PlayerNum);
            }

            int playerRow = rowList.ElementAt(0);
            int playerColumn = columnList.ElementAt(0);
            int playerNum = playerNumList.ElementAt(0);

            generatedBoard[playerRow, playerColumn] = playerNum;

            return generatedBoard;
           
        }


        /// <summary>
        /// This method saves the players current game data
        /// </summary>
        /// <param name="state"></param>
        /// <param name="solvedGrid"></param>
        /// <param name="playerGrid"></param>
        public string PlayerSavesGameWithName(GameState state, int[,] solvedGrid, int[,] playerGrid, Player play, Stack<Move> moves, string saveName)
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


        /// <summary>
        /// This method saves the players current game data
        /// </summary>
        /// <param name="state"></param>
        /// <param name="solvedGrid"></param>
        /// <param name="playerGrid"></param>
        public void  PlayerSavesGameNoName(GameState state, int[,] solvedGrid, int[,] playerGrid, Player play, Stack<Move> moves, string saveName)
        {
            //Method variables
            bool saved;

            //Converting 2D Matrix into 1D Array for Save
            int[] solved = Utilities.Convert2DArrayTo1D(solvedGrid);
            int[] player = Utilities.Convert2DArrayTo1D(playerGrid);

            //Prints a save message
            string saveMessage = "Saving.....\n\n";
            Console.WriteLine(saveMessage);

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
