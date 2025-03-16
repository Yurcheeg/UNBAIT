using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class DestroyColidedObjects : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Entity entity) == false)
                return;

            Destroy(collision.gameObject);
        }
    }
}
