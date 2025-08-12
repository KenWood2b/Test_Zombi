using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float attackRange = 2f; 
    public float detectionRadius = 10f; 
    public float damage = 5f; 
    private float lastAttackTime = 0f; 
    public float attackCooldown = 1f; 

    private bool playerDetected = false; 
    private bool isAttacking = false; 

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
                MoveTowardsPlayer();
                SetAnimatorState(isWalking: true, isAttacking: false);
            }
            else if (!isAttacking && Time.time >= lastAttackTime + attackCooldown)
            {
                StopMoving();
                StartCoroutine(AttackRoutine());
                SetAnimatorState(isWalking: false, isAttacking: true);
            }
        }
        else
        {
            StopMoving();
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
            player.GetComponent<Entity>()?.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
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
