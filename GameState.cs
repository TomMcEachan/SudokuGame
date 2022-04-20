using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace SudokuGame
{
    class GameState
    {

        public int ID { get; set; }
        public int[] solvedBoardArray { get; set; }
        

    }
}
