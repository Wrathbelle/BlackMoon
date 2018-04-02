using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using BlackMoon.Core;
using System.Timers;

namespace BlackMoon.Input
{
    public class KeyEventsHandler
    {
        private Timer timer;
        private bool timerStarted = false;
        private bool timerThresholdMet = false;
        public float buttonDownDelay{get;set;}

        public KeyEventsHandler()
        {
            KeyPressAction += () => { };
            KeyDownAction += () => { };
            KeyUpAction += () => { };
            KeyDownDelayAction += () => { };

            if (buttonDownDelay > 0)
            {
                timer = new Timer(buttonDownDelay);
                timer.AutoReset = false;
                timer.Elapsed += (object sender, ElapsedEventArgs e) => { timerThresholdMet = true; };
            }
        }

        public Action KeyPressAction;
        public Action KeyDownAction;
        public Action KeyUpAction;
        public Action KeyDownDelayAction;

        public void OnKeyPress()
        {
            KeyPressAction();
            if (MemoryManager.DebugMode) Console.WriteLine("KeyPress");
        }

        public void OnKeyDown()
        {
            if (!timerStarted && timer != null)
            {
                timer.Start();
                timerStarted = true;
            }

            if (timerThresholdMet)
            {
                KeyDownDelayAction();
                if (MemoryManager.DebugMode) Console.WriteLine("KeyDownDelay");
            }
            else
            {
                KeyDownAction();
                if (MemoryManager.DebugMode) Console.WriteLine("KeyDown");
            }
            
        }

        public void OnKeyUp()
        {
            KeyUpAction();
            if (timerStarted && timer != null)
            {
                timer.Stop();
                timerThresholdMet = false;
                timerStarted = false;
            }
            if (MemoryManager.DebugMode) Console.WriteLine("KeyUp");
        }

    }
}
