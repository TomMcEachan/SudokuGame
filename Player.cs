using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    class Player
    {
        /// <summary>
        /// This method finds out which column the player wants to place a number on
        /// </summary>
        /// <returns>
        /// This returns the players chosen column as an integer
        /// </returns>
        public int playerInputColumn()
        {

            bool colNumValid = false;

            while (!colNumValid)
            {
                string playerPlacesColumn = "Which column would you like to place the number? (Select a number between 1 & 9) \n\n ";
                Console.WriteLine(playerPlacesColumn);
                string playerColumn = Console.ReadLine();
                int playerColumnInt = Int32.Parse(playerColumn);

                if (Enumerable.Range(1, 9).Contains(playerColumnInt))
                {
                    colNumValid = true;
                    playerColumnInt--; //Removes one from the value to ensure it works with 2d matrix
                    return playerColumnInt;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That number is not valid. Please type a number between 1 and 9\n\n", Console.ForegroundColor);
                }
                


            }

            Console.ForegroundColor = ConsoleColor.Green;

            return 0;
        }


        /// <summary>
        /// This method finds out which row the player wants to place a number on
        /// </summary>
        /// <returns>
        /// This returns the players row as an Integer
        /// </returns>
        public int playerInputRow()
        {
            bool rowNumValid = false;

            while (!rowNumValid)
            {
                string playerPlacesRow = "Which row would you like to place the number?  (Select a number between 1 & 9)\n\n ";
                Console.WriteLine(playerPlacesRow);
                string playerRow = Console.ReadLine();
                int playerRowInt = Int32.Parse(playerRow);

                if (Enumerable.Range(1, 9).Contains(playerRowInt))
                {
                    rowNumValid = true;
                    playerRowInt--; //Removes one from the value to ensure it works with the 2d matrix
                    return playerRowInt;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That number is not valid. Please type a number between 1 and 9\n\n", Console.ForegroundColor);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;

            return 0;
        }

        /// <summary>
        /// This method asks the player which number they would like to play at their selected point
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>
        /// The players selected number
        /// </returns>
        public int playerInputNumber(int row, int column)
        {
            row++;
            column++;
            string playerPlacesNumber = "Which number would you like to play at [" + row + "," + column + "] ? (Select a number between 1 & 9) \n\n";
            bool playerNumValid = false;

            while (!playerNumValid)
            {
                Console.WriteLine(playerPlacesNumber);
                string playerChoice = Console.ReadLine();
                int playerChoiceInt = Int32.Parse(playerChoice);
                Console.WriteLine();

                if (Enumerable.Range(1, 9).Contains(playerChoiceInt))
                {
                    playerNumValid = true;
                    return playerChoiceInt;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That number is not valid. Please type a number between 1 and 9\n\n", Console.ForegroundColor);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;

            return 0;
        }


        /// <summary>
        /// This takes a sudoku gameboard, flattens it into a 1d array and checks that if it contains any zeros. 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns>
        ///  True or False
        /// </returns>
        static bool ArrayContainsZero(int[,] grid)
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
        /// This method takes the generated board and allows the player to play the Sudoku game, turn by turn
        /// </summary>
        /// <param name="generatedBoard"></param>
        /// <returns>
        /// The players completed Sudoku board
        /// </returns>
        public int[,] playerInput(int[,] generatedBoard, Print p)
        {
            bool containsZero = true;
            int turn = 1;

            //Player Makes their choice until board is complete
            while (containsZero)
            {
                string TurnNum = $"Turn: {turn} \n\n";
                Console.WriteLine(TurnNum);

                int playerRow = playerInputRow();
                int playerColumn = playerInputColumn();
                int playerNum = playerInputNumber(playerRow, playerColumn);
                generatedBoard[playerRow, playerColumn] = playerNum;
                containsZero = ArrayContainsZero(generatedBoard);
                turn++;

                p.printBoard(generatedBoard, 9);
            }

            return generatedBoard;
        }
    }
}
