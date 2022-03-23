using System;


namespace SudokuGame
{
  class Shuffle
  {

        Random rng = new Random();
        public void ShuffleGrid<T>(T[,] grid)
        {
            int w = grid.GetUpperBound(1) + 1;

            for (int n = grid.Length; n > 1;)
            {
                int k = rng.Next(n);
                --n;

                int dr = n / w;
                int dc = n % w;
                int sr = k / w;
                int sc = k % w;

                T temp = grid[dr, dc];
                grid[dr, dc] = grid[sr, sc];
                grid[sr, sc] = temp;
            }
        }
    } 
} 
