using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract;
using Assets.UNBAIT.Develop.Gameplay.StateMachine.Fisherman;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Fish
{
    public class FishFSM : BaseFSM<FishFSM>
    {
        private TargetLooker _targetLooker;

        private MovingEntity _entity;
        public Entities.Fish Fish { get; private set; }

        public override void StartMovement() => _entity.IsMoving = true;

        public override void StopMovement() => _entity.IsMoving = false;
        
        public void Unhook() => ChangeState(new MovingState(this));

        public void Hook() => ChangeState(new HookedState(this));

        private void OnPositionSet()
        {
            if(CurrentState != new HookedState(this))
            ChangeState(new MovingState(this));
        }

        private void OnDestroy() => _targetLooker.PositionSet -= OnPositionSet;

        private void Awake()
        {
            _entity = GetComponent<MovingEntity>();
            _targetLooker = GetComponent<TargetLooker>();
            Fish = GetComponent<Entities.Fish>();

            if (CurrentState == null)
                ChangeState(new IdleState<FishFSM>(this));

            _targetLooker.PositionSet += OnPositionSet;
        }

    }
}