using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class FishingState : BaseState<FishermanFSM>
    {
        public FishingState(FishermanFSM fsm) : base(fsm) { }

        public override void Enter()
        {
            base.Enter();

            if (FSM.Hook != null)
                FSM.Hook.Destroyable.Destroyed -= OnHookDestroyed;

            ThrowHook();
        }

        public override void Exit()
        {
            base.Exit();

            if (FSM.Hook != null)
            {
                FSM.Hook.Destroyable.Destroyed -= OnHookDestroyed;
                Object.Destroy(FSM.Hook);
            }
        }

        public override void Update()
        {
            base.Update();
            if (FSM.Fisherman.IsStunned && FSM.Hook != null)
                Object.Destroy(FSM.Hook.gameObject);
        }

        private void OnHookDestroyed()
        {
            if (FSM.Hook != null)
                FSM.Hook.Destroyable.Destroyed -= OnHookDestroyed;

            ThrowHook();
        }

        private void ThrowHook()
        {
            FSM.ThrowHook();

            CustomCoroutine.Instance.WaitOnConditionThenExecute(
                () => FSM.Hook != null,
                () => FSM.Hook.Destroyable.Destroyed += OnHookDestroyed
                );
        }
    }
}
