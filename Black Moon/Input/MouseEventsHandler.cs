using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackMoon.Core;

namespace BlackMoon.Input
{
    public class MouseEventsHandler { 

        public MouseEventsHandler()
        {
            MouseMoveAction += () => { };
            MouseHoverAction += () => { };
            MouseScrollUpAction += () => { };
            MouseScrollDownAction += () => { };
        }

        public Action MouseMoveAction;
        public Action MouseHoverAction;
        public Action MouseScrollUpAction;
        public Action MouseScrollDownAction;

        public void OnMouseMove()
        {
            MouseMoveAction();
            if (MemoryManager.DebugMode)  Console.WriteLine("MouseMove");
        }

        public void OnMouseHover()
        {
            MouseHoverAction();
            if(MemoryManager.DebugMode) Console.WriteLine("MouseHover");
        }

        public void OnMouseScrollUp()
        {
            MouseScrollUpAction();
            if (MemoryManager.DebugMode)  Console.WriteLine("MouseScrollUp");
        }

        public void OnMouseScrollDown()
        {
            MouseScrollDownAction();
            if (MemoryManager.DebugMode)  Console.WriteLine("MouseScrollDown");
        }

    }
}

