using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorController : MonoBehaviour
{
    public GameObject openDoor;
    public GameObject closedDoor;
    public Transform newLocation;

    public bool isTimedDoor;
    public bool isLockedDoor;
    public GameObject pairedDoorPF;


    float timerDelay = 2f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isDoorOpen)
            {
                // Play Sound
                other.transform.position = newLocation.position;
                other.transform.rotation = newLocation.rotation;
            }
            if (isLockedDoor)
            {

                if (transform.CompareTag("GreenDoor"))
                {
                    if (other.transform.GetComponent<PlayerController>().GetHasGreenKey())
                    {
                        print("Open Green Door");

                        pairedDoorPF.GetComponent<NormalDoorController>().openPairedDoor();
                        isLockedDoor = false;
                        isDoorOpen = true;

                        // change prefab
                        openDoor.SetActive(true);
                        closedDoor.SetActive(false);

                        other.transform.position = newLocation.position;
                        other.transform.rotation = newLocation.rotation;
                    }
                }
                if (transform.CompareTag("CyanDoor"))
                {
                    if (other.transform.GetComponent<PlayerController>().GetHasCyanKey())
                    {

                        pairedDoorPF.GetComponent<NormalDoorController>().openPairedDoor();
                        isLockedDoor = false;
                        isDoorOpen = true;

                        // change prefab
                        openDoor.SetActive(true);
                        closedDoor.SetActive(false);

                        other.transform.position = newLocation.position;
                        other.transform.rotation = newLocation.rotation;
                    }
                }

            }

        }
    }


    public void openPairedDoor()
    {
        //if (isLockedDoor)
        //{
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
        isDoorOpen = true;
        isLockedDoor = false;
        //}
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
    }
}
