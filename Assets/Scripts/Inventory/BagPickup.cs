using UnityEngine;

public class BagPickup : PickupItem
{
    public GameObject backpackButton; // Кнопка для отображения рюкзака

    public override void OnPickup(GameObject player)
    {
        if (Inventory.Instance.BackpackActive)
        {
            Debug.LogWarning("Рюкзак уже активирован!");
            return;
        }

        Debug.Log("Bag picked up!");

        if (backpackButton != null)
        {
            backpackButton.SetActive(true); // Активируем кнопку
        }

        Inventory.Instance.ActivateBackpack(); // Активируем рюкзак

        Destroy(gameObject); // Уничтожаем объект в мире
    }
}
