using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.StateMachine
{
    public abstract class BaseState<TFSM> where TFSM : MonoBehaviour
    {
        protected TFSM FSM;

        protected BaseState(TFSM fsm)
        {
            FSM = fsm;
        }

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

    public class IdleState : BaseState<FishermanFSM>
    {
        public IdleState(FishermanFSM fsm) : base(fsm) { }

        public override void Enter()
        {
            base.Enter();
            FSM.StopMovement();
        }
    }

    public class MovingState : BaseState<FishermanFSM>
    {
        public MovingState(FishermanFSM fsm) : base(fsm) { }

        public override void Enter()
        {
            base.Enter();
            FSM.StartMovement();
        }

        public override void Exit()
        {
            base.Exit();
            FSM.StopMovement();
        }

        public override void Update() { }
    }

    public class FishingState : BaseState<FishermanFSM>
    {
        //TODO: Update after deciding on hook mechanics
        public FishingState(FishermanFSM fsm) :base(fsm) { }

        public override void Enter()
        {
            base.Enter();
            FSM.ThrowHook();
        }

        public override void Exit()
        {
            //destroy hook
        }
    }
}
