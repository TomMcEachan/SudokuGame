using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Figgle;

namespace SudokuGame
{
    class Sudoku
    {
        //Global Variables
        private static int[,] _grid; //This is the Sudoku Board (e.g, [9,9])
        private static int _size; //This the size of each row/column (e.g, 9) 
        private static int _difficulty; //This is the difficulty of the users game and removes the specified number of digits
      
        
        //Getters & Setters
        public int[,] Grid { get => _grid; set => _grid = value; }
        public int Size { get => _size; set => _size = value; }
        public int Difficulty { get => _difficulty; set => _difficulty = value; }
        


        /// <summary>
        /// This constructor represents a Sudoku Board
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="size"></param>
        /// <param name="difficulty"></param>
        public Sudoku(int[,] grid, int size, int difficulty)
        {
            Grid = grid;
            Size = size;
            Difficulty = difficulty;
        }

        

        //Test Grid
        public static int[,] testGrid =
        {
            {5, 3, 0, 0, 7, 0, 0, 0, 0},
            {6, 0, 0, 1, 9, 5, 0, 0, 0},
            {0, 9, 8, 0, 0, 0, 6, 0, 0},
            {8, 0, 0, 0, 6, 0, 0, 0, 3},
            {4, 0, 0, 8, 0, 3, 0, 0, 1},
            {7, 0, 0, 0, 2, 0, 0, 0, 6},
            {0, 6, 0, 0, 0, 0, 2, 8, 0},
            {0, 0, 0, 4, 1, 9, 0, 0, 5},
            {0, 0, 0, 0, 8, 0, 0, 7, 9}
        };

        /// <summary>
        /// Lets the player select the difficulty of the game they want to play.
        /// </summary>
        /// <returns>
        /// selectedModeInt
        /// </returns>
        static int printIntro()
        {
            Console.WriteLine(FiggleFonts.Doom.Render("Sudoku Generator!"));
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Welcome! This program generates unique, solvable Sudoku puzzles.");
            Console.WriteLine("To generate a puzzle, please select a difficulty level from the following options by typing 1, 2, 3, or 4.\n\n" +
                                " 1 - Easy (Produces a simple 9x9 Sudoku puzzle to solve)\n" +
                                " 2 - Medium (Produces a more difficult 9x9 Sudoku puzzle to solve)\n" +
                                " 3 - Hard (Produces a 12x12 Sudoku puzzle to solve)\n" +
                                " 4 - Extreme (Produces a 18x18 Sudoku puzzle to solve\n\n");

            string selectedModeString = Console.ReadLine();
            int selectedModeInt;

            while (!int.TryParse(selectedModeString, out selectedModeInt))
            {
                Console.WriteLine("Not a valid selection - please try enter a number between 1 & 4");
                selectedModeString = Console.ReadLine();
            }

            return selectedModeInt;

        }


        /// <summary>
        /// This method gets the user specified grid 
        /// </summary>
        /// <param name="selectedMode"></param>
        /// <returns>
        /// This returns the sudoku grid based on the parameters set by the player
        /// </returns>
        static int[,] getGrid(int selectedMode)
        {

            int[,] grid;
            

            switch (selectedMode)
            {
                case 1:
                    grid = new int [9, 9];
                    Console.WriteLine("\n\nGenerating an easy Sudoku puzzle....\n\n");
                    Sudoku easyBoard = new Sudoku(grid, 9, 34);
                    grid = Generate.Create(easyBoard);
                    break;
                case 2:
                    grid = new int[9, 9];
                    Console.WriteLine("\n\nGenerating a medium Sudoku puzzle....\n\n");
                    Sudoku mediumBoard = new Sudoku(grid, 9, 57);
                    grid = Generate.Create(mediumBoard);
                    break;
                case 3:
                    grid = new int[9, 9];
                    Console.WriteLine("\n\nGenerating a hard Sudoku puzzle....\n\n");
                    Sudoku hardBoard = new Sudoku(grid, 9, 64);
                    grid = Generate.Create(hardBoard);
                    break;
                case 4:
                    grid = new int[12, 12];
                    Console.WriteLine("\n\nGenerating an extreme Sudoku puzzle....\n\n");
                    Sudoku extremeBoard = new Sudoku(grid, 12, 31);
                    grid = Generate.Create(extremeBoard);
                    break;
                default:
                    grid = new int[9, 9];
                    Console.WriteLine("\n\nGenerating an easy Sudoku puzzle....\n\n");
                    Sudoku defaultBoard = new Sudoku(grid, 9, 57);
                    grid = Generate.Create(defaultBoard);
                    break;
            }

            return grid;
        }   

        /// <summary>
        /// The main method of the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            //Instantiate Classes
            Print p = new Print();
            Player player = new Player();

            //Variables
            int selected = printIntro();
            int[,] generatedBoard = getGrid(selected);
            int printNum;
            int sqr;
           
            //Prints the empty board
            printNum = p.print(selected, generatedBoard);
            sqr = Convert.ToInt32(Math.Sqrt(printNum));

            //Player Makes their choice until board is complete
            player.playerInput(generatedBoard, p);

            //Solves the generated board and stores it in memory
            Solve solve = new Solve();
            int[,] solvedBoard = solve.SolveGrid(generatedBoard, printNum, sqr);

            Console.ReadLine();

            p.print(selected, solvedBoard);

            Console.ReadLine();

        }
    }
}

    