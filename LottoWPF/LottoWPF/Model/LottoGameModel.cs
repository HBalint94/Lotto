using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LottoWPF.Model
{
    public class LottoGameModel
    {

        private Timer timer;
        

        public Int32 MapSizeX { get; set; }
        public Int32 MapSizeY { get; set; }
        public Int32 GuessNumber { get; set; } // mennyi lehet a maximális tipp
        public Timer GameTimer { get { return timer; } }
        public Int32 CurrentGuessesNumbersNumber{ get; set; } // az eddigi tippek száma
        public Int32 CurrentWinnerNumbersNumber { get; set; } // az eddig kisorsolt nyerő számok száma
        public Int32[] WinnerNumberHistory { get; set; } // A kisorsolt számok tömbje
        public Int32[] GuessesNumberHistory { get; set; } // A tippelt számok tömbje
        public Int32 Score { get; set; } // találatok


        

        public event EventHandler<LottoEventArgs> GuessAction;

        public event EventHandler<RandomEventArgs> RandomWinnerNumber;


        public LottoGameModel()
        {
            //WinnerNumberHistory = new Int32[GuessNumber];
           // GuessesNumberHistory = new Int32[GuessNumber];
            CurrentWinnerNumbersNumber = 0;
            CurrentGuessesNumbersNumber = 0;
            Score = 0;
            GuessNumber = 0;
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(RandomNumberGenerator);
            timer.Interval = 1000;
        }
        
        public void RandomNumberGenerator(object sender, ElapsedEventArgs e)
        {
            if(CurrentGuessesNumbersNumber == GuessNumber)
            {
                if (WinnerNumberHistory[GuessNumber-1] == 0)
                {


                    Random random = new Random();
                    Int32 theCurrentWinnerNumber = random.Next(1, MapSizeX * MapSizeY);
                    while (WinnerNumberHistory.Contains(theCurrentWinnerNumber))
                    {
                        theCurrentWinnerNumber = random.Next(1, MapSizeX * MapSizeY);
                    }

                    WinnerNumberHistory[CurrentWinnerNumbersNumber] = theCurrentWinnerNumber;
                    CurrentWinnerNumbersNumber++;


                    Boolean b = false;
                    if (GuessesNumberHistory.Contains(theCurrentWinnerNumber))
                    {
                        b = true;
                        Score++;
                    }
                    RandomWinnerNumber(this, new RandomEventArgs(theCurrentWinnerNumber, b));
                }
            }
        }

        public void Step(Int32 x,Int32 y,Int32 ind)
        {
            if (GuessesNumberHistory[GuessNumber-1] == 0 && !GuessesNumberHistory.Contains(ind))
            {
                GuessesNumberHistory[CurrentGuessesNumbersNumber] = ind;
                CurrentGuessesNumbersNumber++;
                GuessAction(this, new LottoEventArgs(x, y));
            }
        }



    }
}
