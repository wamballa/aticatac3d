using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    private Transform currentSpawnAreaTransform;
    private InventoryPanelController inventoryPanelController;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanelController = GameObject.Find("InventoryPanel").GetComponent<InventoryPanelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnArea"))
        {
            //print("Player enterered spawn area " + other.name);
            // Destroy all enemies so new ones can spawn
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in enemies) Destroy(go); 
            currentSpawnAreaTransform = other.transform;
        }


        if (other.CompareTag("GreenKey"))
        {
            transform.GetComponent<PlayerController>().SetHasGreenKey();
            inventoryPanelController.AddItem("GreenKey");
            Destroy(other.gameObject);
        }


        if (other.CompareTag("CyanKey"))
        {
            transform.GetComponent<PlayerController>().SetHasCyanKey();
            if (inventoryPanelController.CanPickUp()) inventoryPanelController.AddItem("CyanKey"); else print("Inventory Full");
            Destroy(other.gameObject);
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
