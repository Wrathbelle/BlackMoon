using System.Collections.Generic;

namespace BlackMoon.Core
{
    public interface IState
    {
        void Update(float deltaTime);
        void HandleInput();
        void Draw();
        void Enter(params object[] args);
        void Exit();
    }

    public class EmptyState : IState
    {
        public void Update(float deltaTime) { }
        public void HandleInput() { }
        public void Draw() { }
        public void Enter(params object[] args) { }
        public void Exit() { }
    }

    public class StateMachine
    {
        public Dictionary<string, IState> stateMap = new Dictionary<string, IState>();
        public IState currentState { get; set; }

        public StateMachine()
        {
            currentState = new EmptyState();
        }

        public void Change(string id, params object[] args)
        {
            //Only change state if it's different
            if (currentState != stateMap[id])
            {
                currentState.Exit();
                IState nextState = stateMap[id];
                nextState.Enter();
                currentState = nextState;
            }
        }

        public void Update(float deltaTime)
        {
            currentState.Update(deltaTime);
        }

        public void HandleInput()
        {
            currentState.HandleInput();
        }

        public void Draw()
        {
            currentState.Draw();
        }
    }
}
