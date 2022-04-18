using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    class Print
    {
        /// <summary>
        /// This takes the generated board and prints it based on user selection
        /// </summary>
        /// <param name="selectedInt"></param>
        /// <param name="grid"></param>
        public int print(int selectedInt, int[,] grid)
        {
            int num;
            switch (selectedInt)
            {
                case 1:
                    num = 9;
                    printBoard(grid, num);
                    break;
                case 2:
                    num = 9;
                    printBoard(grid, num);
                    break;
                case 3:
                    num = 9;
                    printBoard(grid, num);
                    break;
                case 4:
                    num = 12;
                    printBoard(grid, num);
                    break;
                default:
                    num = 9;
                    printBoard(grid, num);
                    break;
            }

            return num;

        }


        /// <summary>
        /// A utility method to print solved/generated Sudoku board
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="size"></param>
        public void printBoard(int[,] grid, int size)
        {
            //string[,] stringGrid = grid.ToString();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write("| ");
                    }
                    Console.Write(grid[i, j] + " ");
                }

                if (i % 3 == 2 && i != 8)
                {
                    Console.WriteLine();
                    Console.Write("------+-------+------");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
