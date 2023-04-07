using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorColliderController : MonoBehaviour
{
    private LockedDoorController parentController;

    private void Start()
    {
        parentController = gameObject.transform.parent.GetComponent<LockedDoorController>();
        if (parentController == null) print("ERROR");
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (parentController.isDoorOpen)
            {
                parentController.TeleportPlayer(other, gameObject);
            }
            else if (parentController.isLockedDoor)
            {
                parentController.AttemptToUnlockDoor(other, gameObject);
            }
        }
    }
}
