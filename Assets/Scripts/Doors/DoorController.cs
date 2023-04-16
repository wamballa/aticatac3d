using System.Collections;
using UnityEngine;

public class DoorController : DoorBase
{
    [Header("Locked Door variables")]
    private GameObject doorA;
    private Transform teleportLocationA;
    private GameObject doorB;
    private Transform teleportLocationB;
    private Transform teleportLocation;
    private bool isLockedDoor;
    private float timerDelay = 2f;
    private GameObject playerPF;
    private bool isActive;
    //[HideInInspector] 
    public bool isDoorOpen = false;
    private float searchRadius = 10f;

    private void Awake()
    {
        InitiateDoors();
    }

    void Start()
    {
        playerPF = GameObject.FindGameObjectWithTag("Player");
        base.Start();
        StartCoroutine(HandleTimedDoors());

    }

    private void Update()
    {
        ActivateIfPlayerNear();
    }

    private void ActivateIfPlayerNear()
    {
        float distance = Vector3.Distance( transform.position, playerPF.transform.position);
        distance = Mathf.Abs(distance);
        if (distance <= searchRadius)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }

    void InitiateDoors()
    {
        doorA = transform.GetChild(0).gameObject;
        doorB = transform.GetChild(1).gameObject;

        teleportLocationA = doorA.transform.GetChild(0);
        teleportLocationB = doorB.transform.GetChild(0);

        SetDoorState(doorA, false);
        SetDoorState(doorB, false);

        timerDelay += Random.Range(1,5);
        //print(timerDelay);
    }

 

    IEnumerator HandleTimedDoors()
    {
        yield return new WaitForSeconds(timerDelay);
        if (isActive) ToggleDoor();
        StartCoroutine(HandleTimedDoors());
    }

    void ToggleDoor()
    {
        isDoorOpen = !isDoorOpen;
        SetDoorState(doorA, isDoorOpen);
        SetDoorState(doorB, isDoorOpen);
        PlayDoorSound();
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
        //Vector3 newPos = new Vector3(
        //   teleportLocation.position.x,
        //   -1.490116f,
        //   teleportLocation.position.z);
        //teleportLocation.position = newPos;
        other.transform.position = teleportLocation.position;
        other.transform.rotation = teleportLocation.rotation;
        PlayTeleportClip();
    }

}