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
        Timer delayTimer;
        public bool delayFinished;
        private bool isDelaying;
        public bool canDelay = true;

        Timer repeatTimer;
        public bool repeated;
        private bool isRepeating;
        private bool canRepeat = true;
        int previousRepeatInterval;

        public Timing()
        {
            delayTimer = new Timer();
            repeatTimer = new Timer();
        }

        ///////////////////////////////////Delay()///////////////////////////////////////////
        public bool Delay(int delayTime)
        {
            if(!isDelaying && canDelay == true)
            {
                isDelaying = true;
                delayFinished = false;
                delayTimer.Start();
                delayTimer.Enabled = true;
                delayTimer.Interval = delayTime;
                delayTimer.Elapsed += OnTimedEventDelay;
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
                repeatTimer.Elapsed += OnTimedEventRepeat;
                repeatTimer.Enabled = true;

                if (previousRepeatInterval != repeatInterval)
                {
                    repeatTimer.Interval = repeatInterval;
                    previousRepeatInterval = repeatInterval;                  
                }
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
