using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScreenController : MonoBehaviour
{
    private FirstPersonMovement firstPersonMovement;

    private void Start()
    {
        firstPersonMovement = transform.GetComponentInParent<FirstPersonMovement>();
        if (firstPersonMovement == null) Debug.LogError("ERROR: cannot find script!");
    }

    public void CanMove()
    {
        firstPersonMovement.CanMoveTrue();
    }

    public void CannotMove()
    {
        firstPersonMovement.CanMoveFalse();
    }
}
