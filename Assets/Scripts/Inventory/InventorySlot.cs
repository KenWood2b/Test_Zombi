using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image itemImage;
    public TMP_Text quantityText;

    private NewItem currentItem;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void SetItem(NewItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }

        currentItem = item;
        itemImage.sprite = item.icon;
        itemImage.enabled = true;
        quantityText.text = item.quantity > 1 ? item.quantity.ToString() : "";
        Debug.Log($"Слот {transform.GetSiblingIndex()} назначен предмет {item.itemName}, тип: {item.itemType}");
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemImage.sprite = null;
        itemImage.enabled = false;
        quantityText.text = "";
        Debug.Log($"Слот {transform.GetSiblingIndex() + 1} очищен.");
    }

    public NewItem GetItem()
    {
        return currentItem;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && currentItem != null)
        {
            ContextMenu.Instance.ShowMenu(this, eventData.position);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            originalPosition = rectTransform.anchoredPosition;
            itemImage.transform.SetParent(canvas.transform, true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            rectTransform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            InventorySlot targetSlot = eventData.pointerEnter?.GetComponent<InventorySlot>();

            if (targetSlot != null && targetSlot != this)
            {
                NewItem tempItem = targetSlot.GetItem();
                targetSlot.SetItem(currentItem);
                SetItem(tempItem);
            }
            else
            {
                rectTransform.anchoredPosition = originalPosition;
            }

            itemImage.transform.SetParent(transform);
        }
    }
}
