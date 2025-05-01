using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class StunState : BaseState<FishermanFSM>
    {
        private Animator _animator;
        private readonly string _shockTrigger = "IsShocked";
        private readonly string _stunBool = "IsStunned";

        public StunState(FishermanFSM fsm) : base(fsm) { }

        public override void Enter()
        {
            _animator = FSM.Fisherman.Animator;
            _animator.SetTrigger(_shockTrigger);

            CustomCoroutine.Instance.WaitOnConditionThenExecute(
                () =>
                {
                    AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

                    return stateInfo.IsName("Fisherman-Shock")
                    && stateInfo.normalizedTime >= 1f;
                },
                () => _animator.SetBool(_stunBool, true)
                );
        }

        public override void Exit() => _animator.SetBool(_stunBool, false);
    }
}
