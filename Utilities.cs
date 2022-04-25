using System;
using System.IO;
using System.Linq;
using System.Text.Json;



namespace SudokuGame
{
    class Utilities
    { 
        /// <summary>
        /// Converts a 2D Array to A 1D array
        /// </summary>
        /// <param name="TwoDimensionalArray"></param>
        /// <returns>
        /// A converted 1D Array
        /// </returns>
        public static int[] Convert2DArrayTo1D(int[,] TwoDimensionalArray)
        {
            int[] OneDimensionalArray = { };

            var array = TwoDimensionalArray.Cast<int>();


            foreach (int i in array)
            {
                OneDimensionalArray = OneDimensionalArray.Append(i).ToArray();
            }

            return OneDimensionalArray;
        }


        /// <summary>
        /// Converts a 1D array to a 2D array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns>
        /// A 2D array
        /// </returns>
        public static T[,] Convert1DArrayTo2D<T>(T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }



        /// <summary>
        /// A utility method to print solved/generated Sudoku board
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="size"></param>
        public static void printBoard(int[,] grid, int size)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("| ");
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (grid[i,j] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(grid[i, j] + " ");
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(grid[i, j] + " ");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    
                }

                if (i % 3 == 2 && i != 8)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.Write("------+-------+------");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
        }


        /// <summary>
        /// This takes the generated board and prints it based on user selection
        /// </summary>
        /// <param name="selectedInt"></param>
        /// <param name="grid"></param>
        public static int print(int selectedInt, int[,] grid)
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
                    num = 9;
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
        /// Asks the user what their name is and returns it
        /// </summary>
        /// <returns>
        /// Returns a string of the players name
        /// </returns>
        public static string GetPlayerName()
        {
            string message = "What is your name? (This is required to save your data)\n\n";
            Console.WriteLine(message);

            string name = Console.ReadLine();

            while (name == null)
            {
                string nullMessage = "Name cannot be null.\n\n" + message;
                name = Console.ReadLine();
            }

            return name;
        }


        /// <summary>
        /// This utility method reads save files
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns>
        /// A deserialized save file
        /// </returns>
        public static T ReadSaveFile<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text);
        }



        /// <summary>
        /// This takes a sudoku game board, flattens it into a 1d array and checks that if it contains any zeros. 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns>
        ///  True or False
        /// </returns>
        public static bool ArrayContainsZero(int[,] grid)
        {
            int[] flatArray = { };
            var nums = grid.Cast<int>();

            foreach (int i in nums)
            {
                flatArray = flatArray.Append(i).ToArray();
            }


            if (flatArray.Contains(0))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method checks whether or not the player specified array is already filled or not
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="playerRow"></param>
        /// <param name="playerColumn"></param>
        /// <returns>
        /// True or False
        /// </returns>
       public static bool AlreadyFilled(int[,] grid, int playerRow, int playerColumn)
        {
            int value = grid[playerRow, playerColumn];

            if (value == 0)
            {
                return false;
            }

            return true;
        }

    }
}
