using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static event Action ItemPicked;
        public static event Action ItemUsed;

        public const int MaxSize = 4;

        [SerializeField] private List<Item> _items = new(MaxSize);

        [SerializeField] private List<DraggableItem> _itemSlot = new(MaxSize);

        public bool IsFull => _items.Count == MaxSize;

        public static Inventory Instance { get; private set; }

        public List<Item> Items => _items;

        public bool TryAddItem(Item item)
        {
            if (IsFull)
                return false;

            int index = GetEmptySpace();

            if (index < _itemSlot.Count)
            {
                if (item.TryGetComponent<MovingEntity>(out MovingEntity entity))//TODO: fix. maybe replace
                {
                    entity.Movable.SetDirection(Vector2.up);
                    entity.IsMoving = false;
                }

                if (item.TryGetComponent<Junk>(out Junk junk))//HACK
                {
                    junk.Ground();
                }

                StartCoroutine(LerpItem(item, _itemSlot[index]));

                //item.transform.position = _itemSlot[index].transform.position;//HACK: xd
                _itemSlot[index].SetItem(item);
                item.IsInInventory = true;

                _items.Add(item);

                ItemPicked?.Invoke();
            }

            return true;
        }

        private IEnumerator LerpItem(Item item, DraggableItem slot, float duration = 0.5f)
        {
            var rb = item.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic; // stops items from falling off the inventory
            rb.simulated = false; //stops hitting items when 'travelling'

            Vector2 startPosition = item.transform.position;

            Vector2 slotPosition = RectTransformUtility.WorldToScreenPoint(null, slot.GetComponent<RectTransform>().position);

            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(slotPosition);

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float time = elapsedTime / duration;
                item.transform.position = Vector2.Lerp(startPosition, worldPosition, time * 3f);//3f - _speed
                yield return null;
            }

            item.transform.position = worldPosition;
        }

        public void RemoveItem(Item item)
        {
            var rb = item.GetComponent<Rigidbody2D>();
            rb.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            rb.simulated = true;

            int index = _items.IndexOf(item);

            _items.RemoveAt(index);
            _itemSlot[index].SetItem(null);
            item.IsInInventory = false;

            ItemUsed?.Invoke();
        }

        private int GetEmptySpace()
        {
            for (int i = 0; i < MaxSize; i++)
            {
                if (i >= _items.Count || _items[i] == null)
                    return i;
            }

            throw new ArgumentOutOfRangeException("Inventory is full");
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
    }
}