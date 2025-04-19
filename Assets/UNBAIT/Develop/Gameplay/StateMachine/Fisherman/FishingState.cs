using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class FishingState : BaseState<FishermanFSM>
    {
        private bool _isHookThrown;
        public FishingState(FishermanFSM fsm) : base(fsm) { }

        public override void Enter()
        {
            base.Enter();

            TryThrowHook();
        }

        public override void Exit()
        {
            base.Exit();

            DestroyHook();
        }

        public override void Update()
        {
            base.Update();
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

            FSM.ThrowHook();
            _isHookThrown = true;
        }

        private void DestroyHook()
        {
            if (FSM.Hook != null)
            {
                UnityEngine.Object.Destroy(FSM.Hook);
                FSM.Fisherman.Hook = null;
                _isHookThrown = false;
            }
        }
    }
}
