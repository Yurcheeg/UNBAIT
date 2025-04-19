using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine
{
    public class IdleState<T> : BaseState<T> where T : BaseFSM<T>
    {
        public IdleState(T fsm) : base(fsm) { }
    }
}
