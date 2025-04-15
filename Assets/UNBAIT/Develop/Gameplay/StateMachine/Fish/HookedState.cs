using Assets.UNBAIT.Develop.Gameplay.StateMachine.Fish;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract
{
    public class HookedState : BaseState<FishFSM>
    {
        public HookedState(FishFSM fsm) : base(fsm)
        {

        }
        public override void Update()
        {
            base.Update();
            if (FSM.Hook == null)
                FSM.Unhook();
        }
    }
}
