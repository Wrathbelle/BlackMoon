using Microsoft.Xna.Framework.Input;
using BlackMoon.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace BlackMoon
{
    public class MouseHandler
	{
        private Dictionary<ButtonType, ButtonEventsHandler> buttons;
        private MouseEventsHandler mouse;
        private MouseState prevMouseState;

        public Point currentLocation { get; set; }

        //TODO SHIFT CLICKS 

        public enum ButtonEventType
        {
            ///<summary>Button is down</summary>
            OnButtonDown,
            ///<summary>Button is down for x period of time</summary>
            OnButtonDownDelay,
            ///<summary>Button is dragging</summary>
            OnButtonDragging,
            ///<summary>Button is clicked</summary>
            OnButtonClick,
            ///<summary>Button is up</summary>
            OnButtonUp

        }

        public enum ButtonType
        {
            LeftButton,
            RightButton,
            MiddleButton,
            XButton1,
            XButton2
        }

        public enum MouseEventType
        {
            OnMouseMove,
            OnMouseHover,
            OnMouseScrollUp,
            OnMouseScrollDown
        }

        public MouseHandler ()
		{
            mouse = new MouseEventsHandler();
            buttons = new Dictionary<ButtonType, ButtonEventsHandler>();
		}

        public void AddButtonEvent(ButtonType button, ButtonEventType eventType, Action action, float delay = 0)
        {
            if (!buttons.ContainsKey(button))
            {
                ButtonEventsHandler buttonEvents = new ButtonEventsHandler();
                buttons.Add(button, buttonEvents);
            }

            switch (eventType)
            {
                case ButtonEventType.OnButtonDown:
                    buttons[button].ButtonDownAction = action;
                    break;
                case ButtonEventType.OnButtonDragging:
                    buttons[button].ButtonDraggingAction = action;
                    break;
                case ButtonEventType.OnButtonClick:
                    buttons[button].ButtonClickAction = action;
                    break;
                case ButtonEventType.OnButtonUp:
                    buttons[button].ButtonUpAction = action;
                    break;
                case ButtonEventType.OnButtonDownDelay:
                    buttons[button].ButtonDownDelayAction = action;
                    break;
            }
        }

        public void AddMouseEvent(MouseEventType eventType, Action action)
        {
            switch (eventType)
            {
                case MouseEventType.OnMouseMove:
                    mouse.MouseMoveAction = action;
                    break;
                case MouseEventType.OnMouseHover:
                    mouse.MouseHoverAction = action;
                    break;
                case MouseEventType.OnMouseScrollUp:
                    mouse.MouseScrollUpAction = action;
                    break;
                case MouseEventType.OnMouseScrollDown:
                    mouse.MouseScrollDownAction = action;
                    break;
            }
        }

        private bool meetsDragDistance(MouseState mouseState)
        {
            return Vector2.Distance(mouseState.Position.ToVector2(), prevMouseState.Position.ToVector2()) > 5;
        }

        public void checkInput(double deltaTime)
        {
            MouseState mouseState = Mouse.GetState();

            //Left Button
            if (buttons.ContainsKey(ButtonType.LeftButton)) {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (meetsDragDistance(mouseState))
                    {
                        buttons[ButtonType.LeftButton].OnButtonDragging();
                    }
                    else
                    {
                        buttons[ButtonType.LeftButton].OnButtonDown();
                    }
                }
                else if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    buttons[ButtonType.LeftButton].OnButtonClick();
                }
                else if(mouseState.LeftButton == ButtonState.Released)
                {
                    buttons[ButtonType.LeftButton].OnButtonUp();
                }
            }

            //Right Button
            if (buttons.ContainsKey(ButtonType.RightButton))
            {
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    if (meetsDragDistance(mouseState))
                    {
                        buttons[ButtonType.RightButton].OnButtonDragging();
                    }
                    else
                    {
                        buttons[ButtonType.RightButton].OnButtonDown();
                    }
                }
                else if (mouseState.RightButton == ButtonState.Released && prevMouseState.RightButton == ButtonState.Pressed)
                {
                    buttons[ButtonType.RightButton].OnButtonClick();
                }
                else if (mouseState.RightButton == ButtonState.Released)
                {
                    buttons[ButtonType.RightButton].OnButtonUp();
                }
            }

            //Middle Button
            if (buttons.ContainsKey(ButtonType.MiddleButton))
            {
                if (mouseState.MiddleButton == ButtonState.Pressed)
                {
                    if (meetsDragDistance(mouseState))
                    {
                        buttons[ButtonType.MiddleButton].OnButtonDragging();
                    }
                    else
                    {
                        buttons[ButtonType.MiddleButton].OnButtonDown();
                    }
                }
                else if (mouseState.MiddleButton == ButtonState.Released && prevMouseState.MiddleButton == ButtonState.Pressed)
                {
                    buttons[ButtonType.MiddleButton].OnButtonClick();
                }
                else if (mouseState.MiddleButton == ButtonState.Released)
                {
                    buttons[ButtonType.MiddleButton].OnButtonUp();
                }
            }

            //XButton1
            if (buttons.ContainsKey(ButtonType.XButton1))
            {
                if (mouseState.XButton1 == ButtonState.Pressed)
                {
                    if (meetsDragDistance(mouseState))
                    {
                        buttons[ButtonType.XButton1].OnButtonDragging();
                    }
                    else
                    {
                        buttons[ButtonType.XButton1].OnButtonDown();
                    }
                }
                else if (mouseState.XButton1 == ButtonState.Released && prevMouseState.XButton1 == ButtonState.Pressed)
                {
                    buttons[ButtonType.XButton1].OnButtonClick();
                }
                else if (mouseState.XButton1 == ButtonState.Released)
                {
                    buttons[ButtonType.XButton1].OnButtonUp();
                }
            }

            //XButton2
            if (buttons.ContainsKey(ButtonType.XButton2))
            {
                if (mouseState.XButton2 == ButtonState.Pressed)
                {
                    if (meetsDragDistance(mouseState))
                    {
                        buttons[ButtonType.XButton2].OnButtonDragging();
                    }
                    else
                    {
                        buttons[ButtonType.XButton2].OnButtonDown();
                    }
                }
                else if (mouseState.XButton2 == ButtonState.Released && prevMouseState.XButton2 == ButtonState.Pressed)
                {
                    buttons[ButtonType.XButton2].OnButtonClick();
                }
                else if (mouseState.XButton2 == ButtonState.Released)
                {
                    buttons[ButtonType.XButton2].OnButtonUp();
                }
            }

            //Mouse move
            if (mouseState.LeftButton == ButtonState.Released
                && mouseState.MiddleButton == ButtonState.Released
                && mouseState.RightButton == ButtonState.Released
                && mouseState.XButton1 == ButtonState.Released
                && mouseState.XButton2 == ButtonState.Released
                && mouseState.Position != prevMouseState.Position
                )
            {
                mouse.OnMouseMove();
            }

            if(mouseState.Position == prevMouseState.Position)
            {
                mouse.OnMouseHover();
            }

			//Scroll Wheel
			if (prevMouseState.ScrollWheelValue != mouseState.ScrollWheelValue) {
				if(prevMouseState.ScrollWheelValue < mouseState.ScrollWheelValue)
                {
                    mouse.OnMouseScrollUp();
                }
                else
                {
                    mouse.OnMouseScrollDown();
                }
			}

            currentLocation = mouseState.Position;
			prevMouseState = mouseState;
		}
	}
}

