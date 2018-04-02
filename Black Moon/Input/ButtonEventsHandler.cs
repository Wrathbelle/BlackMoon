using BlackMoon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace BlackMoon.Input
{
    public class ButtonEventsHandler
    {
        private Timer timer;
        private bool timerStarted = false;
        private bool timerThresholdMet = false;

        //DRAG START & DRAG END
        public Action ButtonUpAction;
        public Action ButtonDownAction;
        public Action ButtonDownDelayAction;
        public Action ButtonClickAction;
        public Action ButtonDraggingAction;

        public ButtonEventsHandler()
        {
            ButtonUpAction += () => { };
            ButtonClickAction += () => { };
            ButtonDownAction += () => { };
            ButtonDraggingAction += () => { };
        }

        

        public void OnButtonUp()
        {
            ButtonUpAction();
            if(timerStarted && timer != null)
            {
                timer.Stop();
                timerThresholdMet = false;
                timerStarted = false;
            }
        }

        public void OnButtonDragging()
        {
            ButtonDraggingAction();
        }

        public void OnButtonDown()
        {
            if (!timerStarted && timer != null)
            {
                timer.Start();
                timerStarted = true;
            }

            if (timerThresholdMet)
            {
                ButtonDownDelayAction();
            }
            else
            {
                ButtonDownAction();
            }
        }

        public void OnButtonClick()
        {
            ButtonClickAction();
        }
    }
}

