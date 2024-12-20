using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public int maxAmmo = 10; // Maximum ammo capacity
    public int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    public float shootForce = 10f;

    public TextMeshProUGUI currentAmmoText; // TextMeshPro for current ammo display
    public TextMeshProUGUI maxAmmoText; // TextMeshPro for max ammo display
    public TextMeshProUGUI scoreText; // TextMeshPro for score display

    public PlayerUpgradeMenu playerUpgradeMenu; // Reference to the PlayerUpgradeMenu script

    public AudioSource gunAudioSource; // AudioSource for sound effects
    public AudioClip gunshotClip; // Gunshot sound effect clip
    public AudioClip reloadClip; // Reload sound effect clip

    private int score = 0; // Player's score

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        UpdateScoreUI();
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.Space)) // Fire weapon
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("Out of ammo! Reload!");
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) // Reload weapon
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();

        // Spawn the bullet
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootPoint.forward * shootForce, ForceMode.VelocityChange);

        // Play gunshot sound
        if (gunAudioSource != null && gunshotClip != null)
        {
            gunAudioSource.PlayOneShot(gunshotClip);
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;

        // Play reload sound
        if (gunAudioSource != null && reloadClip != null)
        {
            gunAudioSource.PlayOneShot(reloadClip);
        }

        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
        Debug.Log("Reloaded!");
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();

        if (playerUpgradeMenu != null)
        {
            playerUpgradeMenu.AddPoints(points);
        }
    }

    void UpdateAmmoUI()
    {
        // Update current ammo display
        if (currentAmmoText != null)
        {
            currentAmmoText.text = $"{currentAmmo}";
        }

        // Update max ammo display
        if (maxAmmoText != null)
        {
            maxAmmoText.text = $"{maxAmmo}";
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Points: {score}";
        }
    }
}
