using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using Unity.VisualScripting;
using UnityEngine;

public class JunkWorkaround : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Hook>(out Hook hook))
        {
            hook.TryHookEntity(gameObject.GetComponent<Entity>());
        }
    }
}
