using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class FindHookOnCollision : MonoBehaviour
    {
        public event Action<Entity> TargetFound;

        private Hook _target = null;

        [SerializeField] private List<Hook> _hooksInRange = new();

        [SerializeField] private Fish _fish;
        
        private void UpdateClosestTarget()
        {
            if (_fish.IsHooked)
                return;

            if (_hooksInRange.Count == 0)
            {
                _target = null;

                TargetFound?.Invoke(_target);
                return;
            }

            float closestEntityDistance = float.MaxValue;
            Hook closestEntity = null;

            foreach (Hook hook in _hooksInRange)
            {
                if (hook == null)
                    continue;

                if (hook.InUse)
                    continue;

                //TODO: replace after replacing the method
                if (GetComponentInParent<MovingEntity>().IsNotLookingAt(hook.gameObject))
                    continue;

                float distance = Vector2.Distance(transform.position, hook.transform.position);

                if (distance < closestEntityDistance)
                {
                    closestEntityDistance = distance;
                    closestEntity = hook;
                }

                if (_target != closestEntity)
                    _target = closestEntity;
            }

            TargetFound?.Invoke(_target);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Hook hook) == false)
                return;

                _hooksInRange.Add(hook);
                UpdateClosestTarget();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Hook hook) == false)
                return;

            if (_hooksInRange.Contains(hook))
            {
                _hooksInRange.Remove(hook);
                UpdateClosestTarget();
            }
        }
        
        private void Start() => UpdateClosestTarget();

        private void OnEnable() => _target = null;
    }
}
