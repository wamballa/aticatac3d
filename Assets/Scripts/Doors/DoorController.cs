using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Locked Door variables")]
    private GameObject doorA;
    private Transform teleportLocationA;
    private GameObject doorB;
    private Transform teleportLocationB;
    private Transform teleportLocation;
    private bool isLockedDoor;
    private float timerDelay = 2f;
    //[HideInInspector] 
    public bool isDoorOpen = false;

    private void Awake()
    {
        InitiateDoors();
    }

    void Start()
    {
        StartCoroutine(HandleTimedDoors());
    }

    IEnumerator HandleTimedDoors()
    {
        yield return new WaitForSeconds(timerDelay);
        ToggleDoor();
        StartCoroutine(HandleTimedDoors());
    }

    void ToggleDoor()
    {
        isDoorOpen = !isDoorOpen;
        SetDoorState(doorA, isDoorOpen);
        SetDoorState(doorB, isDoorOpen);
    }

    void InitiateDoors()
    {
        doorA = transform.GetChild(0).gameObject;
        doorB = transform.GetChild(1).gameObject;

        teleportLocationA = doorA.transform.GetChild(0);
        teleportLocationB = doorB.transform.GetChild(0);

        SetDoorState(doorA, false);
        SetDoorState(doorB, false);
    }

    void SetDoorState(GameObject door, bool isOpen)
    {
        door.transform.GetChild(1).gameObject.SetActive(!isOpen);
        door.transform.GetChild(2).gameObject.SetActive(isOpen);
    }

    public void TeleportPlayer(Collider other, GameObject door)
    {
        if (door.name == "DoorA") teleportLocation = teleportLocationA;
        else if (door.name == "DoorB") teleportLocation = teleportLocationB;
        else print("ERROR: no where to teleport to");
        other.transform.position = teleportLocation.position;
        other.transform.rotation = teleportLocation.rotation;
    }

}