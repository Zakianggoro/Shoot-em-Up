using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject enemyPrefab;
        public float spawnTime; 
    }

    public SpawnEntry[] spawnQueue; 
    public Transform[] spawnPoints; 

    void Start()
    {
        foreach (var entry in spawnQueue)
        {
            StartCoroutine(SpawnEnemyAtTime(entry));
        }
    }

    private System.Collections.IEnumerator SpawnEnemyAtTime(SpawnEntry entry)
    {
        yield return new WaitForSeconds(entry.spawnTime);

        SpawnEnemy(entry.enemyPrefab);
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
