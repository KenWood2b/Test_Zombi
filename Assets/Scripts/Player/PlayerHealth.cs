using UnityEngine;

public class PlayerHealth : Entity
{
    protected override void Die()
    {
        base.Die();
        Debug.Log("Игрок погиб!");
        // Здесь можно добавить логику для окончания игры или перезапуска уровня.
    }
}
