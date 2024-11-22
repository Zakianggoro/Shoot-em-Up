using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUpgradeMenu : MonoBehaviour
{
    public RectTransform upgradePanel; // The UI panel for upgrades
    public TextMeshProUGUI pointsText; // To display current points
    public Button upgradeAmmoButton; // Button for upgrading ammo
    public Button upgradeHealthButton; // Button for upgrading health

    public PlayerShooting playerShooting; // Reference to PlayerShooting script
    public PlayerHealth playerHealth; // Reference to PlayerHealth script

    private int playerPoints = 0; // Current points of the player
    private int ammoUpgradeCost = 50; // Cost of an ammo upgrade
    private int healthUpgradeCost = 10; // Cost of a health upgrade

    private Vector2 hiddenPosition = new Vector2(-407f, 0); // Hidden panel position
    private Vector2 visiblePosition = new Vector2(385.5579f, 0); // Visible panel position

    private bool isPanelVisible = false; // Tracks the panel's visibility state
    private bool isAnimating = false; // Tracks if the panel is animating
    private float slideDuration = 0.5f; // Duration of the slide animation

    void Start()
    {
        // Initialize panel position and set button listeners
        upgradePanel.anchoredPosition = hiddenPosition;

        upgradeAmmoButton.onClick.AddListener(UpgradeAmmo);
        upgradeHealthButton.onClick.AddListener(UpgradeHealth);

        UpdatePointsUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isAnimating)
        {
            ToggleUpgradePanel();
        }
    }

    public void AddPoints(int points)
    {
        playerPoints += points;
        UpdatePointsUI();
    }

    void UpgradeAmmo()
    {
        if (playerPoints >= ammoUpgradeCost)
        {
            playerPoints -= ammoUpgradeCost;

            playerShooting.maxAmmo += 5;

            Debug.Log("Ammo upgraded! New max ammo: " + playerShooting.maxAmmo);
            UpdatePointsUI();
        }
        else
        {
            Debug.Log("Not enough points for ammo upgrade!");
        }
    }

    void UpgradeHealth()
    {
        if (playerPoints >= healthUpgradeCost)
        {
            playerPoints -= healthUpgradeCost;

            playerHealth.Heal(10); // Call a healing method in the PlayerHealth script

            Debug.Log("Health upgraded! New health: " + playerHealth.GetCurrentHealth());
            UpdatePointsUI();
        }
        else
        {
            Debug.Log("Not enough points for health upgrade!");
        }
    }

    void ToggleUpgradePanel()
    {
        isPanelVisible = !isPanelVisible;
        Vector2 targetPosition = isPanelVisible ? visiblePosition : hiddenPosition;
        StartCoroutine(SlidePanel(targetPosition));
    }

    System.Collections.IEnumerator SlidePanel(Vector2 targetPosition)
    {
        isAnimating = true;

        Vector2 startPosition = upgradePanel.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / slideDuration);
            upgradePanel.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        upgradePanel.anchoredPosition = targetPosition;
        isAnimating = false;
    }

    void UpdatePointsUI()
    {
        pointsText.text = $"Points: {playerPoints}";
    }
}
