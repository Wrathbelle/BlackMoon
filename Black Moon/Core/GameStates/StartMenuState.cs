using BlackMoon.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BlackMoon.Core.GameState
{
    public class StartMenuState : IState
    {
        private Game g;
        private KeyboardHandler keyboard;
        private MouseHandler mouse;
        private float currentDeltaTime;

        public StartMenuState(Game g)
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
