using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Key states
    private bool hasGreenKey;
    private bool hasCyanKey;

    // Event listener
    private EventManager eventManager;

    // Pickups
    public GameObject[] pickUps;
    public float maxLength = 10f; // Set the maximum ray length in the Inspector
    public LayerMask layerMask; // Set the layer mask in the Inspector

    private void Start()
    {
    }

    private void OnEnable()
    {
        InitializeEventManager();
    }

    private void OnDisable()
    {
        eventManager.onDropPickup.RemoveListener(DropPickup);
    }

    private void InitializeEventManager()
    {
        if (eventManager == null)
        {
            eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
            if (eventManager == null) print("ERROR");
            eventManager.onDropPickup.AddListener(DropPickup);
        }
    }

    // Green Key
    public void SetHasGreenKey() => hasGreenKey = true;
    public bool GetHasGreenKey() => hasGreenKey;

    // Cyan Key
    public void SetHasCyanKey() => hasCyanKey = true;
    public bool GetHasCyanKey() => hasCyanKey;

    // Drop Pickup
    public void DropPickup(string pickupName)
    {
        foreach (GameObject go in pickUps)
        {
            if (go.CompareTag(pickupName))
            {
                UpdateKeyState(pickupName, false);
                InstantiatePickup(go);
            }
        }
    }

    private void UpdateKeyState(string pickupName, bool state)
    {
        switch (pickupName)
        {
            case "CyanKey":
                hasCyanKey = state;
                break;
            case "GreenKey":
                hasGreenKey = state;
                break;
        }
    }

    private void InstantiatePickup(GameObject go)
    {
        Vector3 newPos = transform.position + (transform.forward * 2);
        newPos.y = newPos.y + 0.2f;
        Instantiate(go, newPos, Quaternion.identity);
    }


}

