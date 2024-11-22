using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUpgradeMenu : MonoBehaviour
{
    public GameObject upgradePanel; // The UI panel for upgrades
    public TextMeshProUGUI pointsText; // To display current points
    public Button upgradeAmmoButton; // Button for upgrading ammo

    public PlayerShooting playerShooting; // Reference to PlayerShooting script

    private int playerPoints = 0; // Current points of the player
    private int upgradeCost = 50; // Cost of an ammo upgrade

    void Start()
    {
        // Initialize UI and hide panel
        upgradePanel.SetActive(false);

        // Set button listener
        upgradeAmmoButton.onClick.AddListener(UpgradeAmmo);

        UpdatePointsUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
        if (playerPoints >= upgradeCost)
        {
            playerPoints -= upgradeCost;

            playerShooting.maxAmmo += 5;

            Debug.Log("Ammo upgraded! New max ammo: " + playerShooting.maxAmmo);
            UpdatePointsUI();
        }
        else
        {
            Debug.Log("Not enough points for upgrade!");
        }
    }

    void ToggleUpgradePanel()
    {
        upgradePanel.SetActive(!upgradePanel.activeSelf);
    }

    void UpdatePointsUI()
    {
        // Update the points display
        pointsText.text = $"Points: {playerPoints}";
    }
}
