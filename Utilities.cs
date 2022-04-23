using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
        /// A utility method to print solved/generated Sudoku board
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="size"></param>
        public static void printBoard(int[,] grid, int size)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write("| ");
                    }
                    Console.Write(grid[i, j] + " ");
                }

                if (i % 3 == 2 && i != 8)
                {
                    Console.WriteLine();
                    Console.Write("------+-------+------");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
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

    }
}
