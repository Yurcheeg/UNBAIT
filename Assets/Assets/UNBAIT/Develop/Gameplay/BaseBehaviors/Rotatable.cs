using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class Rotatable : MonoBehaviour
    {
        public Transform Target { get; private set; } = null;

        public Quaternion Rotate(Transform target)
        {
            Vector2 targetPosition = target ? target.position : Vector2.zero;
            Vector2 direction = (Vector2.zero - targetPosition).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float angleX = Mathf.Abs(angle) >= 90 ? 180f : 0f;
            float angleZ = Mathf.Abs(angleX) == 180f ? (Mathf.Sign(direction.x) * angle) : angle;

            //TODO: Hardcode. Replace
            return Quaternion.Euler(angleX, 0, angleZ);
        }

        public Quaternion Rotate() => Rotate(Target);

        public void SetTarget(Transform target) => Target = target;
    }
}
