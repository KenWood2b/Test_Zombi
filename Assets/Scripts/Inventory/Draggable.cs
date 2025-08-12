using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("Canvas не найден! Убедитесь, что объект Draggable находится внутри Canvas.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsDragSuccessful(eventData))
        {
            StartCoroutine(SmoothReturn());
        }
        else
        {
            Debug.Log("Перетаскивание завершено успешно.");
        }
    }

    private bool IsDragSuccessful(PointerEventData eventData)
    {
        GameObject targetObject = eventData.pointerEnter;

        if (targetObject != null && targetObject.GetComponent<InventorySlot>() != null)
        {
            return true;
        }

        return false;
    }

    private IEnumerator SmoothReturn()
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;
        float duration = 0.25f;

        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }
}
