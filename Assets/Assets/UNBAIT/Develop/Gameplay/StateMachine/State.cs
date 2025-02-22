using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.StateMachine
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }

    public class IdleState : State
    {
        private FishermanFSM fsm;

        public IdleState(FishermanFSM fsm)
        {
            this.fsm = fsm;
        }

        public override void Enter()
        {
            Debug.Log($"Entering {this.GetType().Name}");
        }

        public override void Exit()
        {
            Debug.Log($"Exiting {ToString()}");
        }

        public override void Update()
        {

        }
    }

    public class MovingState : State
    {
        private FishermanFSM fsm;

        public MovingState(FishermanFSM fsm)
        {
            this.fsm = fsm;
        }

        public override void Enter()
        {
            Debug.Log($"Entering {ToString()}");
            fsm.StartMovement();
        }

        public override void Exit()
        {
            Debug.Log($"Exiting {ToString()}");
            fsm.StopMovement();
        }

        public override void Update()
        {

        }
    }

    public class FishingState : State
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
            Debug.Log($"Exiting {ToString()}");
        }

        public override void Update()
        {
            
        }
    }
}
