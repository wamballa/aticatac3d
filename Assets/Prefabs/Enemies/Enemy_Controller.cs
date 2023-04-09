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

    //public int selectedEnemyIndex;

    void Start()
    {
        InitializeComponents();
        FindSelectedEnemyInDatabase();
        StartCoroutine(PrepareForSpawn());
    }

    private void FindSelectedEnemyInDatabase()
    {
        // Find the selected enemy type in the enemy database
        
        foreach (EnemyType enemyType in enemyDatabase.enemyTypes)
        {
            if (enemyType.name == enemySelector.selectedEnemyName)
            {
                selectedEnemy = enemyType;
                print("Selected Enemy = " + selectedEnemy.name);
                break;
            }
        }

        if (selectedEnemy == null)
        {
            Debug.LogError("Selected enemy not found in database!");
            return;
        }


    }

    private void InitializeComponents()
    {
        gameObject.AddComponent<LookAtPlayer>();
        //gameObject.AddComponent<SetObjectColour>();
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 3f;

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
    private IEnumerator PrepareForSpawn()
    {
        GameObject sparkle = Instantiate(enemyDatabase.sparklePrefab, transform);
        SetRandomColour();
        canMove = false;

        //if (animator != null)
        //{
        //    animator.enabled = false;
        //}

        sphereCollider.enabled = false;

        yield return new WaitForSeconds(3);
        Destroy(sparkle);
        enemyPrefab = Instantiate(selectedEnemy.model, transform.position, Quaternion.identity);
        enemyPrefab.transform.parent = transform;
        SetRandomColour();


    }

 



    // Update is called once per frame
    void Update()
    {
        
    }
}
