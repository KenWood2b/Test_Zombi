using UnityEngine;

public class EnemyHealth : Entity
{
    protected override void Die()
    {
        base.Die();
        Debug.Log("���� �����!");
        // ����� ����� �������� ������ ��� ���������� ����� ��� ������ ����.
    }
}
