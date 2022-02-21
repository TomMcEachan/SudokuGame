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

        public static int easy = 9;
        public static int medium = 12;
        public static int hard = 14;
        public static int extreme = 18;
        public static int selectedModeValue;
        public static int selectedModeInt;

        public static int [,] emptyGrid =
            {
            {1, 0, 6, 0, 0, 0, 0, 0, 0},
            {7, 0, 0, 8, 9, 0, 0, 0, 0},
            {0, 8, 0, 7, 0, 0, 6, 0, 0},
            {0, 6, 0, 0, 1, 0, 8, 2, 0},
            {3, 0, 0, 4, 2, 9, 0, 0, 0},
            {0, 2, 1, 0, 8, 0, 0, 9, 0},
            {0, 0, 3, 0, 0, 8, 0, 5, 0},
            {0, 0, 0, 0, 6, 4, 0, 0, 9},
            {0, 0, 0, 1, 0, 0, 2, 0, 4}
            };

        public static int [,] testGrid =
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

        public static int[,] generatedGrid;
        static void printBoard(int[,] grid)
        {
            Console.WriteLine("+-----+-----+-----+");

            for (int x = 1; x < 10; x++)
            {
                for (int y = 1; y < 10; y++)
                    Console.Write("|{0}", grid[x - 1, y - 1]);

                Console.WriteLine("|");
                if (x % 3 == 0) Console.WriteLine("+-----+-----+-----+");
            }
        }

        static int printIntro()
        {
            Console.WriteLine(FiggleFonts.Doom.Render("Sudoku Generator!"));
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Welcome! This program generates unique, solveable Sudoku puzzles.");
            Console.WriteLine("To generate a puzzle, please select a difficulty level from the following options by typing 1, 2, 3, or 4.\n\n" +
                                " 1 - Easy (Produces a simple 9x9 Sudoku puzzle to solve)\n" +
                                " 2 - Medium (Produces a 12x12 Sudoku puzzle to solve)\n" +
                                " 3 - Hard (Produces a 14x14 Sudoku puzzle to solve)\n" +
                                " 4 - Extreme (Produces a 18x18 Sudoku puzzle to solve\n\n");
            
            String selectedModeString = Console.ReadLine();
            int selectedModeNumber = Convert.ToInt32(selectedModeString);
            return selectedModeNumber;
        }


        static void setSelectedOption(int selectedMode)
        {       
            if (selectedMode == 1)
            {
                selectedModeValue = easy;
                Console.WriteLine("Generating an easy Sudoku puzzle....");
            }
            else if (selectedMode == 2)
            {
                selectedModeValue = medium;
                Console.WriteLine("Generating a medium Sudoku puzzle....");
            }
            else if (selectedMode == 3)
            {
                selectedModeValue = hard;
                Console.WriteLine("Generating a hard Sudoku puzzle....");
            }
            else if (selectedMode == 4)
            {
                selectedModeValue = extreme;
                Console.WriteLine("Generating an extreme Sudoku puzzle....");
            } 
            else
            {
                selectedModeValue = easy;
                Console.WriteLine("Generating a standard Sudoku puzzle....");
            }

        }
        static void Main(string[] args)
        {

            Generate generate = new Generate();
            
            int selected = printIntro();
            setSelectedOption(selected);
            printBoard(testGrid);

            Console.WriteLine("Do you want to solve this board?");
            Console.ReadLine();

            Solve.SolveGrid(testGrid);
            //generate.GenerateGrid(selected);
            printBoard(testGrid);
            Console.ReadLine();

        }
    }
}