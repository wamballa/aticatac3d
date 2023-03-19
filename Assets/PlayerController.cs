using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool hasGreenKey;
    bool hasCyanKey;

    // Event listener
    public EventManager eventManager;

    // pickups
    public GameObject[] pickUps;

    void Start()
    {
        //eventManager.onDropPickup.AddListener(DropPickup);
    }

    private void OnEnable()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        eventManager.onDropPickup.AddListener(DropPickup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHasGreenKey()
    {
        hasGreenKey = true;
    }

    public bool GetHasGreenKey()
    {
        return hasGreenKey;
    }

    public void SetHasCyanKey()
    {
        hasCyanKey = true;
    }
    public bool GetHasCyanKey()
    {
        return hasCyanKey;
    }

    public void DropPickup(string pickupName)
    {
        foreach (GameObject go in pickUps)
        {
            if (go.CompareTag(pickupName))
            {
                switch (pickupName)
                {
                    case "CyanKey":
                        hasCyanKey = false;
                        break;
                    case "GreenKey":
                        hasGreenKey = false;
                        break;

                }
                Vector3 newPos = transform.position + (transform.forward*2);
                newPos.y = 0.85f;
                Instantiate(go, newPos, Quaternion.identity);
            }
        }
    }

}
