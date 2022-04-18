using System;

namespace SudokuGame
{
    /// <summary>
    /// Generation Code Adapted from code available at https://www.geeksforgeeks.org/program-sudoku-generator/ 
    /// </summary>
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


        /// <summary>
        /// This method iterates through the different steps to generate a brand new game board
        /// </summary>
        public void GenerateGrid()
        { 
            fillDiagonal();
            fillRemaining(0, Sqr);
            removeDigits();
        }


        /// <summary>
        /// This method fills the diagonal of SQR matrices
        /// </summary>
        void fillDiagonal()
        {
            for (int i = 0; i < Size; i = i + Sqr)
            fillBox(i, i);
        }

        
        /// <summary>
        /// This method checks if the 3 x 3 box contains a num
        /// </summary>
        /// <param name="rowStart"></param>
        /// <param name="colStart"></param>
        /// <param name="num"></param>
        /// <returns>
        /// True or False
        /// </returns>
        bool unUsedInBox(int rowStart, int colStart, int num)
        {
            for (int i = 0; i < Sqr; i++)
                for (int j = 0; j < Sqr; j++)
                    if (Grid[rowStart + i, colStart + j] == num)
                        return false;

            return true;
        }

        /// <summary>
        /// Fills a 3x3 Block
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
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

        /// <summary>
        /// Creates a random number 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        int randomGenerator(int num)
        {
            Random rand = new Random();
            return (int)Math.Floor((double)(rand.NextDouble() * num + 1));
        }



        /// <summary>
        /// Checks if it is safe to put a number in a cell
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="num"></param>
        /// <returns>
        /// A safe number
        /// </returns>
        bool CheckIfSafe(int i, int j, int num)
        {
            return (unUsedInRow(i, num) &&
                    unUsedInCol(j, num) &&
                    unUsedInBox(i - i % Sqr, j - j % Sqr, num));
        }



        /// <summary>
        /// Check that a row exists
        /// </summary>
        /// <param name="i"></param>
        /// <param name="num"></param>
        bool unUsedInRow(int i, int num)
        {
            for (int j = 0; j < Size; j++)
                if (Grid[i, j] == num)
                    return false;
            return true;
        }

        /// <summary>
        /// Check that a column exists
        /// </summary>
        /// <param name="j"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        bool unUsedInCol(int j, int num)
        {
            for (int i = 0; i < Size; i++)
                if (Grid[i, j] == num)
                    return false;
            return true;
        }


        /// <summary>
        /// This is a recursive function to fill in the matrix
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
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

        /// <summary>
        /// This method removes a specified number of digits to create a playable game board
        /// </summary>
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
  

