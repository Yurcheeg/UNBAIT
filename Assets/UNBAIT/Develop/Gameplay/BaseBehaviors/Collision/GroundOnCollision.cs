using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundOnCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Junk junk) == false)
                return;

            junk.Ground();
        }
    }
}
