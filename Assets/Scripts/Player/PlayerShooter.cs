using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка спауна пули
    public float bulletSpeed = 20f; // Скорость пули
    public int maxAmmo = 35; // Максимальное количество патронов в обойме
    public int currentAmmo = 0; // Текущее количество патронов в обойме
    public Button shootButton; // Кнопка стрельбы
    public TMP_Text ammoText; // Текст для отображения патронов
    public AmmoSystem ammoSystem; // Ссылка на систему обоймы

    private void Start()
    {
        if (shootButton != null)
        {
            shootButton.onClick.AddListener(Shoot);
        }
        UpdateAmmoUI();
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        UpdateAmmoUI();
        Debug.Log($"Добавлено {amount} патронов. Текущий запас: {currentAmmo}");
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Нет патронов для стрельбы!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.shooterTag = "Player"; // Указываем, что пуля выпущена игроком
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.up * bulletSpeed;
        }

        currentAmmo--;
        UpdateAmmoUI();
        Destroy(bullet, 3f);
    }

    public void ReloadAmmo()
    {
        if (ammoSystem != null)
        {
            int ammoNeeded = maxAmmo - currentAmmo;
            int ammoTaken = ammoSystem.TakeAmmo(ammoNeeded);
            currentAmmo += ammoTaken;
            UpdateAmmoUI();
        }
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Ammo: {currentAmmo}";
        }
    }
}
