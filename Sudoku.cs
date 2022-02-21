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

        static void printBoard()
        {
            Console.WriteLine("+-----+-----+-----+");

            for (int x = 1; x < 10; x++)
            {
                for (int y = 1; y < 10; y++)
                    Console.Write("|{0}", emptyGrid[x - 1, y - 1]);

                Console.WriteLine("|");
                if (x % 3 == 0) Console.WriteLine("+-----+-----+-----+");
            }
        }

        static int printIntro()
        {
            Console.WriteLine(FiggleFonts.Doom.Render("Sudoku Generator!"));
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Welcome! This program generates unique, solveable Sudoku puzzles.");
            Console.WriteLine("To generate a puzzle, please select a difficulty level from the following options (please select 1, 2, 3, or 4) \n\n" +
                                " 1 - Easy (Produces a simple 9x9 Sudoku puzzle to solve)\n" +
                                " 2 - Medium (Produces a 12x12 Sudoku puzzle to solve)\n" +
                                " 3 - Hard (Produces a 14x14 Sudoku puzzle to solve)\n" +
                                " 4 - Extreme (Produces a 18x18 Sudoku puzzle to solve");
            Console.WriteLine();
            String selectedModeString = Console.ReadLine();
            int selectedModeInt = Convert.ToInt32(selectedModeString);
            return selectedModeInt;
        }
        static void Main(string[] args)
        {

            Generate generate = new Generate();
            Solve solve = new Solve();
            int selected = printIntro();

            solve.SolveBoard(0, 0, emptyGrid);
            generate.GenerateGrid(selected);
            printBoard();
            Console.ReadLine();

        }
    }
}