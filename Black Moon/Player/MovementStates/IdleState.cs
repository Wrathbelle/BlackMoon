using System;
using BlackMoon.Core;

namespace BlackMoon.Player.MovementStates
{
    class IdleState : IState
    {
        private PC pc;
        private StateMachine parentStateMachine;

        public IdleState(PC character)
        {
            this.pc = character;
            this.parentStateMachine = character.movementState;
        }

        public void Enter(params object[] args)
        {
            Console.WriteLine("IDLE");
        }

        public void Exit()
        {
            
        }

        public void Update(float deltaTime)
        {
            
        }

        public void HandleInput()
        {

        }

        public void Draw()
        {

        }
    }
}
