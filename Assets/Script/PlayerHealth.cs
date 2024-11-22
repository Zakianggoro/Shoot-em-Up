using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    private int currentHealth;

    public GameObject gameOverPanel; // Assign your "You Died" panel here
    public TextMeshProUGUI healthText; // Assign a TextMeshProUGUI element for health display
    public int damageAmount = 10; // Damage dealt when an enemy enters the trigger area
    public Collider damageTriggerArea; // Assign the trigger area collider here

    void Start()
    {
        // Initialize health and hide the Game Over panel
        currentHealth = maxHealth;
        UpdateHealthText();
        gameOverPanel.SetActive(false);

        
        if (damageTriggerArea != null)
        {
            damageTriggerArea.isTrigger = true; 
        }
        else
        {
            Debug.LogWarning("No damage trigger area assigned!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (damageTriggerArea != null && other.CompareTag("Enemy"))
        {
            if (other.bounds.Intersects(damageTriggerArea.bounds))
            {
                TakeDamage(damageAmount);

                Destroy(other.gameObject);
            }
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Time.timeScale = 0f; 
        gameOverPanel.SetActive(true);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthText();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }
}
