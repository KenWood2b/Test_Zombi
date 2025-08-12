using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoSystem : MonoBehaviour
{
    public int maxAmmo = 30; // Максимум патронов в обойме
    public int currentAmmo = 0; // Текущее количество патронов

    public TMP_Text ammoText; // Ссылка на UI-элемент для отображения патронов

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
            return 0; // Все патроны добавлены
        }
        else
        {
            currentAmmo = maxAmmo;
            UpdateAmmoUI();
            return amount - ammoNeeded; // Возвращаем остаток патронов
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
            return amount; // Возвращаем запрошенное количество патронов
        }
        else
        {
            int allAmmo = currentAmmo;
            currentAmmo = 0;
            UpdateAmmoUI();
            return allAmmo; // Возвращаем все доступные патроны
        }
    }

}
