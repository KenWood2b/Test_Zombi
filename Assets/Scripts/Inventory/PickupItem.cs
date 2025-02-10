using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    // Абстрактный метод для обработки поднятия предмета
    public abstract void OnPickup(GameObject player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если игрок сталкивается с объектом
        if (collision.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
        }
    }
}
