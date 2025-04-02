using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Cursor = Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Cursor;
using Assets.UNBAIT.Develop.Gameplay;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _originalParent;
    private Canvas _canvas;
    private RectTransform _rectTransform;

    private Image _image;

    private Sprite Sprite
    {
        get => _image.sprite;

        set => value = _image.sprite;
    }

    public Item CurrentItem { get; private set; }

    public void SetItem(Item item)
    {
        CurrentItem = item;
        UpdateSprite();
    }

    private void UpdateSprite() => _image.sprite = CurrentItem != null ? CurrentItem.Sprite : null;

    public void OnBeginDrag(PointerEventData eventData)
    {
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
    }
}
