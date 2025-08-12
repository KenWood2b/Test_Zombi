using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public string shooterTag; // ��� �������� ����: "Player" ��� "Enemy"

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������� �������� � ���, ��� �������� ����
        if (collision.CompareTag(shooterTag))
        {
            return;
        }

        // ���������, �������� �� ���� ������� ��� ������
        Entity entity = collision.GetComponent<Entity>();
        if (entity != null)
        {
            entity.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
