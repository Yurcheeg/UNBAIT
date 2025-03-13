using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using UnityEngine;

public class DestroyColidedObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out Entity entity) == false)
            return;

        //if (entity is not Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Cursor) //TODO: no way
        Destroy(collision.gameObject);
    }
}
