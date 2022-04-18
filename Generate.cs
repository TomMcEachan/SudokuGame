using System;

namespace SudokuGame
{
    class Generate
    {

        //Global Variables 
        private static int[,] grid;
        private static int size; 
        private static int sqr; 
        private static int  remove;

        //Getters and Setters
        public static int[,] Grid { get => grid; set => grid = value; }
        public static int Size { get => size; set => size = value; }
        public static int Sqr { get => sqr; set => sqr = value; }
        public static int Remove { get => remove; set => remove = value; }



        /// <summary>
        /// This method checks if the grid is empty. If it is empty, it passes user set parameters to GenerateGrid() method.
        /// </summary>
        /// <param name="board"></param>
        /// <returns>
        /// This returns the generated board
        /// </returns>
        public static int[,] Create(Game board)
        {
            //Object Variables
            Grid = board.Grid;
            Size = board.Size;
            Sqr = Convert.ToInt32(Math.Sqrt(board.Size));
            Remove = board.Difficulty;

            if (Grid.Length == 0 || Grid == null)
            {
                return null;
            }


           Generate gen = new Generate();
           gen.GenerateGrid();

            return Grid;
        }


        // Sudoku Generator
        public void GenerateGrid()
        {
            // Fill the diagonal of SRN x SRN matrices
            fillDiagonal();

            // Fill remaining blocks
            fillRemaining(0, Sqr);

            // Remove Randomly K digits to make game
            removeDigits();
        }

        // Fill the diagonal SRN number of SRN x SRN matrices
        void fillDiagonal()
        {

            for (int i = 0; i < Size; i = i + Sqr)

                // for diagonal box, start coordinates->i==j
                fillBox(i, i);
        }

        // Returns false if given 3 x 3 block contains num.
        bool unUsedInBox(int rowStart, int colStart, int num)
        {
            for (int i = 0; i < Sqr; i++)
                for (int j = 0; j < Sqr; j++)
                    if (Grid[rowStart + i, colStart + j] == num)
                        return false;

            return true;
        }

        // Fill a 3 x 3 matrix.
        void fillBox(int row, int col)
        {
            int num;
            for (int i = 0; i < Sqr; i++)
            {
                for (int j = 0; j < Sqr; j++)
                {
                    do
                    {
                        num = randomGenerator(size);
                    }
                    while (!unUsedInBox(row, col, num));

                    Grid[row + i, col + j] = num;
                }
            }
        }

        // Random generator
        int randomGenerator(int num)
        {
            Random rand = new Random();
            return (int)Math.Floor((double)(rand.NextDouble() * num + 1));
        }

        // Check if safe to put in cell
        bool CheckIfSafe(int i, int j, int num)
        {
            return (unUsedInRow(i, num) &&
                    unUsedInCol(j, num) &&
                    unUsedInBox(i - i % Sqr, j - j % Sqr, num));
        }

        // check in the row for existence
        bool unUsedInRow(int i, int num)
        {
            for (int j = 0; j < Size; j++)
                if (Grid[i, j] == num)
                    return false;
            return true;
        }

        // check in the row for existence
        bool unUsedInCol(int j, int num)
        {
            for (int i = 0; i < Size; i++)
                if (Grid[i, j] == num)
                    return false;
            return true;
        }

        // A recursive function to fill remaining
        // matrix
        bool fillRemaining(int i, int j)
        {
            //  System.out.println(i+" "+j);
            if (j >= Size && i < Size - 1)
            {
                i = i + 1;
                j = 0;
            }
            if (i >= Size && j >= Size)
                return true;

            if (i < Sqr)
            {
                if (j < Sqr)
                    j = Sqr;
            }
            else if (i < Size - Sqr)
            {
                if (j == (int)(i / Sqr) * Sqr)
                    j = j + Sqr;
            }
            else
            {
                if (j == Size - Sqr)
                {
                    i = i + 1;
                    j = 0;
                    if (i >= Size)
                        return true;
                }
            }

            for (int num = 1; num <= Size; num++)
            {
                if (CheckIfSafe(i, j, num))
                {
                    Grid[i, j] = num;
                    if (fillRemaining(i, j + 1))
                        return true;

                    Grid[i, j] = 0;
                }
            }
            return false;
        }

        // Remove the K no. of digits to
        // complete game
        public void removeDigits()
        {
            int count = Remove;
            while (count != 0)
            {
                int cellId = randomGenerator(Size * Size) - 1;

                // System.out.println(cellId);
                // extract coordinates i  and j
                int i = (cellId / Size);
                int j = cellId % 9;
                if (j != 0)
                    j = j - 1;

                // System.out.println(i+" "+j);
                if (Grid[i, j] != 0)
                {
                    count--;
                    Grid[i, j] = 0;
                }
            }
        }



    }
}
  

