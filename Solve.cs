using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    public class Solve
    {


        //This method checks if the grid is empty. If it is not empty it solves the grid.
        public int[,] SolveGrid(int[,] grid, int size, int sqr)
        {
            if (grid.Length == 0 || grid == null)
            {
                return null;
            }

            Solver(grid, size, sqr);

            return grid;
        }

        private static bool Solver(int[,] grid, int size, int sqr)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 0)
                    {
                        for (int c = 1; c <= size; c++)
                        {
                            if (isValid(grid, i, j, c, sqr))
                            {
                                grid[i, j] = c;

                                if (Solver(grid, size, sqr))
                                    return true;
                                else
                                    grid[i, j] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        private static bool isValid(int[,] grid, int row, int col, int c, int sqr)
        {
            for (int i = 0; i < 9; i++)
            {
                //check row  
                if (grid[i, col] != 0 && grid[i, col] == c)
                    return false;
                //check column  
                if (grid[row, i] != 0 && grid[row, i] == c)
                    return false;
                //check 3*3 block  
                if (grid[sqr * (row / sqr) + i / sqr, sqr * (col / sqr) + i % sqr] != 0 && grid[sqr * (row / sqr) + i / sqr, sqr * (col / sqr) + i % sqr] == c)
                    return false;
            }
            return true;
        }

    }

}





