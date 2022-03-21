using System;


namespace SudokuGame
{
    class Generate
    {
        int size;
        int sqr;
        int[,] grid;
        int missingDigits;

        //Sudoku Generator Constructor
        public Generate(int[,] grid, int size, int missingDigits)
        {
            this.size = size;
            this.missingDigits = missingDigits;
            this.grid = grid;

            double SRNd = Math.Sqrt(size);
            sqr = (int)SRNd;

            grid = new int[size, size];
        }
        
        
        public int[,] GenerateBoard()
        {
            fill();
            fillLeftover(0, sqr);
            removeDigits();

            return grid;
        }


        void fill()
        {
            for (int i = 0; i < size; i = i + sqr)
            {
                fillBox(i, i);
            }
        }


        bool Unused(int row, int column, int num)
        {
            for (int x = 0; x < sqr; x++)
                for (int y = 0; y < sqr; y++)
                {
                    if (grid[row + x, column + y] == num)
                        return false;
                }

            return true;
        }

        void fillBox(int row, int column)
        {
            int num;
            for (int i=0; i < sqr; i++)
            {
                for (int p=0; p < sqr; p++)
                {
                    do
                    {
                        num = GenerateNum(size);
                    }
                    while (!Unused(row, column, num));

                    grid[row + i, column + p] = num;
                }
            }
        }


        int GenerateNum (int num)
        {
            Random rng = new Random();
            return (int)Math.Floor((double)(rng.NextDouble() * num + 1));
        }


        bool CheckSafe(int x, int y, int num)
        {
            return (UnusedRow(x, num) &&
                    UnusedColumn(y, num) &&
                    Unused(x - x % sqr, y - y % sqr, num));
        }


        bool UnusedRow(int x, int num)
        {
            for (int y = 0; x < size; x ++)
            {
                if (grid[x, y] == num)
                    return false;
            }

            return true;
        }

        bool UnusedColumn (int y, int num)
        {
            for (int x = 0; x < size; x++)
                if (grid[x, y] == num)
                    return false;
            return true;
        }

        bool fillLeftover (int x, int y)
        {
            if (y>=size && x<size-1)
            {
                y = y + 1;
                x = 0;
            }

            if (x >= size && y >= size)
            {
                return true;
            }

            if (x < sqr)
            {
                if (y < sqr)
                {
                    x = sqr;
                }
            }
            else if (x < size - sqr)
            {
                if (y==(int)(x/sqr) * sqr) 
                {
                    y = y + sqr;
                }
            }
            else
            {
                if (y == size - sqr)
                {
                    x = x + 1;
                    y = 0;

                    if (x>=size)
                    {
                        return true;
                    }
                }
            }

            for (int num = 1; num <= size; num++)
            {
                if (CheckSafe(x, y, num))
                {
                    grid[x, y] = num;
                    if (fillLeftover(x, y + 1))
                    {
                        return true;
                    }

                    grid[x, y] = 0;
                }
            }

            return false;
        }


        public void removeDigits()
        {
            int num = missingDigits;

            while(num != 0)
            {
                int cell = GenerateNum(size * size) - 1;

                int x = (cell / size);
                int y = cell % 9;
                if (y != 0)
                {
                    y = y - 1;
                }

                if (grid[x,y] != 0)
                {
                    num--;
                    grid[x, y] = 0;
                }
            }
        }
    }

}

  

