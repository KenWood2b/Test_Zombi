using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ������� ������ (�������, ������� ���)
    public int enemyCount = 3; // ���������� ������ ��� ������
    public Vector2 spawnAreaMin; // ����������� ����� ������ (x, y)
    public Vector2 spawnAreaMax; // ������������ ����� ������ (x, y)

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(randomEnemy, randomPosition, Quaternion.identity);
        }
    }
}
