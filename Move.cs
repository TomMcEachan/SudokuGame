using System.Collections.Generic;

namespace SudokuGame
{
    class Move
    {
        public int PlayerNum { get; set; }
        public int RowLocation { get; set; }
        public int ColumnLocation { get; set; }

        /// <summary>
        /// Main Move Constructor
        /// </summary>
        /// <param name="name"></param>
        public Move (int playerNumber, int RowLoc, int ColumnLoc)
        { 
            PlayerNum = playerNumber;
            RowLocation = RowLoc;
            ColumnLocation = ColumnLoc;
        }

        /// <summary>
        /// Empty Moves Constructor
        /// </summary>
        public Move () { }


        //-------------------------------------------------------------------------------------------------------//

        public Stack<Move> playerUndo(Stack<Move> stack)
        {

            return stack;
            
        }


        public Stack<Move> playerRedo(Stack<Move> stack)
        {


            return stack;
        }



    }
}
