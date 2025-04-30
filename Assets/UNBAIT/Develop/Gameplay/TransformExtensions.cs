using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public static class TransformExtensions
    {
        public static bool IsFacing(this Transform origin, Transform target, Vector2 direction, float dotThreshold = 0.1f)
        {
            Vector2 directionToTarget = (target.position - origin.position).normalized;
            return Vector2.Dot(direction.normalized, directionToTarget) > dotThreshold;
        }
    }
}