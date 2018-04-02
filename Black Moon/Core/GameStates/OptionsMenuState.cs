using Microsoft.Xna.Framework.Input;
using System;

namespace BlackMoon.Core.GameState
{
    public class OptionsMenuState : IState
    {
        private Game g;
        private KeyboardHandler keyboard;
        private MouseHandler mouse;
        private float currentDeltaTime;

        public OptionsMenuState(Game g)
        {
            this.g = g;
        }

        public void Enter(params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            
        }

        public void Update(float deltaTime)
        {
            currentDeltaTime = deltaTime;
            HandleInput();
        }

        public void HandleInput()
        {
            keyboard.checkInput(currentDeltaTime);
            mouse.checkInput(currentDeltaTime);
        }

        public void Draw()
        {

        }

        
    }
}
