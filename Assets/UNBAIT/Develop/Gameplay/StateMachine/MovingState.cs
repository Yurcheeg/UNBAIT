using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class MovingState : BaseState<FishermanFSM>
    {
        public MovingState(FishermanFSM fsm) : base(fsm) { }

        public override void Enter() => FSM.StartMovement();

        public override void Exit() => FSM.StopMovement();
    }
}

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fish
{
    public class MovingState : BaseState<FishFSM>
    {
        public MovingState(FishFSM fsm) : base(fsm) { }

        public override void Enter() => FSM.StartMovement();

        public override void Exit() => FSM.StopMovement();

        public override void Update()
        {
            if (FSM.Fish.IsHooked)
                FSM.Hook();
        }
    }
}
