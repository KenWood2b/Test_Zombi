using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // ����� ������ ����
    public float bulletSpeed = 20f; // �������� ����
    public int maxAmmo = 35; // ������������ ���������� �������� � ������
    public int currentAmmo = 0; // ������� ���������� �������� � ������
    public Button shootButton; // ������ ��������
    public TMP_Text ammoText; // ����� ��� ����������� ��������
    public AmmoSystem ammoSystem; // ������ �� ������� ������

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
        Debug.Log($"��������� {amount} ��������. ������� �����: {currentAmmo}");
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("��� �������� ��� ��������!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.shooterTag = "Player"; // ���������, ��� ���� �������� �������
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
