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
        // Сохраняем начальную позицию, чтобы вернуть её при необходимости
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Перемещаем объект в точку указателя мыши
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Проверяем успешность перетаскивания
        if (!IsDragSuccessful(eventData))
        {
            StartCoroutine(SmoothReturn());
        }
        else
        {
            Debug.Log("Перетаскивание завершено успешно.");
        }
    }

    /// <summary>
    /// Проверяет, успешно ли было перетаскивание.
    /// </summary>
    /// <param name="eventData">Данные о событии перетаскивания.</param>
    /// <returns>True, если перетаскивание было успешным; иначе False.</returns>
    private bool IsDragSuccessful(PointerEventData eventData)
    {
        // Получаем объект под указателем мыши
        GameObject targetObject = eventData.pointerEnter;

        // Проверяем, попал ли объект в другой слот инвентаря
        if (targetObject != null && targetObject.GetComponent<InventorySlot>() != null)
        {
            return true; // Перетаскивание успешно
        }

        return false; // Перетаскивание не удалось
    }

    /// <summary>
    /// Плавно возвращает объект на исходную позицию.
    /// </summary>
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
