using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float attackCooldown = 2f;
    public float safeDistance = 5f; // Расстояние до игрока для атаки
    public float detectionRadius = 10f; // Радиус обнаружения игрока

    private float lastAttackTime;
    private bool playerDetected = false;

    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected)
        {
            if (distanceToPlayer > safeDistance)
            {
                MoveTowardsPlayer();
                SetAnimatorState(isWalking: true, isAttacking: false);
            }
            else if (Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                SetAnimatorState(isWalking: false, isAttacking: true);
                lastAttackTime = Time.time;
            }
        }
        else
        {
            StopMoving();
        }
    }

    protected override void Attack()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Пуля или точка выстрела не установлены!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.shooterTag = "Enemy"; // Указываем, что пуля выпущена врагом
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }

        Destroy(bullet, 3f);
    }

    private void SetAnimatorState(bool isWalking, bool isAttacking)
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isAttacking", isAttacking);
        }
    }
}
