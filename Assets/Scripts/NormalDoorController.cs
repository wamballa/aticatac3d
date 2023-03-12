using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorController : MonoBehaviour
{
    public GameObject openDoor;
    public GameObject closedDoor;
    public bool isTimedDoor;

    float timerDelay = 8f;
    bool isDoorOpen = false;

    private void Awake()
    {
        InitiateDoors();
    }

    void Start()
    {

        if (isTimedDoor)
        {
            StartCoroutine(HandleTimedDoors());
        }
    }

    IEnumerator HandleTimedDoors()
    {
        yield return new WaitForSeconds(timerDelay);
        ToggleDoor();
        StartCoroutine(HandleTimedDoors());
    }


    void ToggleDoor()
    {
        if (isDoorOpen)
        {
            openDoor.SetActive(false);
            closedDoor.SetActive(true);
            isDoorOpen = false;
        }
        else
        {
            openDoor.SetActive(true);
            closedDoor.SetActive(false);
            isDoorOpen = true;
        }
    }


    void InitiateDoors()
    {
        if (isTimedDoor)
        {
            openDoor.SetActive(false);
            closedDoor.SetActive(true);
        }
        else
        {
            openDoor.SetActive(true);
            closedDoor.SetActive(false);
        }

    }


}
