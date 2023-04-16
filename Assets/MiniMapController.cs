using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    private Transform playerT;
    private GameObject[] ememiesT;
    public GameObject PlayerMiniMapPF;
    public GameObject EnemyMiniMapPF;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(  FindPlayerTransform());
        
    }

    private IEnumerator FindPlayerTransform()
    {
        while (playerT == null)
        {
            playerT = GameObject.FindGameObjectWithTag("Player").transform;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemies();
        UpdateEnemyDots();
        FollowPlayerPosition();

    }

    private void FindEnemies()
    {
        ememiesT = null;
        ememiesT = GameObject.FindGameObjectsWithTag("Enemy");

    }

    private void UpdateEnemyDots()
    {

    }

    private void FollowPlayerPosition()
    {
        Vector3 newPosition = playerT.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
