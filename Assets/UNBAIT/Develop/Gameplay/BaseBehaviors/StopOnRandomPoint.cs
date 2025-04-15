using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField] private float _moveBackDelaySeconds = 0.5f;

        [Space]

        [SerializeField] private MovementConstrains _movementConstrains;

        private float _position;

        private float _targetPosition;

        private Vector3 _startPosition;

        private List<MoveBackCondition> _conditions = new();
        private MovingEntity _entity;

        public bool HasReachedPosition { get; private set; }
        public bool IsMoveBackConditionMet { get; private set; }

        private IEnumerator MoveToTarget()
        {
            yield return new WaitUntil(() => HasReachedTargetPosition(_targetPosition) || IsMoveBackConditionMet);

            HasReachedPosition = true;
            PositionReached?.Invoke();

            if (_conditions.Count == 0)
                throw new ArgumentNullException($"No conditions, {nameof(MoveBack)} will not trigger");

            yield return new WaitUntil(() => IsMoveBackConditionMet);

            yield return new WaitForSeconds(_moveBackDelaySeconds);
            StartCoroutine(MoveBack());
        }

        private IEnumerator MoveBack()//TODO: replace
        {
            _entity.Movable.SetDirection(_startPosition - transform.position);

            float startValue = _movementConstrains.GetLargestAxis(_startPosition);

            PositionSet?.Invoke();

            yield return new WaitUntil(() => HasReachedTargetPosition(startValue));

            yield return new WaitForSeconds(1.5f);
            if (gameObject != null)
                Destroy(gameObject);
        }
        private bool HasReachedTargetPosition(float targetPosition)
        {
            _position = _movementConstrains.GetLargestAxis(transform.position);
            return Mathf.Abs(_position - targetPosition) < offset;//destroy if past offset
        }

        private void OnConditionMet()
        {
            if (IsMoveBackConditionMet)
                return;
            IsMoveBackConditionMet = true;

            foreach (MoveBackCondition condition in _conditions)
            {
                condition.ConditionMet -= OnConditionMet;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (TryGetComponent<Fisherman>(out _) == false)
                return;

            if (collision.gameObject.TryGetComponent<Fisherman>(out _) == false)
                return;

            _targetPosition += Mathf.Sign(_targetPosition) /** offset*/ * 2;
        }

        private void Awake()
        {
            _conditions = GetComponents<MoveBackCondition>().ToList();
            _entity = GetComponent<MovingEntity>();
        }


        private void OnDestroy()
        {
            foreach (MoveBackCondition condition in _conditions)
            {
                condition.ConditionMet -= OnConditionMet;
            }
        }
        private void Start()
        {
            foreach (MoveBackCondition condition in _conditions)
            {
                condition.ConditionMet += OnConditionMet;
            }

            _startPosition = transform.position;

            _targetPosition = RandomNumber.GetInRange(_startPositionValue, _endPositionValue);

            PositionSet?.Invoke();

            StartCoroutine(MoveToTarget());
        }

    }
}
