using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class StopOnRandomPoint : MonoBehaviour, IDestinationSetter
    {
        public event Action PositionSet;
        public event Action PositionReached;

        private const float offset = 1f;
        [SerializeField] private float _startPositionValue;
        [SerializeField] private float _endPositionValue;

        [Space]

        [SerializeField] private MovementConstrains _movementConstrains;

        private float _position;

        private float _targetPosition;

        private Vector3 _startPosition;


        private IConditionMeetable _conditionMeetable;

        public bool HasReachedPosition { get; private set; }
        public bool IsMoveBackConditionMet { get; private set; }

        //TODO pls replace the region
        private IEnumerator MoveToTarget()
        {
            yield return new WaitUntil(() => HasReachedTargetPosition(_targetPosition));

            HasReachedPosition = true;
            PositionReached?.Invoke();

            if (_conditionMeetable == null)
                yield break;

            yield return new WaitUntil(() => IsMoveBackConditionMet);

            StartCoroutine(MoveBack());
        }

        private IEnumerator MoveBack()//todo replace
        {
            BaseEntity entity = GetComponent<BaseEntity>();
            entity.Movable.SetDirection(_startPosition - transform.position);
            entity.IsMoving = true;

            float startValue = _movementConstrains.GetLargestAxis(_startPosition);
            yield return new WaitUntil(() => HasReachedTargetPosition(startValue));
            entity.IsMoving = false;
        }
        private bool HasReachedTargetPosition(float targetPosition)
        {
            _position = _movementConstrains.GetLargestAxis(transform.position);
            return Mathf.Abs(_position - targetPosition) < offset;
        }
        private void OnConditionMet() => IsMoveBackConditionMet = true;

        private void Awake() => _conditionMeetable = GetComponent<MeetConditionOnCollisionWithTarget>();

        private void Start()
        {
            _startPosition = transform.position;

            _targetPosition = RandomNumber.GetInRange(_startPositionValue, _endPositionValue);

            _conditionMeetable.ConditionMet += OnConditionMet;
            PositionSet?.Invoke();
            StartCoroutine(MoveToTarget());
        }

    }
}
