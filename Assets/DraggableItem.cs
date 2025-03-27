using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Cursor = Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Cursor;
using Assets.UNBAIT.Develop.Gameplay;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private Canvas canvas;
    private RectTransform rectTransform;

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

    private void UpdateSprite()
    {
        _image.sprite = CurrentItem != null ? CurrentItem.Sprite : null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Sprite == null)
            return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CurrentItem != null)
        {
            CurrentItem.transform.position = Cursor.GetMousePosition();
            Inventory.Instance.RemoveItem(CurrentItem);
        }

        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        _image = GetComponent<Image>();
    }
}
