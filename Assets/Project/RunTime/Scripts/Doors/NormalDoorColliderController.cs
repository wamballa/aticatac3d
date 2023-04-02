using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorColliderController : MonoBehaviour
{
    private DoorController parentController;

    private void Start()
    {
        parentController = gameObject.transform.parent.GetComponent<DoorController>();
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
        }
    }
}
