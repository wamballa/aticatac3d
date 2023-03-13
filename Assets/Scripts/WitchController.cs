using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : EnemyController
{
    void Start()
    {
        base.Start();
        StartCoroutine(ShowEnemy());
    }
}
