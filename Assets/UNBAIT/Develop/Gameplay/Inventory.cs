using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public class Inventory : MonoBehaviour
    {
        public const int MaxSize = 4;

        [SerializeField] private  List<Item> _items = new();

        private bool _isFull;

        public  bool IsFull
        {
            get => _items.Count == MaxSize;

            private set => _isFull = value;
        }

        public static Inventory Instance; //TODO: hmmm
        
        public bool TryAddItem(Item item)
        {
            if (IsFull)
                return false;

            _items.Add(item);
            return true;
        }

        public void RemoveItem(Item item) => _items.Add(item);
        
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