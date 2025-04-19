using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision
{
    public class DestroyColidedObjects : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Entity _) == false)
                return;

            Destroy(collision.gameObject);
        }
    }
}
