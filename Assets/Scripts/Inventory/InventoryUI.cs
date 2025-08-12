using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public static InventoryUI Instance { get; private set; }
    public Transform slotsParent;
    public GameObject inventoryPanel;

    private InventorySlot[] slots;

    private void Start()
    {
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        Inventory.Instance.onItemChangedCallback += UpdateUI;
        inventoryPanel.SetActive(false);
    }

    public int GetFreeSlotsCount()
    {
        return slots.Length - Inventory.Instance.items.Count;
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        Debug.Log($"Инвентарь: {(inventoryPanel.activeSelf ? "открыт" : "закрыт")}");
    }


    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Inventory.Instance.items.Count)
            {
                slots[i].SetItem(Inventory.Instance.items[i]);
            }
            else if (slots[i].GetItem() != null)
            {
                slots[i].ClearSlot();
            }
        }
    }

}
