using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class FishingState : BaseState<FishermanFSM>
    {
        private bool _isHookThrown;
        public FishingState(FishermanFSM fsm) : base(fsm) { }

        public override void Enter() => TryThrowHook();

        public override void Exit() => DestroyHook();

        public override void Update()
        {
            if (FSM.Fisherman.IsStunned)
                DestroyHook();

            if (_isHookThrown == false && FSM.Fisherman.IsStunned == false)
                TryThrowHook();
        }

        private void TryThrowHook()
        {
            if (_isHookThrown)
                return;

            if (FSM.Hook != null)
                return;

            CustomCoroutine.Instance.WaitThenExecute(FSM.Fisherman.ThrowDelay, FSM.ThrowHook);
            _isHookThrown = true;
        }

        private void DestroyHook()
        {
            if (FSM.Hook != null)
            {
                UnityEngine.Object.Destroy(FSM.Hook.gameObject);
                FSM.Fisherman.Hook = null;
                _isHookThrown = false;
            }
        }
    }
}
