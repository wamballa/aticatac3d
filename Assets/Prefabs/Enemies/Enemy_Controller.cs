using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private EventManager eventManager;
    public EnemyDatabase enemyDatabase;
    public EnemySelector enemySelector;
    //public EnemyType enemyType;
    //public string selectedEnemyName;

    private bool canMove;
    private SphereCollider sphereCollider;
    private ZXPalette palette;
    private Color targetColor;

    private GameObject enemyPrefab;
    EnemyType selectedEnemy = null;

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
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 0.5f;
        sphereCollider.isTrigger = true;
        sphereCollider.enabled = false;

    }
    private void FindSelectedEnemyInDatabase()
    {
        // Find the selected enemy type in the enemy database
        int i = 0;
        foreach (EnemyType enemyType in enemyDatabase.enemyTypes)
        {
            if (enemyType.name == enemySelector.selectedEnemyName)
            {
                selectedEnemy = enemyType;
                //print("Selected Enemy = " + selectedEnemy.name + " " + i);
                break;
            }
            i++;
        }

        if (selectedEnemy == null)
        {
            Debug.LogError("Selected enemy not found in database!");
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

    private void HandleDeath(GameObject other)
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
