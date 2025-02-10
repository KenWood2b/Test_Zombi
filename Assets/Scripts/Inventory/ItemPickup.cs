using UnityEngine;

public class ItemPickup : PickupItem
{
    public NewItem itemData; // Данные предмета
    public int quantity = 1;

    public override void OnPickup(GameObject player)
    {
        Debug.Log($"Попытка подобрать: {itemData.itemName}, тип: {itemData.itemType}, количество: {quantity}");

        if (Inventory.Instance.Add(itemData))
        {
            Debug.Log($"Успешно добавлен: {itemData.itemName}, тип: {itemData.itemType}");
            Destroy(gameObject); // Уничтожить объект на сцене
        }
        else
        {
            Debug.LogWarning($"Не удалось добавить: {itemData.itemName}. Инвентарь заполнен.");
        }
    }
}
