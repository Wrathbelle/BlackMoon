using Microsoft.Xna.Framework;
using BlackMoon.Sprite;
using Microsoft.Xna.Framework.Graphics;
using BlackMoon.Core;
using BlackMoon.Player.MovementStates;

namespace BlackMoon.Player
{
    //Player character
    public class PC : AnimatedSprite
	{
        public Camera camera;
        public DirectionFlags directionFlags;
        public SpriteMods.Direction moveDirection;
        public Vector2 destLocation { get; set; }
        public Vector2 clickMoveVector { get; set; }

        //turn into buffs dictionary? :) TODO
        private bool running;
        public bool isRunning
        {
            get
            {
                return running;
            }
            set
            {
                running = value;
                speedMod = (isRunning ? 1.5f : 1);
            }
        }
            
        public StateMachine movementState { get; set; }
        
        public PC()
        {
            baseSpeed = 4;
            isRunning = false;
            frameTime = .4f;
            columns = 6;
            rows = 8;
            currentFrame = 0;
            totalFrames = 6;
            frameTime = .25f;
            setFaceDirectionFrames(3, 7, 2, 5, 0, 4, 1, 6, 0);

            movementState = new StateMachine();
            movementState.stateMap.Add("move", new MoveState(this));
            movementState.stateMap.Add("jump", new JumpState(this));
            movementState.stateMap.Add("fall", new FallState(this));
            movementState.stateMap.Add("idle", new IdleState(this));
            movementState.stateMap.Add("stopMoveState", new StopMoveState(this));

            movementState.Change("idle");
        }

        public void Update(float deltaTime)
        {
            movementState.Update(deltaTime);
            base.Update(deltaTime);
        }

        public struct DirectionFlags
        {
            public bool N;
            public bool E;
            public bool W;
            public bool S;
            public bool CLICK;
        }
    }
}

