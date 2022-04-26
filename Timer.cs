using System;

namespace SudokuGame
{
    class Timer
    {
        public int SecondsTimer { get; set; }
        public TimeSpan TimeAmmount { get; set; }


        /// <summary>
        /// Timer Constructor
        /// </summary>
        /// <param name="timeSelected"></param>
        public Timer(TimeSpan timeSelected)
        {
            TimeAmmount = timeSelected;
        }

        /// <summary>
        /// Empty timer constructor
        /// </summary>
        public Timer() { }


        //--------------------------------------------------------------METHODS---------------------------------------------------------------/


        /// <summary>
        /// This method creates a timer based on user input
        /// </summary>
        /// <param name="modeSelected"></param>
        /// <param name="timerAdded"></param>
        /// <returns>
        /// A timer object with the user specified ammount of time
        /// </returns>
        public Timer CreateTimer(int modeSelected, bool timerAdded)
        {
            if (timerAdded)
            {
                TimeSpan timeSelected;
                switch (modeSelected)
                {
                    case 1:
                        timeSelected = TimeSpan.FromMilliseconds(420000);
                        break;
                    case 2:
                        timeSelected = TimeSpan.FromMilliseconds(540000);
                        break;
                    case 3:
                        timeSelected = TimeSpan.FromMilliseconds(720000);
                        break;
                    case 4:
                        timeSelected = TimeSpan.FromMilliseconds(900000);
                        break;
                    case 111:
                        timeSelected = TimeSpan.FromMilliseconds(60000);
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

        /// <summary>
        /// This method gets the start time
        /// </summary>
        /// <returns>
        /// Start time
        /// </returns>
        public TimeSpan GetStartTime()
        {
            TimeSpan startTime = DateTime.Now.TimeOfDay;
            return startTime;
        }

        /// <summary>
        /// This method gets the end time
        /// </summary>
        /// <returns>
        /// End time
        ///</returns>
        public TimeSpan GetEndTime()
        {
            TimeSpan endTime = DateTime.Now.TimeOfDay;
            return endTime;
        }

        /// <summary>
        /// This method calculates the time taken to complete a turn
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>
        /// The time taken
        /// </returns>
        public TimeSpan CalculateTimeTaken(TimeSpan startTime, TimeSpan endTime)
        {
            TimeSpan timeTaken;
            timeTaken = endTime.Subtract(startTime);

            return timeTaken;
        }


        /// <summary>
        /// This method calculates the time left to complete the game
        /// </summary>
        /// <param name="timeTaken"></param>
        /// <param name="gameTime"></param>
        /// <returns>
        /// The time left
        /// </returns>
        public TimeSpan CalculateTimeLeft(TimeSpan timeTaken, TimeSpan gameTime)
        {
            TimeSpan timeLeft = gameTime.Subtract(timeTaken);

            return timeLeft;
        }
    }
}
