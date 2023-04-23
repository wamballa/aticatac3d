using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Pickup states
    private bool hasGreenKey;
    private bool hasCyanKey;
    private bool hasCrucifix;
    private bool hasRedKey;
    private bool hasLeaf;

    // Event listener
    private EventManager eventManager;
    private int score;

    // Pickups
    [Header("Pickups")]
    public GameObject[] pickUps;
    public float maxLength = 10f; // Set the maximum ray length in the Inspector
    public LayerMask layerMask; // Set the layer mask in the Inspector

    [Header("Sounds")]
    public AudioClip playerSpawnClip;

    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        print("Spawn Player");
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null) print("ERROR: player has no audiosource");
        audioSource.PlayOneShot(playerSpawnClip);
        
        Animator animUI = GameObject.Find("Scroll").GetComponent<Animator>();
        animUI.Play("Scroll Animation", -1, 0);
        Animator anim = gameObject.GetComponentInChildren<Animator>();

        Animator animMiniMap = GameObject.Find("MiniMap").GetComponent<Animator>();
        animMiniMap.Play("MiniMap Animation", -1, 0);
        Animator animatorMiniMap = gameObject.GetComponentInChildren<Animator>();

        anim.Play("PlayerSpawn Animation", -1, 0);

        //anim.StartPlayback();
        //anim.SetBool("canPlay", true);
        //anim.SetBool("canPlay", false);
        //anim.SetBool("canPlay", true);
    }

    private void OnEnable()
    {
        InitializeEventManager();
    }

    private void OnDisable()
    {
        eventManager.onDropPickup.RemoveListener(DropPickup);
        eventManager.onEnemyDeath.RemoveListener(EnemyDeath);
    }

    private void Update()
    {
        //Debug.DrawLine(transform.position, transform.forward * 3, Color.green, 1);
    }


    private void InitializeEventManager()
    {
        if (eventManager == null)
        {
            eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
            if (eventManager == null) print("ERROR");

            eventManager.onDropPickup.AddListener(DropPickup);
            eventManager.onEnemyDeath.AddListener(EnemyDeath);
            eventManager.onPickup.AddListener(Pickup);
        }
    }

    private void EnemyDeath(int points)
    {
        score += points;
        eventManager.onAddScore.Invoke(score);
        PlayerPrefs.SetInt("Score", score);
    }

    private void Pickup(int points)
    {
        score += points;
        eventManager.onAddScore.Invoke(score);
        PlayerPrefs.SetInt("Score", score);
    }

    public void SetHasPickup(string keyType)
    {
        switch (keyType)
        {
            case "GreenKey":
                hasGreenKey = true;
                break;
            case "CyanKey":
                hasCyanKey = true;
                break;
            case "RedKey":
                hasRedKey = true;
                break;
            case "Crucifix":
                hasCrucifix = true;
                break;
            case "Leaf":
                hasLeaf = true;
                break;
        }
    }

    public bool GetHasPickup(string keyType)
    {
        switch (keyType)
        {
            case "GreenKey":
                return hasGreenKey;
            case "CyanKey":
                return hasCyanKey;
            case "RedKey":
                return hasRedKey;
            case "Crucifix":
                return hasCrucifix;
            case "Leaf":
                return hasLeaf;
            default:
                return false;
        }
    }



    // Drop Pickup
    public void DropPickup(string pickupName)
    {
        print("DropPick - " + pickupName);
        foreach (GameObject go in pickUps)
        {
            if (go.CompareTag(pickupName))
            {
                print("DropPick FOUND - " + go.name);

                UpdatePickupState(pickupName, false);
                InstantiatePickup(go);
            }
        }
    }

    private void UpdatePickupState(string pickupName, bool state)
    {
        switch (pickupName)
        {
            case "CyanKey":
                hasCyanKey = state;
                break;
            case "GreenKey":
                hasGreenKey = state;
                break;
            case "RedKey":
                hasRedKey = state;
                break;
            case "Crucifix":
                hasCrucifix = state;
                break;
            case "Leaf":
                hasLeaf = state;
                break;
        }
    }

    private void InstantiatePickup(GameObject go)
    {
        Vector3 newPos = transform.position + (transform.forward);
        //newPos.y = newPos.y + 0.1f;
        Instantiate(go, newPos, Quaternion.identity);
    }


}

