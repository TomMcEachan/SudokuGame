using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    class Moves
    {
        //Global Variables
        private static int _playerNum; //This represents the number the player placed on their turn
        private static int _rowLocation; //This represents the row location of the player placed number
        private static int _columnLocation; //This represents the column location of the player placed number
        private static int _moveNum; //This represents the move ID

        //Getters & Setters
        public int PlayerNum { get => _playerNum; set => _playerNum = value; }
        public int RowLocation { get => _rowLocation; set => _rowLocation = value; }
        public int ColumnLocation { get => _columnLocation; set => _columnLocation = value; }
        public int MoveNum { get => _moveNum; set => _moveNum = value; }



        /// <summary>
        /// Main Move Constructor
        /// </summary>
        /// <param name="name"></param>
        public Moves (int playerNumber, int RowLoc, int ColumnLoc, int moveNum)
        {
            MoveNum = moveNum;
            PlayerNum = playerNumber;
            RowLocation = RowLoc;
            ColumnLocation = ColumnLoc;
        }

        /// <summary>
        /// Empty Moves Constructor
        /// </summary>
        public Moves () { }


        //-------------------------------------------------------------------------------------------------------//

        public Stack<Moves> playerUndo(Stack<Moves> stack)
        {

            return stack;
            
        }


        public Stack<Moves> playerRedo(Stack<Moves> stack)
        {


            return stack;
        }



    }
}
