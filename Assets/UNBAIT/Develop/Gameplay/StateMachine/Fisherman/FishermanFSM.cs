using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition;
using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement;
using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners;
using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman
{
    public class FishermanFSM : BaseFSM<FishermanFSM>
    {
        private MovingEntity _entity;

        private HookSpawner _hookSpawner;

        private MovementController _movementController;

        public Entities.Fisherman Fisherman { get; private set; }

        public Hook Hook => Fisherman.Hook;

        public override void StartMovement() => _entity.IsMoving = true;

        public override void StopMovement() => _entity.IsMoving = false;

        public void ThrowHook() => CustomCoroutine.Instance.WaitOnConditionThenExecute(
            () => Fisherman != null && Fisherman.IsStunned == false,
            () =>
            {
                if (Fisherman != null)//checked again because of async callback
                {
                    Fisherman.Hook = _hookSpawner.ThrowHook();
                    Hook.Caught += OnCaught;
                }
            });

        private void OnCaught(Entity entity)
        {
            Hook.Caught -= OnCaught;
            GetComponent<FishCaughtCondition>().OnCaught(entity);//HACK it shouldn't call the random OnCaught method
            if (entity is not Entities.Fish)
                CustomCoroutine.Instance.WaitThenExecute(Fisherman.ThrowDelay, ThrowHook);
        }

        private void OnPositionSet() => ChangeState(new MovingState(this));

        private void OnPositionReached() => ChangeState(new FishingState(this));


        private void Awake()
        {
            _entity = GetComponent<MovingEntity>();
            _movementController = GetComponent<MovementController>();
            _hookSpawner = GetComponent<HookSpawner>();
            Fisherman = GetComponent<Entities.Fisherman>();

            ChangeState(new IdleState<FishermanFSM>(this));
        }

        private void OnStunned() => ChangeState(new StunState(this));

        private void OnUnstunned() => ChangeState(new FishingState(this));

        private void OnEnable()
        {
            Fisherman.Stunned += OnStunned;
            Fisherman.Unstunned += OnUnstunned;

            _movementController.PositionSet += OnPositionSet;
            _movementController.PositionReached += OnPositionReached;
        }

        private void OnDisable()
        {
            Fisherman.Stunned -= OnStunned;
            Fisherman.Unstunned -= OnUnstunned;

            _movementController.PositionSet -= OnPositionSet;
            _movementController.PositionReached -= OnPositionReached;
        }
        private void Update() => CurrentState?.Update();
    }
}