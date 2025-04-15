using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class StunState : BaseState<FishermanFSM>
    {
        public StunState(FishermanFSM fsm) : base(fsm) { }
    }
}
