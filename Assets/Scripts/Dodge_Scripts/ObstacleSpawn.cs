using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float initialSpawnInterval = 2f;
    public float spawnIntervalDecreaseRate = 0.1f;
    public float minSpawnInterval = 0.5f;
    public float minX = -5f;
    public float maxX = 5f;

    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        InvokeRepeating("SpawnObstacle", 1f, currentSpawnInterval);
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, 0, 10);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        IncreaseSpawnRate();
    }

    void IncreaseSpawnRate()
    {
        CancelInvoke("SpawnObstacle");
        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalDecreaseRate);
        InvokeRepeating("SpawnObstacle", currentSpawnInterval, currentSpawnInterval);
    }
}
