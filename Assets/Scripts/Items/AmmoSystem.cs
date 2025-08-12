using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoSystem : MonoBehaviour
{
    public int maxAmmo = 30; // �������� �������� � ������
    public int currentAmmo = 0; // ������� ���������� ��������

    public TMP_Text ammoText; // ������ �� UI-������� ��� ����������� ��������

    private void Start()
    {
        UpdateAmmoUI();
    }

    public int AddAmmo(int amount)
    {
        int ammoNeeded = maxAmmo - currentAmmo;
        if (amount <= ammoNeeded)
        {
            currentAmmo += amount;
            UpdateAmmoUI();
            return 0; // ��� ������� ���������
        }
        else
        {
            currentAmmo = maxAmmo;
            UpdateAmmoUI();
            return amount - ammoNeeded; // ���������� ������� ��������
        }
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo.ToString();
        }
    }

    public int TakeAmmo(int amount)
    {
        if (currentAmmo >= amount)
        {
            currentAmmo -= amount;
            UpdateAmmoUI();
            return amount; // ���������� ����������� ���������� ��������
        }
        else
        {
            int allAmmo = currentAmmo;
            currentAmmo = 0;
            UpdateAmmoUI();
            return allAmmo; // ���������� ��� ��������� �������
        }
    }

}
