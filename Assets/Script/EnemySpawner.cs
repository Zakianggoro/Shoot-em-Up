using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class Wave
{
    public int enemyCount = 5; // Number of enemies in this wave
    public GameObject enemyPrefab; // Enemy prefab for this wave
    public List<Transform> spawnAreas; // Spawn areas for this wave
}

public class EnemySpawner : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>(); // List of waves
    public TextMeshProUGUI waveText; // UI element to display the current wave
    public TextMeshProUGUI statusText; // UI element to display wave/rest status

    private int currentWaveIndex = 0; // Current wave index
    private bool isSpawning = false; // Tracks if a wave is currently spawning
    private List<GameObject> activeEnemies = new List<GameObject>(); // List of currently spawned enemies

    void Start()
    {
        UpdateWaveUI();
        StartCoroutine(StartWaveRoutine());
    }

    IEnumerator StartWaveRoutine()
    {
        // Loop through all waves
        while (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];

            // Update the UI to reflect the current wave
            UpdateWaveUI();

            // Notify that the wave is starting
            UpdateStatusUI("Wave Starting!");

            // Spawn the wave and wait for it to complete
            yield return StartCoroutine(SpawnWave(currentWave));

            // Notify that the wave is over and rest is starting
            UpdateStatusUI("Wave Complete! Rest Time...");
            yield return new WaitForSeconds(5f); // 5-second rest time

            currentWaveIndex++;
        }

        Debug.Log("All waves completed!");
        UpdateWaveUI(); // Update UI to indicate waves are completed
        UpdateStatusUI("All Waves Completed!");
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        // Spawn all enemies for this wave
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(0.5f); // Delay between enemy spawns
        }

        // Wait until all enemies are destroyed
        while (activeEnemies.Count > 0)
        {
            // Clean up the active enemies list by removing any destroyed enemies
            activeEnemies.RemoveAll(enemy => enemy == null);
            yield return null;
        }

        isSpawning = false;
    }

    void SpawnEnemy(Wave wave)
    {
        // Randomly pick a spawn area from the wave's spawnAreas list
        if (wave.spawnAreas.Count == 0)
        {
            Debug.LogError("No spawn areas assigned for this wave.");
            return;
        }

        Transform spawnPoint = wave.spawnAreas[Random.Range(0, wave.spawnAreas.Count)];

        // Spawn the enemy and track it
        GameObject enemy = Instantiate(wave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        activeEnemies.Add(enemy);
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
        {
            if (currentWaveIndex < waves.Count)
            {
                waveText.text = $"Wave: {currentWaveIndex + 1}/{waves.Count}";
            }
            else
            {
                waveText.text = "All Waves Completed!";
            }
        }
    }

    void UpdateStatusUI(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }
}
