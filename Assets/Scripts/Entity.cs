using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    [Header("Entity Settings")]
    public float maxHealth = 100f;
    protected float currentHealth;
    public float CurrentHealth => currentHealth;

    [Header("UI Settings")]
    public Image healthBar; // Полоска здоровья

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void AddHealth(float amount, bool increaseMaxHealth = false)
    {
        if (increaseMaxHealth)
        {
            maxHealth += amount;
        }
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        Debug.Log($"Здоровье увеличено на {amount}. Текущее здоровье: {currentHealth}/{maxHealth}");
    }


    public float GetCurrentHealth() // Метод для получения текущего здоровья
    {
        return currentHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        EnemyDrop drop = GetComponent<EnemyDrop>();
        if (drop != null)
        {
            drop.DropLoot();
        }
        Debug.Log($"{gameObject.name} погиб.");
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth; // Обновляем UI полоску здоровья
        }
    }
}
