using UnityEngine;

public class PlayerHealth : Entity
{
    protected override void Die()
    {
        base.Die();
        Debug.Log("����� �����!");
        // ����� ����� �������� ������ ��� ��������� ���� ��� ����������� ������.
    }
}
