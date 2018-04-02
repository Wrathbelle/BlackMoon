using System;
using BlackMoon.Core;

namespace BlackMoon.Player.MovementStates
{
    public class JumpState : IState
    {
        private PC pc;
        private StateMachine parentStateMachine;

        public JumpState(PC character)
        {
            this.pc = character;
            this.parentStateMachine = character.movementState;
        }

        public void Enter(params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public void HandleInput()
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {

        }
    }
}
