using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : EnemyController
{

    void Start()
    {
        base.Start();
        StartCoroutine(ShowEnemy());
    }

    private void Update()
    {
        //CheckCanMove();
    }


}
