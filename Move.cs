namespace SudokuGame
{
    class Move
    {
        public int PlayerNum { get; set; }
        public int RowLocation { get; set; }
        public int ColumnLocation { get; set; }
        public int[] GameBoard { get; set; }

        /// <summary>
        /// Main Move Constructor
        /// </summary>
        /// <param name="name"></param>
        public Move(int playerNumber, int RowLoc, int ColumnLoc, int[,] gameBoard)
        {
            PlayerNum = playerNumber;
            RowLocation = RowLoc;
            ColumnLocation = ColumnLoc;
            GameBoard = Utilities.Convert2DArrayTo1D(gameBoard);
        }

        /// <summary>
        /// Empty Moves Constructor
        /// </summary>
        public Move() { }
    }
}
