using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy details")]
    public GameObject[] prefabsToSpawn;
    //public GameObject[] spawnAreas;
    public Transform spawnAreaTransform;
    public int maxEnemies = 1;
    public float spawnInterval = 1f;
    public float minDistanceFromPlayer = 2f;
    public float spawnHeight = -1f;

    private Vector3 spawnAreaSize;
    private Transform playerTransform;
    private float timer;
    private int currentEnemies;


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
        //spawnAreas = GameObject.FindGameObjectsWithTag("SpawnArea");
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
            int invalidSpawnCount = 0;
            do
            {
                spawnPosition = GetRandomSpawnPosition();
                if (Vector3.Distance(spawnPosition, playerTransform.position) >= minDistanceFromPlayer)
                {
                    invalidSpawnCount = 0;
                    break;
                }
                invalidSpawnCount++;
            } while (invalidSpawnCount < 10);

            if (invalidSpawnCount < 10)
            {
                int random = Random.Range(0, prefabsToSpawn.Length);
                GameObject randomPF = prefabsToSpawn[random];
                GameObject spawnPrefab = Instantiate(randomPF, spawnPosition, Quaternion.identity);
                spawnPrefab.name = randomPF.name;
                timer = spawnInterval;
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        //print("Spawnarea size = " + spawnAreaSize);
        float offset = 0.1f;
        Vector3 spawnPos = spawnAreaTransform.position + new Vector3(
            Random.Range((-spawnAreaSize.x / 2f)+ offset, (spawnAreaSize.x / 2f)- offset),
            spawnHeight,
            Random.Range((-spawnAreaSize.z / 2f)+ offset, (spawnAreaSize.z / 2f)- offset)
        );
        spawnPos.y = spawnHeight;
        return spawnPos;
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
