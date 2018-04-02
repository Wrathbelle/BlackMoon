using System;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;
using BlackMoon.Input;

namespace BlackMoon
{
    public class KeyboardHandler
	{
        KeyboardState previousState;

        private double deltaTime;
        private Dictionary<Keys, KeyEventsHandler> keys;
        private Dictionary<KeyCombo, KeyEventsHandler> keyCombos;

        public enum KeyEventType
        {
            ///<summary>Key is pressed once</summary>
            OnKeyPress,
            ///<summary>Key is down</summary>
            OnKeyDown,
            ///<summary>Key is up</summary>
            OnKeyUp,
            ///<summary>Key is down for x period of time</summary>
            OnKeyDownDelay
        }

		public KeyboardHandler ()
		{
            keys = new Dictionary<Keys, KeyEventsHandler>();
            keyCombos = new Dictionary<KeyCombo, KeyEventsHandler>();
		}

        public void AddKeyComboDownEvent(KeyCombo keyCombo, Action action, float delay = 0)
        {
            if (!keyCombos.ContainsKey(keyCombo))
            {
                KeyEventsHandler keyEvents = new KeyEventsHandler();
                keyCombos.Add(keyCombo, keyEvents);
            }

            keyCombos[keyCombo].KeyDownAction = action;
        }

        public void AddKeyEvent(Keys key, KeyEventType eventType, Action action, float delay = 0)
        {
            if (!keys.ContainsKey(key)) {
                KeyEventsHandler keyEvents = new KeyEventsHandler();
                keys.Add(key, keyEvents);
            }

            switch (eventType)
            {
                case KeyEventType.OnKeyDown:
                    keys[key].KeyDownAction = action;
                    break;
                case KeyEventType.OnKeyPress:
                    keys[key].KeyPressAction = action;
                    break;
                case KeyEventType.OnKeyUp:
                    keys[key].KeyUpAction = action;
                    break;
                case KeyEventType.OnKeyDownDelay:
                    keys[key].KeyDownDelayAction = action;
                    break;
            }
        }

		public void checkInput(double deltaTimeRef){
            //Don't bother checking inputs when game isn't focused
            KeyboardState keyboardState = Keyboard.GetState();
            this.deltaTime = deltaTimeRef;

            List<Keys> keysUsedInCombos = new List<Keys>();
            foreach(KeyCombo keyCombo in keyCombos.Keys)
            {
                bool comboAchieved = true;
                foreach(Keys key in keyCombo)
                {
                    if (!keyboardState.IsKeyDown(key))
                    {
                        comboAchieved = false;
                        break;
                    }
                }

                if (comboAchieved)
                {
                    keyCombos[keyCombo].OnKeyDown();
                    keysUsedInCombos.AddRange(keyCombo);
                }
            }

            foreach(Keys key in keys.Keys)
            {
                if (keysUsedInCombos.Contains(key))
                {
                    continue;
                }
                if(previousState.IsKeyDown(key) && keyboardState.IsKeyUp(key))
                {
                    keys[key].OnKeyPress();
                }
                else if (keyboardState.IsKeyDown(key))
                {
                    keys[key].OnKeyDown();
                }
                else if (keyboardState.IsKeyUp(key))
                {
                    keys[key].OnKeyUp();
                }
            }

            this.previousState = keyboardState;
        }
	}
}

