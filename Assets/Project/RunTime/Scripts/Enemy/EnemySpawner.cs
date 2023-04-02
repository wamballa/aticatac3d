using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public GameObject[] spawnAreas;
    public Transform spawnAreaTransform;
    public int maxEnemies = 1;
    public float spawnInterval = 1f;
    public float minDistanceFromPlayer = 3f;

    private Vector3 spawnAreaSize;
    private Transform playerTransform;
    private float timer;
    private int currentEnemies;
    private float spawnHeight = -0.5f;

    void Start()
    {
        InitializeSpawner();
    }

    void Update()
    {
        if (prefabsToSpawn.Length == 0) return;

        UpdateSpawnAreaTransform();
        UpdateCurrentEnemiesCount();

        if (currentEnemies < maxEnemies)
        {
            TrySpawnEnemy();
        }
    }

    void InitializeSpawner()
    {
        timer = spawnInterval;
        currentEnemies = 0;
        spawnAreaSize = spawnAreaTransform.localScale;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spawnAreas = GameObject.FindGameObjectsWithTag("SpawnArea");
    }

    void UpdateSpawnAreaTransform()
    {
        spawnAreaTransform = GetCurrentSpawnAreaTransform();
        spawnAreaSize = spawnAreaTransform.localScale;
    }

    void UpdateCurrentEnemiesCount()
    {
        currentEnemies = CountEnemies();
    }

    void TrySpawnEnemy()
    {
        timer -= Time.deltaTime;
        Vector3 spawnPosition;

        if (timer <= 0f)
        {
            do
            {
                spawnPosition = GetRandomSpawnPosition();
            } while (Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer);

            GameObject spawnPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
            Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
            timer = spawnInterval;
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        return spawnAreaTransform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            spawnHeight,
            Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
        );
    }

    Transform GetCurrentSpawnAreaTransform()
    {
        return playerTransform.GetComponent<PlayerCollisions>().GetCurrentSpawnAreaTransform();
    }

    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
