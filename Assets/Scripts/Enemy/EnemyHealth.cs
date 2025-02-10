using UnityEngine;

public class EnemyHealth : Entity
{
    protected override void Die()
    {
        base.Die();
        Debug.Log("Враг погиб!");
        // Здесь можно добавить логику для начисления очков или спавна лута.
    }
}
