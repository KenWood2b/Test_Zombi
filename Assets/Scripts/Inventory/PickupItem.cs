using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    public abstract void OnPickup(GameObject player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
        }
    }
}
