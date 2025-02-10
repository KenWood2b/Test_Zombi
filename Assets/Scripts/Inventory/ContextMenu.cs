using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ContextMenu : MonoBehaviour
{
    public static ContextMenu Instance { get; private set; }

    public GameObject contextMenuPanel;
    public Button useButton;
    public Button dropButton;
    public Button deleteButton;

    private InventorySlot currentSlot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        contextMenuPanel.SetActive(false);

        useButton.onClick.AddListener(UseItem);
        dropButton.onClick.AddListener(DropItem);
        deleteButton.onClick.AddListener(DeleteItem);
    }

    public void ShowMenu(InventorySlot slot, Vector2 position)
    {
        currentSlot = slot;
        contextMenuPanel.transform.position = position;
        contextMenuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        contextMenuPanel.SetActive(false);
        currentSlot = null;
    }

    private void UseItem()
    {
        if (currentSlot == null || currentSlot.GetItem() == null)
        {
            Debug.LogWarning("���� ��� ������� ������!");
            HideMenu();
            return;
        }

        NewItem item = currentSlot.GetItem();
        Debug.Log($"������������ �������: {item.itemName}, ���: {item.itemType}");

        switch (item.itemType)
        {
            case ItemType.Ammo:
                UseAmmo(item);
                break;

            case ItemType.Armor:
                EquipArmor(item);
                break;

            // ����������� ���������� ����� ����� ���������
            case ItemType.Consumable:
                UseConsumable(item);
                break;

            default:
                Debug.Log($"���� ������� ������ ������������: {item.itemName}");
                break;
        }
        HideMenu();
    }

    private void UseConsumable(NewItem item)
    {
        Debug.Log($"����������� ����������� �������: {item.itemName}");
        Inventory.Instance.Remove(item);
        currentSlot.ClearSlot();
        InventoryUI.Instance?.UpdateUI();
    }




    public void EquipArmor(NewItem item)
    {
        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        if (player == null)
        {
            Debug.LogError("PlayerHealth �� ������!");
            return;
        }

        float armorHealthBonus = 25f; // ���������� �������� ��� ��������������
        player.AddHealth(armorHealthBonus);
        Debug.Log($"���������� ����������: {item.itemName}. ������� ��������: {player.CurrentHealth}/{player.maxHealth}");

        Inventory.Instance.Remove(item); // ������� �� ���������
        InventoryUI.Instance?.UpdateUI(); // ��������� UI (��������� �� null)
    }


    private void UseAmmo(NewItem item)
    {
        PlayerShooter playerShooter = FindObjectOfType<PlayerShooter>();
        if (playerShooter != null)
        {
            playerShooter.AddAmmo(item.quantity);
            Inventory.Instance.Remove(item); // ������� ������ ������������ �������
            currentSlot.ClearSlot(); // ������� ������� ����
            Debug.Log($"������� {item.itemName} ������������. ����������: {item.quantity}");
        }
    }



    private void DropItem()
    {
        if (currentSlot != null && currentSlot.GetItem() != null)
        {
            NewItem item = currentSlot.GetItem();
            Vector3 dropPosition = FindObjectOfType<PlayerShooter>().transform.position + Vector3.right;

            if (item.prefab != null)
            {
                Instantiate(item.prefab, dropPosition, Quaternion.identity);
            }

            Inventory.Instance.Remove(item);
            currentSlot.ClearSlot();
        }
        HideMenu();
    }

    private void DeleteItem()
    {
        if (currentSlot != null && currentSlot.GetItem() != null)
        {
            NewItem item = currentSlot.GetItem();
            Inventory.Instance.Remove(item);
            currentSlot.ClearSlot();
        }
        HideMenu();
    }
}
