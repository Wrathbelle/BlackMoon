using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackMoon.Core;

namespace BlackMoon.Player.MovementStates
{
    public class StopMoveState : IState
    {
        private PC pc;
        private StateMachine parentStateMachine;

        //THE TRANSITION TO IDLE
        public StopMoveState(PC character)
        {
            this.pc = character;
            this.parentStateMachine = character.movementState;
        }

        public void Enter(params object[] args)
        {
            Console.WriteLine("TRANSITION");
        }

        public void Exit()
        {
            
        }

        public void HandleInput()
        {
            throw new NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            parentStateMachine.Change("idle");
        }

        public void Draw()
        {

        }
    }
}
