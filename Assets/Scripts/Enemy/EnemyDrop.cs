using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab; // ������ ��������
        public float dropChance; // ����������� ��������� (� ���������)
    }

    [Header("��������� �����")]
    public DropItem[] dropItems; // ������ ��������� ���������

    public void DropLoot()
    {
        foreach (var drop in dropItems)
        {
            if (Random.value <= drop.dropChance / 100f) // ���� ���������
            {
                Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
