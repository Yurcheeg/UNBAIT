using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;
using Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fish
{
    public class FishFSM : BaseFSM<FishFSM>
    {
        private TargetLooker _targetLooker;

        private HookableOnCollision _hookOnCollision;

        private MovingEntity _entity;

        public Hook Hook { get; private set; }

        public override void StartMovement() => _entity.IsMoving = true;

        public override void StopMovement() => _entity.IsMoving = false;
        
        public void Unhook() => ChangeState(new MovingState<FishFSM>(this));
        
        private void OnHooked(Hook hook)
        {
            Hook = hook;
            ChangeState(new HookedState(this));
        }

        private void OnPositionSet()
        {
            if(CurrentState != new HookedState(this))
            ChangeState(new MovingState<FishFSM>(this));
        }

        private void OnDisable()
        {
            _targetLooker.PositionSet -= OnPositionSet;
            _hookOnCollision.Hooked -= OnHooked;
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
        }

    }
}