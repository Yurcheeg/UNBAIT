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
            UnityEngine.Object.Destroy(FSM.Hook);
            Debug.Log($"{FSM.Hook} is set inactive");
            //FSM.Hook.gameObject.SetActive(false);
            //destroy hook
        }
    }
}
