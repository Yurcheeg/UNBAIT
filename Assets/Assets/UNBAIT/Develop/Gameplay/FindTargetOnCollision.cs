using Assets.Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay
{
    public class FindTargetOnCollision : MonoBehaviour
    {
        public event Action<Entity> FoundEntity;

        private Entity _baseObject;

        [SerializeField] private List<Entity> _entitiesInRange = new();

        [SerializeField] private CircleCollider2D _circleCollider;

        [SerializeField] private Targets _targetToFind;

        private Type _targetType;
        private Entity _target = null;

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

                if(_target != closestEntity)
                {
                    _target = closestEntity;
                }
            }

            FoundEntity?.Invoke(_target);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Entity>(out Entity entity) == false)
                return;

            if(entity.GetType() == _targetType)
            {
                _entitiesInRange.Add(entity);
                UpdateClosestTarget();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent<Entity>(out Entity entity) == false)
                return;

            if(_entitiesInRange.Contains(entity))
            {
                _entitiesInRange.Remove(entity);
                UpdateClosestTarget();
            }
        }

        private void Awake()
        {
            _targetType = Target.GetType(_targetToFind);
            _circleCollider = GetComponent<CircleCollider2D>();
            _baseObject = GetComponentInParent<Entity>();
        }

        private void OnEnable() => _target = null;
    }
}
