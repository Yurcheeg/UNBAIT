using Assets.UNBAIT.Develop.Gameplay.Entities;
using System.Collections.Generic;
using UnityEngine;
using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement;
using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.FishFlip;
using System;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class FindHookOnCollision : MonoBehaviour
    {
        private Hook _target = null;
        [SerializeField] private HashSet<Hook> _hooksInRange = new();

        [SerializeField] private Fish _fish;
        [SerializeField] private TargetTracker _targetTracker;

        [SerializeField] private float _updateInterval = 0.3f;
        private float _timer;
        private Flip _flip;

        public Movable Movable => _fish.Movable;

        private void UpdateClosestTarget()
        {
            if (_fish.IsHooked)
                return;

            if (_hooksInRange.Count == 0)
                return;

            Hook closestEntity = GetClosestTargetInDirection();

            if (closestEntity == null)
            {
                ClearTarget();
                return;
            }

            if (ShouldUpdateTarget(closestEntity))
            {
                _target = closestEntity;
                _targetTracker.SetTarget(_target);
            }
        }

        private void ClearTarget()
        {
            if (_target == null)
                return;

            _target = null;
            _targetTracker.ClearTarget();
        }

        private Hook GetClosestTargetInDirection()
        {
            float closestDistance = float.MaxValue;
            Hook closestHook = null;

            foreach (Hook hook in _hooksInRange)
            {
                if (hook == null)
                    continue;

                if (hook.InUse)
                    continue;

                if (_fish.transform.IsFacing(hook.transform, Movable.Direction) == false)
                    continue;

                float distance = Vector2.Distance(transform.position, hook.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestHook = hook;
                }
            }

            return closestHook;
        }

        private bool ShouldUpdateTarget(Hook closestEntity)
        {
            if (closestEntity == null)
                return false;

            if (_fish.transform.IsFacing(closestEntity.transform, Movable.Direction) == false)
                return false;

            if (_target != closestEntity)
                return true;

            if (_fish.transform.IsFacing(_target.transform, Movable.Direction) == false)
                return true;

            return false;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _updateInterval)
            {
                _timer = 0f;
                UpdateClosestTarget();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Hook hook) == false)
                return;

            if (_hooksInRange.Add(hook))
                UpdateClosestTarget();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Hook hook) == false)
                return;

            if (_hooksInRange.Remove(hook))
            {
                if (_target == hook)
                    ClearTarget();
            }
            UpdateClosestTarget();
        }

        private void Start()
        {
            _flip = _fish.GetComponent<Flip>();

            UpdateClosestTarget();
        }

        private void OnEnable()
        {
            if(_fish.TryGetComponent<Flip>(out Flip flip))
            {
                _flip = flip;
                _flip.Flipped += OnFlipped;
            }

            _target = null;
        }

        private void OnDestroy() => _flip.Flipped -= OnFlipped;

        private void OnFlipped()
        {
            if (_target == null)
                return;

            if (_fish.transform.IsFacing(_target.transform, Movable.Direction) == false)
                ClearTarget();
        }
    }
}
