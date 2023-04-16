using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNearDoors : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
