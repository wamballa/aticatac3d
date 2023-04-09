using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip playerHitClip;
    public AudioClip pickupClip;

    private Transform currentSpawnAreaTransform;
    private UImanager uiManager;
    private EventManager eventManager;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseComponents();
    }

    private void InitialiseComponents()
    {
        uiManager = GameObject.Find("UICanvas").GetComponent<UImanager>();
        audioSource = gameObject.GetComponent<AudioSource>();
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            print("Can picked up "+other.name);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(pickupClip);
            eventManager.onPickup.Invoke(100);
        }

        if (other.CompareTag("SpawnArea"))
        {
            // Destroy all enemies so new ones can spawn
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in enemies) Destroy(go);
            currentSpawnAreaTransform = other.transform;
        }


        if (other.CompareTag("GreenKey"))
        {
            transform.GetComponent<PlayerController>().SetHasGreenKey();
            if (uiManager.CanPickUp())
            {
                uiManager.AddItem("GreenKey");
                Destroy(other.gameObject);
            }
        }


        if (other.CompareTag("CyanKey"))
        {
            transform.GetComponent<PlayerController>().SetHasCyanKey();
            if (uiManager.CanPickUp())
            {
                uiManager.AddItem("CyanKey");
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Enemy"))
        {
            Destroy(other);
            HandleHeath();
        }
    }

    private void HandleHeath()
    {
        gameObject.GetComponent<LivesController>().ReduceHealth();
    }

    public Transform GetCurrentSpawnAreaTransform()
    {
        return currentSpawnAreaTransform;
    }
}
