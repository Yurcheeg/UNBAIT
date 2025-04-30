using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement
{
    public enum MovementAxis { X, Y }

    public class MovementController : MonoBehaviour
    {
        public event Action PositionSet;
        public event Action PositionReached;

        [SerializeField] private float _reachOffset = 0.01f;
        [SerializeField] private MovementAxis _movementAxis;

        private MovingEntity _entity;
        private float _targetPositionValue;
        private bool _moving;

        public MovementAxis MovementAxis => _movementAxis;

        public void MoveTo(float targetValue)
        {
            _targetPositionValue = targetValue;
            _moving = true;

            PositionSet?.Invoke();
        }

        private Vector2 GetDirection()
        {
            Vector2 currentPosition = transform.position;

            return _movementAxis switch
            {
                MovementAxis.X => new Vector2(_targetPositionValue - currentPosition.x, 0),
                MovementAxis.Y => new Vector2(0, _targetPositionValue - currentPosition.y),
                _ => throw new ArgumentException("No valid axis found"),
            };
        }

        private void Update()
        {
            if (_moving == false)
                return;

            Vector2 direction = GetDirection();
            _entity.Movable.SetDirection(direction);

            if (HasReachedTarget())
            {
                _moving = false;
                _entity.Movable.SetDirection(default);

                PositionReached?.Invoke();
            }
        }

        private bool HasReachedTarget()
        {
            return _movementAxis switch
            {
                MovementAxis.X => Mathf.Abs(transform.position.x - _targetPositionValue) < _reachOffset,
                MovementAxis.Y => Mathf.Abs(transform.position.y - _targetPositionValue) < _reachOffset,
                _ => throw new ArgumentException("No valid axis found"),
            };
        }

        private void Awake() => _entity = GetComponent<MovingEntity>();
    }
}
