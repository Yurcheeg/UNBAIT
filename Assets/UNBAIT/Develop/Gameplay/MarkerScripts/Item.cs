using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    public sealed class Item : MonoBehaviour
    {
        public Sprite Sprite { get; private set; }

        public bool IsPicked { get; private set; }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Cursor.IsMouseOverTarget(gameObject) == false)
                    return;

                if (IsPicked)
                    return;
                
                if (Inventory.Instance.TryAddItem(this))
                {
                    IsPicked = true;
                    //Destroy(gameObject);
                }
            }
        }

        private void Awake() => Sprite = GetComponent<SpriteRenderer>().sprite;
    }
}