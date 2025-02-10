using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; } // ����������� ��������� ��� �������

    public int space = 16; // ������������ ���������� ������
    public List<NewItem> items = new List<NewItem>(); // ������ ���������

    private bool backpackActive = false; // ������ ��������� �������

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
            Debug.LogWarning("��������� ��������!");
            return false;
        }

        Debug.Log($"���������� ��������: {item.itemName}, ���: {item.itemType}");
        items.Add(item);
        onItemChangedCallback?.Invoke();
        return true;
    }




    public void Remove(NewItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"����� �������: {item.itemName}");
            onItemChangedCallback?.Invoke();
        }
        else
        {
            Debug.LogWarning($"������� ������� �������, �������� ��� � ���������: {item.itemName}");
        }
    }

}
