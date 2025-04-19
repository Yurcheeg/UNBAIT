using Assets.UNBAIT.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundJunkOnCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Junk junk) == false)
                return;

            junk.Ground();
        }
    }
}
