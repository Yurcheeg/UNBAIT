using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement
{
    public class DirectionChooser : MonoBehaviour
    {
        public event Action DirectionSet;

        [SerializeField] private List<Transform> _borders;

        private MovingEntity _entity;
        private TargetTracker _targetTracker;
        private Entity _currentTarget;

        public Movable Movable => _entity.Movable;

        private void SetDirection(Entity target)
        {
            Vector2 direction = target != null && transform.IsFacing(target.transform, Movable.Direction)
                ? GetDirectionTo(target)
                : MoveInAStraightLine();

            Movable.SetDirection(direction);

            DirectionSet?.Invoke();
        }

        private Vector2 GetDirectionTo(Entity target) => (target.transform.position - transform.position).normalized;

        private Vector2 MoveInAStraightLine()
        {
            if (_borders == null || _borders.Count == 0)
                throw new InvalidOperationException("No borders found");

            if (Movable.Direction == Vector2.zero)
                return (Vector2.zero - (Vector2)transform.position).normalized;


            foreach (var border in _borders)
            {
                Vector2 direction = (border.transform.position - transform.position).normalized;
                float dot = Vector2.Dot(direction, Movable.Direction);

                if (dot > 0)
                    return direction;
            }

            throw new ArgumentException($"no valid direction is found");
        }

        private void OnTargetFound(Entity target)
        {
            _currentTarget = target;
            SetDirection(_currentTarget);
        }

        private void Start()
        {
            _currentTarget = _targetTracker.CurrentTarget;
            SetDirection(_targetTracker.CurrentTarget);
        }

        private void Awake()
        {
            _borders = FindObjectsByType<DestroyColidedObjects>(FindObjectsSortMode.None)
                .Select(border => border.transform)
                .ToList();//HACK

            _entity = GetComponent<MovingEntity>();
            _targetTracker = GetComponent<TargetTracker>();

            _targetTracker.TargetFound += OnTargetFound;
        }

        private void OnDestroy() => _targetTracker.TargetFound -= OnTargetFound;
    }
}
