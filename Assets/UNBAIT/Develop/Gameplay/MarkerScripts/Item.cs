using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    [RequireComponent(typeof(Entity))]
    public sealed class Item : MonoBehaviour
    {
        private Entity _entity;

        public bool IsHooked => _entity is IHookable hookable && hookable.IsHooked;

        public Sprite Sprite { get; private set; }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Cursor.IsMouseOverTarget(gameObject) == false)
                    return;

                if (IsHooked)
                    return;

                Inventory.Instance.TryAddItem(this);
            }
        }

        private void Awake()
        {
            _entity = GetComponent<Entity>();
            Sprite = GetComponent<SpriteRenderer>().sprite;
        }
    }
}