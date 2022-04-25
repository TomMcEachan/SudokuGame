using System;
using System.Diagnostics;
using System.Timers;

namespace SudokuGame
{
    class Timer
    {
        public int SecondsTimer { get; set; }
        public TimeSpan TimeAmmount { get; set; }
        

        public Timer(TimeSpan timeSelected)
        {
            TimeAmmount = timeSelected;
        }

        public Timer () { }

        public Timer CreateTimer(int modeSelected, bool timerAdded)
        {
            if (timerAdded)
            {
                TimeSpan timeSelected;
                switch (modeSelected)
                {
                    case 1:
                        timeSelected = TimeSpan.FromMilliseconds(900000);
                        break;
                    case 2:
                        timeSelected = TimeSpan.FromMilliseconds(600000);
                        break;
                    case 3:
                        timeSelected = TimeSpan.FromMilliseconds(600000);
                        break;
                    case 4:
                        timeSelected = TimeSpan.FromMilliseconds(420000);
                        break;
                    default:
                        timeSelected = TimeSpan.FromMilliseconds(600000);
                        break;
                }
                Timer time = new Timer(timeSelected);

                return time;
            }

            return null;
        }

          
        public TimeSpan GetStartTime()
        {
            TimeSpan startTime = DateTime.Now.TimeOfDay;
            return startTime;
        }

        public TimeSpan GetEndTime()
        {
            TimeSpan endTime = DateTime.Now.TimeOfDay;
            return endTime;
        }

        public TimeSpan CalculateTimeTaken(TimeSpan startTime, TimeSpan endTime)
        {
            TimeSpan timeTaken;
            timeTaken = endTime - startTime;

            return timeTaken;
        }

        public TimeSpan CalculateTimeLeft(TimeSpan timeTaken, TimeSpan gameTime)
        {
            TimeSpan timeLeft = gameTime - timeTaken;

            return timeLeft;
        }
    }
}
