using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public EnemyDatabase enemyDatabase;
    public GameObject miniMapMarker;
    private string selectedEnemyName;

    private EventManager eventManager;
    //private EnemySelector enemySelector;
    //public EnemyType enemyType;
    //public string selectedEnemyName;

    private bool canMove;
    private SphereCollider sphereCollider;
    private ZXPalette palette;
    private Color targetColor;

    private GameObject enemyPrefab;
    private EnemyType selectedEnemy = null;

    void Start()
    {
        InitializeComponents();
        FindSelectedEnemyInDatabase();
        StartCoroutine(PrepareForSpawn());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            HandleDeath(other.gameObject);
        }
    }

    private void InitializeComponents()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        gameObject.AddComponent<LookAtPlayer>();
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<EnemyMovement>();
        gameObject.AddComponent<EnemyCollisions>();
        GameObject marker = Instantiate(miniMapMarker);
        marker.transform.position = transform.position;
        marker.transform.parent = transform;
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        if (sphereCollider == null) print("ERROR: no sphere collider created");
        sphereCollider.radius = 0.5f;
        sphereCollider.isTrigger = false;
        sphereCollider.enabled = false;
        //enemySelector = gameObject.GetComponent<EnemySelector>();
    }
    private void FindSelectedEnemyInDatabase()
    {
        selectedEnemyName = gameObject.name;
        // Find the selected enemy type in the enemy database
        foreach (EnemyType enemyType in enemyDatabase.enemyTypes)
        {
            if (enemyType.name == selectedEnemyName)
            {
                selectedEnemy = enemyType;
                break;
            }
        }

        if (selectedEnemy == null)
        {
            Debug.LogError("Selected enemy not found in database! "+selectedEnemyName);
            return;
        }
    }

    private IEnumerator PrepareForSpawn()
    {
        // Instantiate sparkle
        GameObject go = Instantiate(enemyDatabase.sparklePrefab, transform.position, Quaternion.identity);
        go.transform.parent = transform;
        SetRandomColour();
        canMove = false;

        sphereCollider.enabled = false;

        yield return new WaitForSeconds(3);

        Destroy(go);

        enemyPrefab = Instantiate(selectedEnemy.model, transform);
        //enemyPrefab.transform.parent = transform;
        SetRandomColour();
        sphereCollider.enabled = true;
        canMove = true;


    }


    private void SetRandomColour()
    {
        palette = ScriptableObject.CreateInstance<ZXPalette>();
        palette = ZXPalette.CreateInstance<ZXPalette>();
        targetColor = palette.GetZXColor();

        SpriteRenderer[] spriteRenders = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (SpriteRenderer m in spriteRenders)
        {
            m.material.color = targetColor;
        }
    }


    public bool GetCanMove()
    {
        return canMove;
    }

    public void HandleDeath(GameObject other)
    {
        eventManager.onEnemyDeath.Invoke(selectedEnemy.points);

        GameObject pop = Instantiate(
            enemyDatabase.popPF, 
            transform.position, 
            Quaternion.identity);
        Destroy(pop, 1f);
        Destroy(other);

        Destroy(gameObject);
    }


}
