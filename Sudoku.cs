using System;
using Figgle;

namespace SudokuGame
{
    class Sudoku
    {
        //Global Variables
        private static int[,] _grid;
        private static int _size;

        //Sudoku Object Constructor
        public Sudoku(int[,] grid, int size)
        {
            Grid = grid;
            Size = size;
        }

        //Getters & Setters
        public int[,] Grid 
        { 
            get => _grid; set => _grid = value; 
        }
        public int Size 
        { 
            get => _size; set => _size = value; 
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


        /* A utility method to print grid */
        static void printBoard(int[,] grid, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(grid[i, j] + " ");
                Console.WriteLine();
            }
        }

        //This method prints the intro of the game for the player
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


        //This method gets the user specified grid 
        static int[,] getGrid(int selectedMode)
        {

            int[,] grid;
            

            switch (selectedMode)
            {
                case 1:
                    grid = new int [9, 9];
                    Console.WriteLine("Generating an easy Sudoku puzzle....");
                    Sudoku easyBoard = new Sudoku(grid, 9);
                    Generate.Create(easyBoard);
                    break;
                case 2:
                    grid = new int[9, 9];
                    Console.WriteLine("Generating an easy Sudoku puzzle....");
                    Sudoku mediumBoard = new Sudoku(grid, 9);
                    Generate.Create(mediumBoard);
                    break;
                case 3:
                    grid = new int[9, 9];
                    Console.WriteLine("Generating an easy Sudoku puzzle....");
                    Sudoku hardBoard = new Sudoku(grid, 9);
                    Generate.Create(hardBoard);
                    break;
                case 4:
                    grid = new int[9, 9];
                    Console.WriteLine("Generating an easy Sudoku puzzle....");
                    Sudoku extremeBoard = new Sudoku(grid, 9);
                    Generate.Create(extremeBoard);
                    break;
                default:
                    grid = new int[9, 9];
                    Console.WriteLine("Generating an easy Sudoku puzzle....");
                    Sudoku defaultBoard = new Sudoku(grid, 9);
                    Generate.Create(defaultBoard);
                    break;
            }

            return grid;
        }

        static void print(int selectedInt, int[,] grid)
        {
            switch (selectedInt)
            {
                case 1:
                    printBoard(grid, 6);
                    break;
                case 2:
                    printBoard(grid, 9);
                    break;
                case 3:
                    printBoard(grid, 12);
                    break;
                case 4:
                    printBoard(grid, 18);
                    break;
                default:
                    printBoard(grid, 9);
                    break;
            }
        }








        //This is the main method of the program
        static void Main(string[] args)
        {
            int selected = printIntro();
            int[,] gameBoard = getGrid(selected);

            print(selected, gameBoard);


            Console.ReadLine();

        }
    }
}

    