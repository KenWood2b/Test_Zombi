using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float attackRange = 2f; // Дистанция атаки
    public float detectionRadius = 10f; // Радиус обнаружения игрока
    public float damage = 5f; // Урон
    private float lastAttackTime = 0f; // Последнее время атаки
    public float attackCooldown = 1f; // Интервал между атаками

    private bool playerDetected = false; // Флаг обнаружения игрока
    private bool isAttacking = false; // Флаг текущей атаки

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

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Проверяем, находится ли игрок в радиусе обнаружения
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
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer(); // Двигаемся к игроку
                SetAnimatorState(isWalking: true, isAttacking: false);
            }
            else if (!isAttacking && Time.time >= lastAttackTime + attackCooldown)
            {
                StopMoving(); // Останавливаем движение
                StartCoroutine(AttackRoutine()); // Запускаем атаку
                SetAnimatorState(isWalking: false, isAttacking: true);
            }
        }
        else
        {
            StopMoving(); // Если игрок вне зоны обнаружения, стоим на месте
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        Attack(); // Атакуем игрока
        lastAttackTime = Time.time;
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    protected override void Attack()
    {
        Debug.Log("Враг ближнего боя атакует игрока!");
        if (player != null)
        {
            player.GetComponent<Entity>()?.TakeDamage(damage); // Наносим урон игроку
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Радиус атаки
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Радиус обнаружения
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
