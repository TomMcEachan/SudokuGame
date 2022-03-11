using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Figgle;

namespace SudokuGame
{
    class Sudoku
    {

        //Global Variables
        public static int easy = 6;
        public static int medium = 9;
        public static int hard = 12;
        public static int extreme = 18;
        public static int normal = 9;
        public static int selectedModeValue;
        public static int gridSizeNum;

        //Empty Boards of varying difficulty 
        public static int[,] emptyGridEasy = new int[6, 6];
        public static int[,] emptyGridMedium = new int[9, 9];
        public static int[,] emptyGridHard = new int[12, 12];
        public static int[,] emptyGridExtreme = new int[18, 18];


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
                                " 2 - Medium (Produces a 12x12 Sudoku puzzle to solve)\n" +
                                " 3 - Hard (Produces a 14x14 Sudoku puzzle to solve)\n" +
                                " 4 - Extreme (Produces a 18x18 Sudoku puzzle to solve\n\n");
            
            string selectedModeString = Console.ReadLine();

            int selectedModeInt;

            while (!int.TryParse(selectedModeString, out selectedModeInt))
            {
                Console.WriteLine("Try again");
                selectedModeString = Console.ReadLine();
            }

            return selectedModeInt;
        }


        


        
        static int[,] setSelectedOption(int selectedMode)
        {

            int[,] grid;

            switch (selectedMode)
            {
                case 1:
                    selectedModeValue = easy;
                    Console.WriteLine("Generating an easy Sudoku puzzle....");
                    gridSizeNum = easy;
                    grid = Solve.CheckSolve(emptyGridEasy, gridSizeNum);

                    break;
                case 2:
                    selectedModeValue = medium;
                    Console.WriteLine("Generating a medium Sudoku puzzle....");
                    gridSizeNum = medium;
                    grid = Solve.CheckSolve(emptyGridMedium, gridSizeNum);


                    break;
                case 3:
                    selectedModeValue = hard;
                    Console.WriteLine("Generating a hard Sudoku puzzle....");
                    gridSizeNum = hard;
                    grid = Solve.CheckSolve(emptyGridHard, gridSizeNum);
                    break;
                case 4:
                    selectedModeValue = extreme;
                    Console.WriteLine("Generating an extreme Sudoku puzzle....");
                    gridSizeNum = extreme;
                    grid = Solve.CheckSolve(emptyGridExtreme, gridSizeNum);
                    break;
                default:
                    selectedModeValue = medium;
                    Console.WriteLine("Generating a standard Sudoku puzzle....");
                    gridSizeNum = medium;
                    grid = Solve.CheckSolve(emptyGridEasy, gridSizeNum);
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
            Generate generate = new Generate();
            
            int selected = printIntro();
            int [,] selectedGrid = setSelectedOption(selected);
           
            print(selected, selectedGrid);
 
            //int[,] randomGrid = generate.GenerateGrid(emptyGridEasy);
            //printBoard(randomGrid, gridSizeNum);
            Console.ReadLine();

        }
    }
}