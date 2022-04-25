using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    class Timer
    {
       public void hello()
        {
            Console.Write("Time Left: ");
            for (int a = 10; a >= 0; a--)
            {
                Console.CursorLeft = 22;
                Console.Write("{0} minutes", a);
                System.Threading.Thread.Sleep(600000); //Ten Minutes;
            }
        }



    }
}
