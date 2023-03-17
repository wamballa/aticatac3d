using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{

    //GameObject[] items;
    List<GameObject> items;
    int numItems = 3;

    public GameObject cyanKeyImage;
    public GameObject greenKeyImage;
    public GameObject redKeyImage;
    public GameObject blueKeyImage;
    public GameObject bankKeyImage;

    // playercontroller to trigger drop pickup
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<GameObject>();
        // Add nulls to list
        for (int i=0; i < numItems; i++)
        {
            items.Add(null);
        }
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (playerController == null) print("ERROR: cannot find player controller");
    }

    // Update is called once per frame
    void Update()
    {
        /// DEBUG
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddItem("CyanKey");
        }
        // Print list
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("Print list "+items.Count);
            for (int i =0; i< numItems; i++)
            {
                print(i + " item = " + items[i]);
            }
        }


        /// DEBUG

        // Remove item
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RemoveItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RemoveItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RemoveItem(2);
        }
    }

    public void AddItem(string item)
    {
        // find first blank position
        int blankPosition = 0;

        for (int i=0; i< numItems; i++)
        {
            if (items[i] == null)
            {
                //print("Blank found at position " + i);
                blankPosition = i;
                break;
            }

        }
        // add the right key
        switch (item)
        {
            case "CyanKey":

                InsertAt(blankPosition, cyanKeyImage);
                //items.Insert(blankPosition, cyanKeyImage);
                break;
            case "GreenKey":
                InsertAt(blankPosition, greenKeyImage);
                break;
        }
        RefreshPanel();
    }

    void InsertAt(int i, GameObject go)
    {
        if (i>=0) items.RemoveAt(i);
        items.Insert(i, go);
    }

    public void RemoveItem(int i)
    {
        playerController.DropPickup(items[i].tag);
        items[i] = null;
        RefreshPanel();
    }

    void RefreshPanel()
    {
        // Remove All Children from UI panel
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            Destroy(child.gameObject);
        }
        // add back children from the inventory list
        foreach (GameObject go in items)
        {
            if (go == null)
            {
                Instantiate(bankKeyImage, transform);
            }
            else
            {
                Instantiate(go, transform);
            }
        }
    }

    public bool CanPickUp()
    {
        int numBlanks = 0;
        for (int i =0; i < numItems; i++)
        {
            if (items[i] == null) numBlanks++;
        }
        if (numBlanks > 0) return true; else return false;
    }

}
