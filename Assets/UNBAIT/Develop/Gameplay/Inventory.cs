using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public class Inventory : MonoBehaviour
    {
        public const int MaxSize = 4;

        [SerializeField] private List<Item> _items = new(MaxSize);

        [SerializeField] private List<Image> _itemIcons = new(MaxSize);

        public bool IsFull => _items.Count == MaxSize;

        public static Inventory Instance { get; private set; } //TODO: hmmm

        public bool TryAddItem(Item item)
        {
            if (IsFull)
                return false;

            int index = GetEmptySpace();
            _items.Add(item);

            if(index < _itemIcons.Count)
            {
                //item.transform.position = _itemIcons[index].transform.position;
                _itemIcons[index].sprite = item.Sprite;

            }

            return true;
        }

        public void RemoveItem(Item item) => _items.Remove(item);

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