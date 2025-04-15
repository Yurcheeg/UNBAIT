using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public class Inventory : MonoBehaviour
    {
        public const int MaxSize = 4;

        [SerializeField] private List<Item> _items = new(MaxSize);

        [SerializeField] private List<DraggableItem> _itemSlot = new(MaxSize);

        public bool IsFull => _items.Count == MaxSize;

        public static Inventory Instance { get; private set; }

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

                if(item.TryGetComponent<Junk>(out Junk junk))//HACK
                {
                    junk.Ground();
                }

                item.transform.position = _itemSlot[index].transform.position;//HACK: xd
                _itemSlot[index].SetItem(item);

                _items.Add(item);
            }

            return true;
        }

        public void RemoveItem(Item item)
        {
            int index = _items.IndexOf(item);

            _items.RemoveAt(index);
            _itemSlot[index].SetItem(null);
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