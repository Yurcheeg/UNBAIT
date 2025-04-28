using Assets.UNBAIT.Develop.Gameplay.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.UNBAIT.Develop.Gameplay.Inventory
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform _originalParent;
        private Canvas _canvas;
        private RectTransform _rectTransform;

        private Image _image;

        private Sprite Sprite
        {
            get => _image.sprite;

            set => _image.sprite = value;
        }

        public Item CurrentItem { get; private set; }

        public void SetItem(Item item)
        {
            CurrentItem = item;
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            Sprite = CurrentItem == null ? null : CurrentItem.Sprite;

            _image.color = Sprite == null ? Color.clear : Color.white;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var isTutorial = FindAnyObjectByType(typeof(TutorialManager)) != null;//HACK workaround for tutorial
            if (isTutorial)
            {
                print(isTutorial);
                _originalParent = FindAnyObjectByType(typeof(Hook)).GameObject().transform;
            }
            else
                _originalParent = transform.parent;

            transform.SetParent(_canvas.transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Sprite == null)
                return;

            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (CurrentItem != null)
            {
                CurrentItem.transform.position = Cursor.GetMousePosition();
                Inventory.Instance.RemoveItem(CurrentItem);
            }

            transform.SetParent(_originalParent);
            _rectTransform.anchoredPosition = Vector2.zero;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();

            _image = GetComponent<Image>();

            UpdateSprite();
        }
    }
}
