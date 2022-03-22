using System;


namespace SudokuGame
{
  class Shuffle
  {
       void ShuffleNums(int size, int [,] grid)
        {
            Random rng = new Random();
            for (int x = 0; x < size; x++)
            {
                int random = rng.Next(size);
                Swap(x, random, size, grid);
            }
        }

        private void Swap(int n1, int n2, int size, int[,] grid)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (grid[x,y] == n1)
                    {
                        grid[x, y] = n2;
                    } else if (grid[x,y] == n2)
                    {
                        grid[x, y] = n1;
                    }
                }
            }
        }


  } 
} 
