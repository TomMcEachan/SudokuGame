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
        public static int[,] CheckSolve(int[,] grid, int size)
        {
            if (grid.Length == 0)
            {
                return null;
            }

            SolveGrid(grid, size);

            return grid;
        }


        //This method solves the grid
        public static bool SolveGrid(int[,] grid, int size)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == 0)
                    {
                        for (int z = 1; z <= size; z++)
                        {
                            if (isValid(grid, x, y, z))
                            {
                                grid[x, y] = z;

                                if (SolveGrid(grid, size))
                                    return true;
                                else
                                    grid[x, y] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }


        //This method checks if a row or column is valid
        private static bool isValid(int[,] grid, int row, int column, int size)
        {
            for (int x = 0; x < size; x++)
            {
                //check row  
                if (grid[x, column] != 0 && grid[x, column] == size)
                    return false;
                //check column  
                if (grid[row, x] != 0 && grid[row, x] == size)
                    return false;
                //check 3*3 block  
                if (grid[3 * (row / 3) + x / 3, 3 * (column / 3) + x % 3] != 0 && grid[3 * (row / 3) + x / 3, 3 * (column / 3) + x % 3] == size)
                    return false;
            }
            return true;
        }

    }
}




