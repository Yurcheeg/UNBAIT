using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class FishingState : BaseState<FishermanFSM>
    {
        //TODO: Update after deciding on hook mechanics
        public FishingState(FishermanFSM fsm) : base(fsm) { }

        public override void Enter()
        {
            base.Enter();
            FSM.ThrowHook();
        }

        public override void Exit()
        {
            base.Exit();
            Object.Destroy(FSM.Hook);
        }
    }
}
