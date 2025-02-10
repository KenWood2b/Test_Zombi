using UnityEngine;

public abstract class Enemy : Entity
{
    protected Transform player;
    public float speed = 2f; // Скорость врага
    public float stopDistance = 2f; // Дистанция, на которой враг останавливается
    private bool playerInSight = false; // Игрок в зоне видимости

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Ищем игрока
                                                                      
        IgnoreEnemyCollisions();
        IgnorePlayerCollision();

    }

    protected virtual void Update()
    {
        if (player != null && playerInSight)
        {
            RotateTowardsPlayer();

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > stopDistance)
            {
                MoveTowardsPlayer();
            }
            else
            {
                StopMoving();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = true;
            Debug.Log($"{gameObject.name}: Игрок вошёл в зону видимости!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = false;
            Debug.Log($"{gameObject.name}: Игрок вышел из зоны видимости.");
        }
    }

    protected void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;

        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    protected void StopMoving()
    {
        // Останавливаем движение врага
    }

    protected void MoveTowardsPlayer()
    {
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void IgnoreEnemyCollisions()
    {
        Collider2D thisCollider = GetComponent<Collider2D>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy otherEnemy in enemies)
        {
            if (otherEnemy != this) // Исключаем себя
            {
                Collider2D otherCollider = otherEnemy.GetComponent<Collider2D>();
                if (otherCollider != null)
                {
                    Physics2D.IgnoreCollision(thisCollider, otherCollider);
                }
            }
        }
    }

    private void IgnorePlayerCollision()
    {
        Collider2D thisCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Collider2D>();

        if (thisCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(thisCollider, playerCollider);
        }
    }

    protected abstract void Attack();
}
