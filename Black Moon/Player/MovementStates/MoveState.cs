using System;
using BlackMoon.Core;
using Microsoft.Xna.Framework;
using BlackMoon.Network.net.packets;

namespace BlackMoon.Player.MovementStates
{
    public class MoveState : IState
    {
        private PC pc;
        private StateMachine parentStateMachine;
        

        public MoveState(PC character)
        {
            this.pc = character;
            this.parentStateMachine = character.movementState;
        }

        public void Enter(params object[] args)
        {
            Console.WriteLine("MOVE");
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
            float baseSpeed = pc.speed;
            float xSpeed = pc.speed;
            float ySpeed = pc.speed;

            //Slow diagonal movement
            if (pc.directionFlags.N && pc.directionFlags.E)
            {
                pc.moveDirection = SpriteMods.Direction.NE;
                ySpeed = baseSpeed * .5f;
            }
            else if (pc.directionFlags.N && pc.directionFlags.W)
            {
                pc.moveDirection = SpriteMods.Direction.NW;
                ySpeed = baseSpeed * .5f;
            }
            else if (pc.directionFlags.S && pc.directionFlags.E)
            {
                pc.moveDirection = SpriteMods.Direction.SE;
                ySpeed = baseSpeed * .5f;
            }
            else if (pc.directionFlags.S && pc.directionFlags.W)
            {
                pc.moveDirection = SpriteMods.Direction.SW;
                ySpeed = baseSpeed * .5f;
            }
            else if (pc.directionFlags.N)
            {
                pc.moveDirection = SpriteMods.Direction.N;
            }
            else if (pc.directionFlags.S)
            {
                pc.moveDirection = SpriteMods.Direction.S;
            }
            else if (pc.directionFlags.E)
            {
                pc.moveDirection = SpriteMods.Direction.E;
            }
            else if (pc.directionFlags.W)
            {
                pc.moveDirection = SpriteMods.Direction.W;
            }
            else if(!pc.directionFlags.CLICK)
            {
                this.parentStateMachine.Change("stopMoveState");
            }

            //Do the movement
            if (pc.directionFlags.N)
            {
                pc.camera.Move(new Vector3(0,1,0));
                pc.position += new Vector2(0, -ySpeed);
            }
            if (pc.directionFlags.E)
            {
                pc.camera.Move(new Vector3(0,0,0), -3);
                pc.position += new Vector2(xSpeed, 0);
            }
            if (pc.directionFlags.S)
            {
                pc.camera.Move(new Vector3(0,-1,0));
                pc.position += new Vector2(0, ySpeed);
            }
            if (pc.directionFlags.W)
            {
                pc.camera.Move(new Vector3(0,0,0), 3);
                pc.position += new Vector2(-xSpeed, 0);
            }


            /*if (pc.directionFlags.CLICK)
            {
                //pc.camera.Move(pc.clickMoveVector);
                pc.position += pc.clickMoveVector;

                if (Vector2.Distance(pc.destLocation, pc.position) <= 2f) //5f
                {
                    this.parentStateMachine.Change("stopMoveState");
                }
            }
            else
            {
                pc.faceDirection = pc.moveDirection;
            }*/

            MovementPackets packets = new MovementPackets();
            packets.sendMovement(SessionManager.getSession().getConnection(), (int)pc.position.X, (int)pc.position.Y);

        }

        public void Draw()
        {

        }
    }
}
