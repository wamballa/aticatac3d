using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool hasGreenKey;
    bool hasCyanKey;

    // pickups
    public GameObject[] pickUps;

    void Start()
    {
        
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
