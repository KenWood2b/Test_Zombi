using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; } // Статический экземпляр для доступа

    public int space = 16; // Максимальное количество слотов
    public List<NewItem> items = new List<NewItem>(); // Список предметов

    private bool backpackActive = false; // Статус активации рюкзака

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplicate Inventory instance detected!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool BackpackActive
    {
        get => backpackActive;
        private set => backpackActive = value;
    }

    public void ActivateBackpack()
    {
        BackpackActive = true;
        Debug.Log("Backpack activated!");
    }

    public bool HasSpace()
    {
        return items.Count < space;
    }

    public bool Add(NewItem item)
    {
        if (!HasSpace())
        {
            Debug.LogWarning("Инвентарь заполнен!");
            return false;
        }

        Debug.Log($"Добавление предмета: {item.itemName}, тип: {item.itemType}");
        items.Add(item);
        onItemChangedCallback?.Invoke();
        return true;
    }




    public void Remove(NewItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"Удалён предмет: {item.itemName}");
            onItemChangedCallback?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Попытка удалить предмет, которого нет в инвентаре: {item.itemName}");
        }
    }

}
