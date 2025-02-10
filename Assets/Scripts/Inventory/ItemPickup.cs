using UnityEngine;

public class ItemPickup : PickupItem
{
    public NewItem itemData; // ������ ��������
    public int quantity = 1;

    public override void OnPickup(GameObject player)
    {
        Debug.Log($"������� ���������: {itemData.itemName}, ���: {itemData.itemType}, ����������: {quantity}");

        if (Inventory.Instance.Add(itemData))
        {
            Debug.Log($"������� ��������: {itemData.itemName}, ���: {itemData.itemType}");
            Destroy(gameObject); // ���������� ������ �� �����
        }
        else
        {
            Debug.LogWarning($"�� ������� ��������: {itemData.itemName}. ��������� ��������.");
        }
    }
}
