using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;
    public Animator animator; // Ссылка на Animator для анимаций персонажа

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 initialScale; // Для сохранения начального масштаба

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale; // Сохраняем начальный масштаб персонажа
        IgnoreEnemyCollisions();
    }

    void Update()
    {
        // Получаем ввод от джойстика
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        // Поворот персонажа в зависимости от направления движения
        if (movement.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movement.x) * initialScale.x, initialScale.y, initialScale.z);
        }

        // Управление анимацией
        bool isWalking = movement.magnitude > 0;
        animator.SetBool("isWalking", isWalking);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
    }

    void FixedUpdate()
    {
        // Двигаем персонажа
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void IgnoreEnemyCollisions()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
            if (playerCollider != null && enemyCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, enemyCollider);
            }
        }
    }
}
