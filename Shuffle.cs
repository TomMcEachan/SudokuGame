using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
  class Shuffle
  {
        public static T[,] ShuffleGrid<T>(T[,] grid, Random rng)
        {
            int p = grid.GetUpperBound(1) + 1;

            for (int i = grid.Length; i > 1;)
            {
                int z = rng.Next(i);
                --i;

                int xr = i / p;
                int xc = i % p;
                int yr = z / p;
                int yc = z % p;

                T temp = grid[xr, xc];
                grid[xr, xc] = grid[yr, yc];
                grid[yr, yc] = temp;
            }

            return grid;
        }
  } 
} 
