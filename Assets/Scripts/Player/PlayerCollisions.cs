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

    private void OnCollisionEnter(Collision collision)
    {

        switch (collision.transform.tag)
        {
            case ("Enemy"):
            case ("Dracula"):
            case ("Mummy"):
                //print("@Player Collision " + collision.gameObject.name + " " + collision.transform.tag);
                Destroy(collision.gameObject);
                HandleCollisionWithEnemy();
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PickUp":
                HandleItemPickup(other);
                break;
            case "SpawnArea":
                HandleSpawnAreaEnter(other);
                break;
            case "GreenKey":
            case "CyanKey":
            case "RedKey":
            case "Crucifix":
            case "Leaf":
                HandlePickup(other);
                break;
        }
    }

    private void HandleItemPickup(Collider other)
    {
        print("Can picked up " + other.name);
        Destroy(other.gameObject);
        audioSource.PlayOneShot(pickupClip);
        eventManager.onPickup.Invoke(100);
    }

    private void HandleSpawnAreaEnter(Collider other)
    {
        print("Player entered TRIGGER " + other.name);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in enemies) Destroy(go);
        currentSpawnAreaTransform = other.transform;
    }

    private void HandlePickup(Collider other)
    {
        string pickupTag = other.tag;
        transform.GetComponent<PlayerController>().SetHasPickup(pickupTag);

        if (uiManager.CanPickUp())
        {
            uiManager.AddItem(pickupTag);
            Destroy(other.gameObject);
        }
    }



    private void HandleCollisionWithEnemy()
    {
        gameObject.GetComponent<LivesController>().ReduceHealth();
    }

    public Transform GetCurrentSpawnAreaTransform()
    {
        return currentSpawnAreaTransform;
    }
}
