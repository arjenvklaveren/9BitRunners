using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GXPEngine
{
    class Timing
    {
        private bool delayFinished;
        private bool isDelaying;
        public bool canDelay = true;

        public bool repeated;
        private bool isRepeating;
        private bool canRepeat = true;

        public Timing()
        {

        }

        ///////////////////////////////////Delay()///////////////////////////////////////////
        public bool Delay(int delayTime)
        {
            if(!isDelaying && canDelay == true)
            {
                isDelaying = true;
                delayFinished = false;
                Timer timer = new Timer(delayTime);
                timer.Elapsed += OnTimedEventDelay;
                timer.Enabled = true;
            }
            return delayFinished;
        }

        private void OnTimedEventDelay(Object source, ElapsedEventArgs e)
        {
            if(isDelaying)
            {
                DelayEvent();
            }    
        }

        private void DelayEvent()
        {
            isDelaying = false;
            delayFinished = true;
            canDelay = false;
        }

        ///////////////////////////////////Repeat()///////////////////////////////////////////
        public bool Repeat(int repeatInterval, bool repeatWithDelay)
        {
            if (!isRepeating && canRepeat)
            {
                if(!repeatWithDelay)
                {
                    RepeatEvent();
                }
                isRepeating = true;
                Timer timer = new Timer(repeatInterval);
                timer.Elapsed += OnTimedEventRepeat;
                timer.Enabled = true;
            }
            return repeated;
        }

        private void OnTimedEventRepeat(Object source, ElapsedEventArgs e)
        {
            if (isRepeating)
            {
                RepeatEvent();
            }
        }

        private void RepeatEvent()
        {
            repeated = true;           
        }
    }  
}
