using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class FindTargetOnCollision : MonoBehaviour
    {
        public event Action<Entity> TargetFound;

        [SerializeField] private EntityType _targetToFind;
        private Type _targetType;
        private Entity _target = null;

        [SerializeField] private List<Entity> _entitiesInRange = new();
        
        private void UpdateClosestTarget()
        {
            if (_entitiesInRange.Count == 0)
            {
                _target = null;

                TargetFound?.Invoke(_target);
                return;
            }

            float closestEntityDistance = float.MaxValue;
            Entity closestEntity = null;

            foreach (Entity entity in _entitiesInRange)
            {
                if (entity == null)
                    continue;

                if (entity.TryGetComponent<Hook>(out Hook hook) && hook.InUse)//TODO: costyl'
                    continue;

                //TODO: replace after replacing the method
                if (GetComponentInParent<BaseEntity>().IsNotLookingAt(entity.gameObject))
                    continue;

                float distance = Vector2.Distance(transform.position, entity.transform.position);

                if (distance < closestEntityDistance)
                {
                    closestEntityDistance = distance;
                    closestEntity = entity;
                }

                if (_target != closestEntity)
                    _target = closestEntity;
            }

            TargetFound?.Invoke(_target);
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

        private void Start() => UpdateClosestTarget();

        private void Awake() => _targetType = Target.GetType(_targetToFind);

        private void OnEnable() => _target = null;
    }
}
