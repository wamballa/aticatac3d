using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    
    private Transform currentSpawnAreaTransform;
    private UImanager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UICanvas").GetComponent<UImanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
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

        //if (other.CompareTag("GreenDoor"))
        //{
        //    print("Triggered Green Door");
        //    if (transform.GetComponent<PlayerController>().GetHasGreenKey())
        //    {
        //        //
        //    }
        //}
    }

    public Transform GetCurrentSpawnAreaTransform()
    {
        return currentSpawnAreaTransform;
    }
}
