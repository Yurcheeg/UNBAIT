using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;
using Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman;
using System;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fish
{
    public class FishFSM : BaseFSM<FishFSM>
    {
        private TargetLooker _targetLooker;

        private HookableOnCollision _hookOnCollision;

        private MovingEntity _entity;

        public override void StartMovement() => _entity.IsMoving = true;

        public override void StopMovement() => _entity.IsMoving = false;

        private void OnPositionSet()
        {
            if(CurrentState != new HookedState(this))
            ChangeState(new MovingState<FishFSM>(this));
        }

        private void OnHooked(Hook hook) => ChangeState(new HookedState(this));

        private void OnUnhooked() => ChangeState(new IdleState<FishFSM>(this));

        private void OnDisable()
        {
            _targetLooker.PositionSet -= OnPositionSet;
            _hookOnCollision.Hooked -= OnHooked;
            _hookOnCollision.Unhooked -= OnUnhooked;
        }

        private void Awake()
        {
            _entity = GetComponent<MovingEntity>();
            _targetLooker = GetComponent<TargetLooker>();
            _hookOnCollision = GetComponent<HookableOnCollision>();

            if (CurrentState == null)
                ChangeState(new IdleState<FishFSM>(this));

            _targetLooker.PositionSet += OnPositionSet;
            _hookOnCollision.Hooked += OnHooked;
            _hookOnCollision.Unhooked += OnUnhooked;
        }

    }
}