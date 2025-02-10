using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab; // Префаб предмета
        public float dropChance; // Вероятность выпадения (в процентах)
    }

    [Header("Настройки дропа")]
    public DropItem[] dropItems; // Список возможных предметов

    public void DropLoot()
    {
        foreach (var drop in dropItems)
        {
            if (Random.value <= drop.dropChance / 100f) // Шанс выпадения
            {
                Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
