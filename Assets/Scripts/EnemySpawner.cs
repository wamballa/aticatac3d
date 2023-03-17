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
    //private bool isWaitingForNewPosition = false;

    // Timer

    private float timer;
    private int currentEnemies;

    private float spawnHeight = -0.5f;



    // Start is called before the first frame update
    void Start()
    {
        timer = spawnInterval;
        currentEnemies = 0;
        spawnAreaSize = spawnAreaTransform.localScale;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        spawnAreas = GameObject.FindGameObjectsWithTag("SpawnArea");
    }

    // Update is called once per frame
    void Update()
    {
        if (prefabsToSpawn.Length == 0) return;
        currentEnemies = CountEnemies();


        spawnAreaTransform = GetCurrentSpawnAreaTransform();
        spawnAreaSize = spawnAreaTransform.localScale;

        if (currentEnemies < maxEnemies)
        {
            timer -= Time.deltaTime;
            Vector3 spawnPosition;
            if (timer <= 0f)
            {
                do
                {
                    spawnPosition = spawnAreaTransform.position + new Vector3(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f), spawnHeight, Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f));
                } while (Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer);

                GameObject spawnPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
                Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
                //currentEnemies++;

                //print("Num Ememies = " + currentEnemies);
                timer = spawnInterval;
            }
        }
    }

    Transform GetCurrentSpawnAreaTransform()
    {
        return playerTransform.GetComponent<PlayerCollisions>().GetCurrentSpawnAreaTransform();
    }
    


    int CountEnemies()
    {
        int n = GameObject.FindGameObjectsWithTag("Enemy").Length;
        return n;
    }
}
