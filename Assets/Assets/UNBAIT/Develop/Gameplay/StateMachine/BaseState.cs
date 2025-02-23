using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.StateMachine
{
    public abstract class BaseState
    {
        public virtual void Enter()
        {
#if UNITY_EDITOR
            Debug.Log($"Entering {this.GetType().Name}");
#endif
        }
        public virtual void Exit()
        {
#if UNITY_EDITOR
            Debug.Log($"Exiting {this.GetType().Name}");
#endif
        }
        public virtual void Update()
        {
        }
    }

    public class IdleState : BaseState
    {
        private FishermanFSM fsm;

        public IdleState(FishermanFSM fsm)
        {
            this.fsm = fsm;
        }
    }

    public class MovingState : BaseState
    {
        private FishermanFSM fsm;

        public MovingState(FishermanFSM fsm)
        {
            this.fsm = fsm;
        }

        public override void Enter()
        {
            fsm.StartMovement();
        }

        public override void Exit()
        {
            fsm.StopMovement();
        }

        public override void Update()
        {

        }
    }

    public class FishingState : BaseState
    {
        private FishermanFSM fsm;

        public FishingState(FishermanFSM fsm)
        {
            this.fsm = fsm;
        }

        public override void Enter()
        {
            fsm.ThrowHook();
        }

        public override void Exit()
        {
            //destroy hook
        }
    }
}
