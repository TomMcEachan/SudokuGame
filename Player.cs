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
        public List<List<int>> MovesList = new List<List<int>>();
        Stack<Move> DiscardedMoves = new Stack<Move>();
        Stack<Move> PossibleRedos = new Stack<Move>();
        public int NumOfMoves;
        public Move DiscardedMove { get; set; }
        TimeSpan gameTime { get; set; }
        TimeSpan startTime { get; set; }
        TimeSpan endTime { get; set; }
        TimeSpan timeTaken { get; set; }
        TimeSpan timeLeft { get; set; }
        bool start { get; set; }


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
        public Player() { }

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
                    Console.ForegroundColor = ConsoleColor.Green;
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
                    Console.ForegroundColor = ConsoleColor.Green;
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
                    Console.ForegroundColor = ConsoleColor.Green;
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
        public int[,] playerInputTimed(int[,] generatedBoard, int[,] solvedBoard, GameState state, Player play, Timer time)
        {
            bool containsZero = true;
            bool alreadyFilled;
            bool previousMove = false;
            bool firstMove = true;
            bool movesEmpty = false;
            int turn = 1;
            TimeSpan zero = TimeSpan.Zero;
            string saveName = default;
            string undoMessage = "Would you like to undo/redo a move? \n\n";

            int[,] startBoard = ((int[,])generatedBoard.Clone()); //Creates a clone of the generated board to allow for user manipulation

            start = true;

            //Player Makes their choice until board is complete
            while (containsZero)
            {
                if (time != null)
                {
                    if (start)
                    {
                        gameTime = time.TimeAmmount;
                        timeLeft = gameTime;
                    }

                    if (!start)
                    {
                        timeLeft = time.CalculateTimeLeft(timeTaken, timeLeft);
                    }

                    startTime = time.GetStartTime(); //Gets the start time
                }

                if (timeLeft > zero)
                {
                    //Prints Turn Data
                    Console.WriteLine("Turn Data");
                    Console.WriteLine("-------------");
                    Console.WriteLine($"Turn: {turn}");
                    if (time != null)
                    {
                        string format = timeLeft.ToString();
                        Console.WriteLine($"Minutes and seconds left: {format}");
                    }
                    Console.WriteLine("---------------");



                    if (GameMoves.Any())
                    {
                        while (previousMove)
                        {
                            Console.WriteLine(undoMessage);
                            bool instructionsPrinted = true;
                            string answer = Console.ReadLine();

                            while (instructionsPrinted)
                            {
                                switch (answer)
                                {
                                    case "Y":
                                    case "y":
                                    case "yes":
                                    case "YES":
                                    case "Yes":
                                        movesEmpty = UndoRedoUntilEmpty(generatedBoard);
                                        previousMove = false;
                                        instructionsPrinted = false;
                                        firstMove = false;
                                        break;
                                    case "N":
                                    case "n":
                                    case "no":
                                    case "NO":
                                    case "No":
                                        instructionsPrinted = false;
                                        previousMove = false;
                                        firstMove = false;
                                        Console.WriteLine("Continuing....\n\n");
                                        Utilities.printBoard(generatedBoard, 9);
                                        break;
                                    default:
                                        instructionsPrinted = false;
                                        previousMove = false;
                                        firstMove = false;
                                        Console.WriteLine($"{answer} is not a valid selection. Continuing....\n\n");
                                        Utilities.printBoard(generatedBoard, 9);
                                        break;
                                }
                            }

                        }
                    }

                    if (movesEmpty && !firstMove)
                    {
                        Console.WriteLine("No more moves left undo/redo");
                    }

                    if (!firstMove)
                    {
                        Console.WriteLine("\nNew move starting....\n");
                        Utilities.printBoard(generatedBoard, 9);
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

                    if (time != null)
                    {
                        endTime = time.GetEndTime();
                        timeTaken = time.CalculateTimeTaken(startTime, endTime);
                    }

                    previousMove = true;

                    if (saveName == null)
                    {
                        saveName = PlayerSavesGameWithName(state, solvedBoard, generatedBoard, startBoard, play, GameMoves, saveName, timeTaken); //Asks the player what they want to name their files and saves the data as a JSON file
                    }
                    else
                    {
                        PlayerSavesGameNoName(state, solvedBoard, generatedBoard, startBoard, play, GameMoves, saveName, timeTaken); //Saves the data as a JSON file with the save name previously selected
                    }

                    Utilities.printBoard(generatedBoard, 9);

                    start = false;
                }

                if (timeLeft < zero)
                {
                    Console.WriteLine("You Lose - you ran out of time!");
                    containsZero = false;
                }


            }
            return generatedBoard;
        }

        /// <summary>
        /// This method takes the generated board and allows the player to play the Sudoku game, turn by turn. It saves the game each turn too. 
        /// </summary>
        /// <param name="generatedBoard"></param>
        /// <returns>
        /// The players completed Sudoku board
        /// </returns>
        public int[,] playerInputNoTime(int[,] generatedBoard, int[,] solvedBoard, GameState state, Player play)
        {
            bool containsZero = true;
            bool alreadyFilled;
            bool previousMove = false;
            bool firstMove = true;
            bool movesEmpty = false;
            int turn = 1;
            TimeSpan zero = TimeSpan.Zero;
            string saveName = default;
            string undoMessage = "Would you like to undo/redo a move? \n\n";

            int[,] startBoard = ((int[,])generatedBoard.Clone()); //Creates a clone of the generated board to allow for user manipulation

            start = true;

            //Player Makes their choice until board is complete
            while (containsZero)
            {

                //Prints Turn Data
                Console.WriteLine("Turn Data");
                Console.WriteLine("-------------");
                Console.WriteLine($"Turn: {turn}");
                Console.WriteLine("---------------");

                if (GameMoves.Any())
                {
                    while (previousMove)
                    {
                        Console.WriteLine(undoMessage);
                        bool instructionsPrinted = true;
                        string answer = Console.ReadLine();

                        while (instructionsPrinted)
                        {
                            switch (answer)
                            {
                                case "Y":
                                case "y":
                                case "yes":
                                case "YES":
                                case "Yes":
                                    movesEmpty = UndoRedoUntilEmpty(generatedBoard);
                                    previousMove = false;
                                    instructionsPrinted = false;
                                    firstMove = false;
                                    break;
                                case "N":
                                case "n":
                                case "no":
                                case "NO":
                                case "No":
                                    instructionsPrinted = false;
                                    previousMove = false;
                                    firstMove = false;
                                    Console.WriteLine("Continuing....\n\n");
                                    Utilities.printBoard(generatedBoard, 9);
                                    break;
                                default:
                                    instructionsPrinted = false;
                                    previousMove = false;
                                    firstMove = false;
                                    Console.WriteLine($"{answer} is not a valid selection. Continuing....\n\n");
                                    Utilities.printBoard(generatedBoard, 9);
                                    break;
                            }
                        }

                    }
                }

                if (movesEmpty && !firstMove)
                {
                    Console.WriteLine("No more moves left undo/redo");
                }

                if (!firstMove)
                {
                    Console.WriteLine("\nNew move starting....\n");
                    Utilities.printBoard(generatedBoard, 9);
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

                if (saveName == null)
                {
                    saveName = PlayerSavesGameWithName(state, solvedBoard, generatedBoard, startBoard, play, GameMoves, saveName, timeTaken); //Asks the player what they want to name their files and saves the data as a JSON file
                }
                else
                {
                    PlayerSavesGameNoName(state, solvedBoard, generatedBoard, startBoard, play, GameMoves, saveName, timeTaken); //Saves the data as a JSON file with the save name previously selected
                }

                Utilities.printBoard(generatedBoard, 9);

                start = false;
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
        public Tuple<int[,], Move, bool> PlayerUndoesTurn(int[,] generatedBoard)
        {
            List<Move> tempList = new List<Move>();
            List<int> rowList = new List<int>();
            List<int> columnList = new List<int>();
            int playerRow = 0;
            int playerColumn = 0;
            bool movesEmpty = false;
            Move move = null;




            if (GameMoves.Any() == true)
            {
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


                if (rowList.Any() == true)
                {
                    playerRow = rowList.ElementAt(0);
                    playerColumn = columnList.ElementAt(0);
                    generatedBoard[playerRow, playerColumn] = 0;
                    move = GameMoves.Pop();
                }
            }
            else
            {
                movesEmpty = true;
            }

            return new Tuple<int[,], Move, bool>(generatedBoard, move, movesEmpty);

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
        public Tuple<int[,], bool> PlayerRedoesTurn(Stack<Move> possibleRedos, int[,] generatedBoard)
        {
            List<Move> tempList = new List<Move>();
            List<int> rowList = new List<int>();
            List<int> columnList = new List<int>();
            List<int> playerNumList = new List<int>();
            bool movesEmpty = false;
            int playerRow = 0;
            int playerColumn = 0;
            int playerNum = 0;

            if (possibleRedos.Any() == true)
            {
                foreach (var m in possibleRedos)
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

                playerRow = rowList.ElementAt(0);
                playerColumn = columnList.ElementAt(0);
                playerNum = playerNumList.ElementAt(0);
            }
            else
            {

                movesEmpty = true;
            }


            generatedBoard[playerRow, playerColumn] = playerNum;

            return new Tuple<int[,], bool>(generatedBoard, movesEmpty);

        }


        /// <summary>
        /// This method saves the players current game data
        /// </summary>
        /// <param name="state"></param>
        /// <param name="solvedGrid"></param>
        /// <param name="playerGrid"></param>
        public string PlayerSavesGameWithName(GameState state, int[,] solvedGrid, int[,] playerGrid, int[,] gameBoardAtStart, Player play, Stack<Move> moves, string saveName, TimeSpan timeTakenToComplete)
        {
            //Method variables
            bool saved;

            //Converting 2D Matrix into 1D Array for Save
            int[] solved = Utilities.Convert2DArrayTo1D(solvedGrid);
            int[] player = Utilities.Convert2DArrayTo1D(playerGrid);
            int[] startBoard = Utilities.Convert2DArrayTo1D(gameBoardAtStart);

            //Prints a save message
            string saveMessage = "Saving.....\n\n";
            Console.WriteLine(saveMessage);

            //Prompts the player for a save name if none exists
            saveName = PlayerNamesSave(saveName);

            //Saves the game with the data provided
            saved = state.SaveGame(solved, player, startBoard, play, moves, saveName, timeTakenToComplete);


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
        public void PlayerSavesGameNoName(GameState state, int[,] solvedGrid, int[,] playerGrid, int[,] gameBoardAtStart, Player play, Stack<Move> moves, string saveName, TimeSpan timeTakenToComplete)
        {
            //Method variables
            bool saved;

            //Converting 2D Matrix into 1D Array for Save
            int[] solved = Utilities.Convert2DArrayTo1D(solvedGrid);
            int[] player = Utilities.Convert2DArrayTo1D(playerGrid);
            int[] startBoard = Utilities.Convert2DArrayTo1D(gameBoardAtStart);

            //Prints a save message
            string saveMessage = "Saving.....\n\n";
            Console.WriteLine(saveMessage);

            //Saves the game with the data provided
            saved = state.SaveGame(solved, player, startBoard, play, moves, saveName, timeTakenToComplete);

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

            if (String.IsNullOrEmpty(saveName))
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


        public bool UndoRedoUntilEmpty(int[,] generatedBoard)
        {
            bool movesEmpty = false;
            ConsoleKey keyDown;



            while (!movesEmpty)
            {
                Console.WriteLine("Press the left arrow to undo a move(<---)\n\n" +
                                    "Press the right arrow to redo the most recent undo (--->)\n\n" +
                                    "Otherwise press the ENTER key to continue...\n\n");

                keyDown = Console.ReadKey(false).Key;

                if (keyDown == ConsoleKey.RightArrow)
                {
                    Console.WriteLine("\nRedoing last move...");

                    if (DiscardedMoves.Any() == true)
                    {
                        for (int i = 0; i < DiscardedMoves.Count(); i++)
                        {
                            Move move = DiscardedMoves.Pop();
                            PossibleRedos.Push(move);
                        }
                    }

                    Tuple<int[,], bool> t = PlayerRedoesTurn(PossibleRedos, generatedBoard);

                    generatedBoard = t.Item1;
                    movesEmpty = t.Item2;
                    Utilities.printBoard(generatedBoard, 9);
                }

                if (keyDown == ConsoleKey.LeftArrow)
                {
                    Console.WriteLine("\nUndoing last move...");
                    Tuple<int[,], Move, bool> t = PlayerUndoesTurn(generatedBoard);

                    generatedBoard = t.Item1;
                    DiscardedMove = t.Item2;
                    movesEmpty = t.Item3;

                    DiscardedMoves.Push(DiscardedMove);
                    Utilities.printBoard(generatedBoard, 9);
                    NumOfMoves = GameMoves.Count();
                }

                if (keyDown == ConsoleKey.Enter)
                {
                    movesEmpty = true;
                    Console.WriteLine("\nContinuing....\n\n");
                }
            }
            return movesEmpty;
        }

    }



}
