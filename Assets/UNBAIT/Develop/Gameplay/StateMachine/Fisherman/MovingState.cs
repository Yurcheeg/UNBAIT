using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class MovingState<T> : BaseState<T> where T : BaseFSM<T>
    {
        public MovingState(T fsm) : base(fsm) { }

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
}
