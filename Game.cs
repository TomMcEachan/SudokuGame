﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Figgle;

namespace SudokuGame
{
    class Game
    {
        //Global Variables
        private static int[,] _grid; //This is the Sudoku Board (e.g, [9,9])
        private static int _size; //This the size of each row/column (e.g, 9) 
        private static int _difficulty; //This is the difficulty of the users game and removes the specified number of digits


        //Getters & Setters
        public int[,] Grid { get => _grid; set => _grid = value; }
        public int Size { get => _size; set => _size = value; }
        public int Difficulty { get => _difficulty; set => _difficulty = value; }


        /// <summary>
        /// This constructor represents a Sudoku Board
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="size"></param>
        /// <param name="difficulty"></param>
        public Game(int[,] grid, int size, int difficulty)
        {
            Grid = grid;
            Size = size;
            Difficulty = difficulty;
        }

        public Game() { }

        /// <summary>
        /// This represents the main steps of the Sudoku Game
        /// </summary>
        /// <param name="p"></param>
        /// <param name="player"></param>
        /// <param name="solve"></param>
        /// <returns>
        /// Whether or not the player wants to have another game
        /// </returns>
        public bool gameplay(Print p, Player player, Solve solve)
        {
            //Variables
            int printNum;
            int sqr;
            bool boardCorrect = false;
            bool goAgain = false;

            //Prints the intro
            int selected = printIntro();

            //Generates a partially filled Game board
            int[,] generatedBoard = getGrid(selected);

            //Prints the empty board
            printNum = p.print(selected, generatedBoard);
            sqr = Convert.ToInt32(Math.Sqrt(printNum));

            //Player Makes their choice until board is complete
            int[,] playerGrid = player.playerInput(generatedBoard, p);

            //Solves the generated board and stores it in memory
            int[,] solvedBoard = solve.SolveGrid(generatedBoard, printNum, sqr);

            //Compares the SolvedBoard against the PlayerBoard
            boardCorrect = CompareSudoku(playerGrid, solvedBoard);

            if (!boardCorrect)
            {
                string wrongMessage = "Your game board is incorrect. Would you like to try again?";
                Console.WriteLine(wrongMessage);
            }
            else
            {
                string rightMessage = "Congratulations! Your board is correct.";
                Console.WriteLine((rightMessage));
                goAgain = GoAgain();
            }

            if (goAgain)
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// This method gets the user specified grid 
        /// </summary>
        /// <param name="selectedMode"></param>
        /// <returns>
        /// This returns the sudoku grid based on the parameters set by the player
        /// </returns>
        static int[,] getGrid(int selectedMode)
        {

            int[,] grid;


            switch (selectedMode)
            {
                case 1:
                    grid = new int[9, 9];
                    Console.WriteLine("\n\nGenerating an easy Sudoku puzzle....\n\n");
                    Game easyBoard = new Game(grid, 9, 34);
                    grid = Generate.Create(easyBoard);
                    break;
                case 2:
                    grid = new int[9, 9];
                    Console.WriteLine("\n\nGenerating a medium Sudoku puzzle....\n\n");
                    Game mediumBoard = new Game(grid, 9, 45);
                    grid = Generate.Create(mediumBoard);
                    break;
                case 3:
                    grid = new int[9, 9];
                    Console.WriteLine("\n\nGenerating a hard Sudoku puzzle....\n\n");
                    Game hardBoard = new Game(grid, 9, 57);
                    grid = Generate.Create(hardBoard);
                    break;
                case 4:
                    grid = new int[9, 9];
                    Console.WriteLine("\n\nGenerating an extreme Sudoku puzzle....\n\n");
                    Game extremeBoard = new Game(grid, 9, 59);
                    grid = Generate.Create(extremeBoard);
                    break;
                case 666:
                    grid = new int[9, 9];
                    Console.WriteLine("\n\nGenerating an test Sudoku puzzle....\n\n");
                    Game testBoard = new Game(grid, 9, 1);
                    grid = Generate.Create(testBoard);
                    break;
                default:
                    grid = new int[9, 9];
                    Console.WriteLine("\n\nGenerating an test Sudoku puzzle....\n\n");
                    Game defaultBoard = new Game(grid, 9, 1);
                    grid = Generate.Create(defaultBoard);
                    break;
            }

            return grid;
        }

        /// <summary>
        /// Lets the player select the difficulty of the game they want to play.
        /// </summary>
        /// <returns>
        /// selectedModeInt
        /// </returns>
        static int printIntro()
        {
            Console.WriteLine(FiggleFonts.Doom.Render("Sudoku Generator!"));
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Welcome! This program generates unique, solvable Sudoku puzzles.");
            Console.WriteLine("To generate a puzzle, please select a difficulty level from the following options by typing 1, 2, 3, or 4.\n\n" +
                                " 1 - Easy (Produces a simple 9x9 Sudoku puzzle to solve)\n" +
                                " 2 - Medium (Produces a more difficult 9x9 Sudoku puzzle to solve)\n" +
                                " 3 - Hard (Produces a really difficult 9x9 Sudoku puzzle to solve)\n" +
                                " 4 - Extreme (Produces ridiculously difficult 9x9 Sudoku puzzle to solve\n\n");

            string selectedModeString = Console.ReadLine();
            int selectedModeInt;

            while (!int.TryParse(selectedModeString, out selectedModeInt))
            {
                Console.WriteLine("Not a valid selection - please try enter a number between 1 & 4");
                selectedModeString = Console.ReadLine();
            }

            return selectedModeInt;

        }

        /// <summary>
        /// This method compares both the player completed grid and the solved grid and returns false if they are not the same
        /// </summary>
        /// <param name="playerGrid"></param>
        /// <param name="solvedGrid"></param>
        /// <returns>
        /// True or False
        /// </returns>
        static bool CompareSudoku(int[,] playerGrid, int[,] solvedGrid)
        {
            if (playerGrid == solvedGrid)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// This method asks the player if they would like to go again. 
        /// </summary>
        /// <returns>
        /// True or False
        /// </returns>
        static bool GoAgain()
        {
            string message = "Would you like to go again? (Y/N)";
            bool answered = false;

            while (!answered)
            {
                Console.WriteLine(message);
                string answer = Console.ReadLine();

                if (answer == "Y" || answer == "y")
                {
                    return true;
                }

                if (answer == "N" || answer == "n")
                {
                    return false;
                }

                if (answer != "Y" && answer != "y" && answer != "N" && answer != "n")
                {
                    Console.WriteLine("That is not a valid answer. Please type either Y or N.");
                    answered = false;
                }
            }
                  
            return false;
        }
    }
}