using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
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

        public static Inventory Instance { get; private set; } //TODO: hmmm

        public bool TryAddItem(Item item)
        {
            if (IsFull)
                return false;

            int index = GetEmptySpace();

            if (index < _itemSlot.Count)
            {
                item.transform.position = _itemSlot[index].transform.position;
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
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}