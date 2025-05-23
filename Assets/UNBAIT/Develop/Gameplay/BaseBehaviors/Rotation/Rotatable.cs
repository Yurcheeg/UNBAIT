﻿using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Rotation
{
    public sealed class Rotatable : MonoBehaviour
    {
        [field: SerializeField] public Transform Target { get; private set; } = null;

        public Quaternion RotateTowards(Transform target)
        {
            Vector2 targetPosition = target ? target.position : Vector2.zero;
            return GetRotation(targetPosition);
        }

        public Quaternion RotateTowards(Vector2 targetPosition) => GetRotation(targetPosition, calculateDirection: false);

        [ContextMenu("Rotate")]
        public Quaternion Rotate() => RotateTowards(Target);

        public void SetTarget(Transform target) => Target = target;

        private Quaternion GetRotation(Vector2 targetPosition, bool calculateDirection = true)
        {
            Vector2 direction = calculateDirection 
                ? (targetPosition - (Vector2)transform.position).normalized 
                : targetPosition;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            bool shouldFlip = direction.x < 0f;
            float angleX = shouldFlip ? 180f : 0f;

            float angleZ = shouldFlip ? -angle : angle;

            transform.localRotation = Quaternion.Euler(angleX, 0, angleZ);

            return Quaternion.Euler(angleX, 0, angleZ);
        }
    }
}
