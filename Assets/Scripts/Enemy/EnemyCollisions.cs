using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private Enemy_Controller enemyController;
    
    // Start is called before the first frame update
    void Start()
    {
        InitialiseComponents();
    }

    private void InitialiseComponents()
    {
        //print("ENEMYCOLLISSON INITIALISED");
        enemyMovement = transform.GetComponent<EnemyMovement>();
        if (enemyMovement == null) Debug.LogError("ERROR: no enemymovement found!");
        enemyController = transform.GetComponent<Enemy_Controller>();
        if (enemyController == null) Debug.LogError("ERROR: no enemy controller found)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print(gameObject.name + " collided name: "+collision.transform.name+ " collided tag "+collision.transform.tag);
        switch (collision.transform.tag)
        {
            case "Environment":
                //print(gameObject.name + " hit wall");
                enemyMovement.FlipObject();
                break;
            case "Weapon":
                //print("Enemy hit weapon");
                if (transform.CompareTag("Enemy")) enemyController.HandleDeath(collision.gameObject);
                if (transform.CompareTag("Dracula")) Destroy(collision.gameObject);
                break;
        }

        //if (collision.transform.CompareTag("Environment"))
        //{
        //    print(gameObject.name + " hit wall");
        //    enemyMovement.FlipObject();
        //}


    }


}
