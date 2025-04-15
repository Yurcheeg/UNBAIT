﻿using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners;
using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class FishermanFSM : BaseFSM<FishermanFSM>
    {
        private MovingEntity _entity;

        private StopOnRandomPoint _stopOnRandomPoint;

        private HookSpawner _hookSpawner;

        public MarkerScripts.Fisherman Fisherman { get; private set; }

        public Hook Hook => Fisherman.Hook;

        public override void StartMovement() => _entity.IsMoving = true;

        public override void StopMovement() => _entity.IsMoving = false;

        public void ThrowHook() => CustomCoroutine.Instance.WaitOnConditionThenExecute(
            () => Fisherman.IsStunned == false,
            () => Fisherman.Hook = _hookSpawner.ThrowHook()
        );

        private void OnPositionSet() => ChangeState(new MovingState<FishermanFSM>(this));

        private void OnPositionReached() => ChangeState(new FishingState(this));

        private void Update() => CurrentState?.Update();

        private void Awake()
        {
            _entity = GetComponent<MovingEntity>();
            _stopOnRandomPoint = GetComponent<StopOnRandomPoint>();
            _hookSpawner = GetComponent<HookSpawner>();
            Fisherman = GetComponent<MarkerScripts.Fisherman>();

            Fisherman.Stunned += OnStunned;
            Fisherman.Unstunned += OnUnstunned;

            _stopOnRandomPoint.PositionSet += OnPositionSet;
            _stopOnRandomPoint.PositionReached += OnPositionReached;

            ChangeState(new IdleState<FishermanFSM>(this));

        }

        private void OnStunned() => ChangeState(new StunState(this));

        private void OnUnstunned() => ChangeState(new FishingState(this));

        private void OnDestroy()
        {
            Fisherman.Stunned -= OnStunned;
            Fisherman.Unstunned -= OnUnstunned;

            _stopOnRandomPoint.PositionSet -= OnPositionSet;
            _stopOnRandomPoint.PositionReached -= OnPositionReached;
        }
    }
}