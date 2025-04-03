using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
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
        private MarkerScripts.Fisherman _fisherman;

        public Hook Hook => _fisherman.Hook;

        public override void StartMovement() => _entity.IsMoving = true;

        public override void StopMovement() => _entity.IsMoving = false;

        private void OnPositionSet() => ChangeState(new MovingState<FishermanFSM>(this));

        private void OnPositionReached() => ChangeState(new FishingState(this));

        public void ThrowHook() => CustomCoroutine.Instance.WaitOnConditionThenExecute(
            () => _fisherman.IsStunned == false,
            () => _fisherman.Hook = _hookSpawner.ThrowHook()
        );

        private void Update() => CurrentState?.Update();

        private void Awake()
        {
            _entity = GetComponent<MovingEntity>();
            _stopOnRandomPoint = GetComponent<StopOnRandomPoint>();
            _hookSpawner = GetComponent<HookSpawner>();
            _fisherman = GetComponent<MarkerScripts.Fisherman>();

            if (CurrentState == null)
                ChangeState(new IdleState<FishermanFSM>(this));

            _stopOnRandomPoint.PositionSet += OnPositionSet;
            _stopOnRandomPoint.PositionReached += OnPositionReached;
        }

        private void OnDestroy()
        {
            _stopOnRandomPoint.PositionSet -= OnPositionSet;
            _stopOnRandomPoint.PositionReached -= OnPositionReached;

            Destroy(gameObject);
        }
    }
}