using UnityEngine;

public class BagPickup : PickupItem
{
    public GameObject backpackButton; // ������ ��� ����������� �������

    public override void OnPickup(GameObject player)
    {
        if (Inventory.Instance.BackpackActive)
        {
            Debug.LogWarning("������ ��� �����������!");
            return;
        }

        Debug.Log("Bag picked up!");

        if (backpackButton != null)
        {
            backpackButton.SetActive(true); // ���������� ������
        }

        Inventory.Instance.ActivateBackpack(); // ���������� ������

        Destroy(gameObject); // ���������� ������ � ����
    }
}
