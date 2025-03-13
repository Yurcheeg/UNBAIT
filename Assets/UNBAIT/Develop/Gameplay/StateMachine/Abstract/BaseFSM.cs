using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract
{
    public abstract class BaseFSM<T> : MonoBehaviour where T : BaseFSM<T>
    {
        protected BaseState<T> CurrentState;

        public abstract void StartMovement();
        public abstract void StopMovement();

        protected void ChangeState(BaseState<T> newState)
        {
            if (CurrentState != null && newState.Equals(CurrentState))
                return;

            CurrentState?.Exit();

            CurrentState = newState;
            CurrentState.Enter();
        }

        private void Update() => CurrentState?.Update();
    }
}
