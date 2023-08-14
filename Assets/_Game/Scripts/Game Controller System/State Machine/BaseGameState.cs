using System;

namespace GameSystem
{
    [Serializable]
    public abstract class BaseGameState
    {
        public event Action<GameController> OnStateEntered;
        public event Action<GameController> OnStateExited;

        protected virtual void OnStateEnter(GameController controller) { }

        public virtual void OnStateUpdate(GameController controller) { }

        public virtual void OnStateFixedUpdate(GameController controller) { }

        protected virtual void OnStateExit(GameController controller) { }

        public void StateEnter(GameController controller)
        {
            OnStateEnter(controller);
            OnStateEntered?.Invoke(controller);
        }

        public void StateExit(GameController controller)
        {
            OnStateExit(controller);
            OnStateExited?.Invoke(controller);
        }
    }
}

