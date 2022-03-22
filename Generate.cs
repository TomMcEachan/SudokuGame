using System;

namespace SudokuGame
{
    class Generate
    {
      
        //This method checks if the grid is empty. If it is not empty it solves the grid.
        public static int[,] Create(Sudoku board)
        {
            //Object Variables
            int[,] grid = board.Grid;
            int size = board.Size;
            int sqr = Convert.ToInt32(Math.Sqrt(board.Size));


            if (grid.Length == 0 || grid == null)
            {
                return null;
            }

            GenerateGrid(grid, size, sqr);

            return grid;
        }


        public static void GenerateGrid(int[,] grid, int size, int sqr)
        {
            if (grid == null || grid.Length == 0)
                return;
            solve(grid, size, sqr);
        }
        private static bool solve(int[,] grid, int size, int sqr)
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

                                if (solve(grid, size, sqr))
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
                if (grid[sqr * (row / sqr) + i / sqr, sqr* (col / sqr) + i % sqr] != 0 && grid[sqr * (row / sqr) + i /sqr, sqr * (col / sqr) + i % sqr] == c)
                    return false;
            }
            return true;
        }

    }
}

  

