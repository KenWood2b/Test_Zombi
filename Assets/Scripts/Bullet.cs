using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public string shooterTag; // Кто выпустил пулю: "Player" или "Enemy"

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Игнорируем коллизии с тем, кто выпустил пулю
        if (collision.CompareTag(shooterTag))
        {
            return;
        }

        // Проверяем, является ли цель игроком или врагом
        Entity entity = collision.GetComponent<Entity>();
        if (entity != null)
        {
            entity.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
