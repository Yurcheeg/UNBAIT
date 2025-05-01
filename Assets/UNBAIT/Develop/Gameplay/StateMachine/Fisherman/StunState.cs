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
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            _animator.SetTrigger(_shockTrigger);

            CustomCoroutine.Instance.WaitOnConditionThenExecute(
                () => stateInfo.normalizedTime >= 1f,
                () => _animator.SetBool(_stunBool, true) //TODO ADD ANIMATION FOR STUN
                );
        }

        public override void Exit() => _animator.SetBool(_stunBool, false);
    }
}
