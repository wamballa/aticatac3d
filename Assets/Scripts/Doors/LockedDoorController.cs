using System.Collections;
using UnityEngine;

public class LockedDoorController : DoorBase
{
    [Header("Locked Door variables")]
    private GameObject doorA;
    private Transform teleportLocationA;
    private GameObject doorB;
    private Transform teleportLocationB;
    private Transform teleportLocation;

    public bool isLockedDoor = true;
    public bool isDoorOpen = false;

    private void Awake()
    {
        base.Start();
        InitiateDoors();
    }

    void InitiateDoors()
    {
        doorA = transform.GetChild(0).gameObject;
        teleportLocationA = doorA.transform.GetChild(0);
        doorB = transform.GetChild(1).gameObject;
        teleportLocationB = doorB.transform.GetChild(0);
    }

    public void AttemptToUnlockDoor(Collider other, GameObject door)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        print("DOOR " + door.name);
        if (transform.CompareTag("GreenDoor") && playerController.GetHasGreenKey())
        {
            UnlockDoors(playerController, door);
        }
        else if (transform.CompareTag("CyanDoor") && playerController.GetHasCyanKey())
        {
            UnlockDoors(playerController, door);
        }
    }

    void UnlockDoors(PlayerController playerController, GameObject door)
    {

        doorA.transform.GetChild(1).gameObject.SetActive(false);
        doorA.transform.GetChild(2).gameObject.SetActive(true);
        doorB.transform.GetChild(1).gameObject.SetActive(false);
        doorB.transform.GetChild(2).gameObject.SetActive(true);

        isDoorOpen = true;

        PlayDoorSound();

        TeleportPlayer(playerController.GetComponent<Collider>(), door);
    }

    public void TeleportPlayer(Collider other, GameObject door)
    {
        if (door.name == "DoorA") teleportLocation = teleportLocationA;
        else if (door.name == "DoorB") teleportLocation = teleportLocationB;
        else print("ERROR: no where to teleport to");
        other.transform.position = teleportLocation.position;
        other.transform.rotation = teleportLocation.rotation;
        PlayTeleportClip();
    }
}
