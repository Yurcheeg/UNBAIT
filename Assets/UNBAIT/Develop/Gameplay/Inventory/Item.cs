using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UNBAIT.Develop.Gameplay.Inventory
{
    [RequireComponent(typeof(Entity))]
    public sealed class Item : MonoBehaviour
    {
        private Entity _entity;

        public bool IsInInventory { get; set; }

        public bool IsHooked => _entity is IHookable hookable && hookable.IsHooked;

        public Sprite Sprite { get; private set; }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Cursor.IsMouseOverTarget(gameObject) == false && Cursor.IsMouseOverUI(gameObject) == false)
                    return;

                if (IsHooked)
                    return;

                Inventory.Instance.TryAddItem(this);
            }
        }

        private void Awake()
        {
            _entity = GetComponent<Entity>();

            if (TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer))
                Sprite = renderer.sprite;
            else if (TryGetComponent<Image>(out Image image))
                Sprite = image.sprite;
        }
    }
}