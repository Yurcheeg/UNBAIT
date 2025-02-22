using Assets.Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class FindTargetOnCollision : MonoBehaviour
    {
        public event Action<Entity> FoundEntity;

        [SerializeField] private EntityType _targetToFind;

        private Type _targetType;
        private Entity _target = null;

        private List<Entity> _entitiesInRange = new();

        private CircleCollider2D _circleCollider;

        private void UpdateClosestTarget()
        {
            if (_entitiesInRange.Count == 0)
                return;

            float closestEntityDistance = float.MaxValue;
            Entity closestEntity = null;

            foreach (Entity entity in _entitiesInRange)
            {
                if (entity == null)
                    continue;

                float distance = Vector2.Distance(transform.position, entity.transform.position);

                if (distance < closestEntityDistance)
                {
                    closestEntityDistance = distance;
                    closestEntity = entity;
                }

                if (_target != closestEntity)
                {
                    _target = closestEntity;
                }
            }
            //TODO: can be null, should be handled appropriately
            FoundEntity?.Invoke(_target);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Entity entity) == false)
                return;

            if (entity.GetType() == _targetType)
            {
                _entitiesInRange.Add(entity);
                UpdateClosestTarget();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Entity entity) == false)
                return;

            if (_entitiesInRange.Contains(entity))
            {
                _entitiesInRange.Remove(entity);
                UpdateClosestTarget();
            }
        }

        private void Awake()
        {
            _circleCollider = GetComponent<CircleCollider2D>();

            _targetType = Target.GetType(_targetToFind);
        }

        private void OnEnable() => _target = null;
    }
}
