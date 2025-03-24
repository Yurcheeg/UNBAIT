using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    public sealed class Item : MonoBehaviour
    {
        private void Update()
        {
            if (Cursor.IsMouseOverTarget(gameObject) == false)
                return;
            if (Inventory.Instance.IsFull)
                return;

            if (Input.GetMouseButtonDown(1) == false)
                return;

            Inventory.Instance.TryAddItem(this);
        }
    }
}