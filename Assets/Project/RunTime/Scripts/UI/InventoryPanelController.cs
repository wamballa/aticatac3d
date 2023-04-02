using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryPanelController : MonoBehaviour
{
    private EventManager eventManager;
    private List<GameObject> items;
    private const int numItems = 3;

    public GameObject cyanKeyImage;
    public GameObject greenKeyImage;
    public GameObject redKeyImage;
    public GameObject blueKeyImage;
    public GameObject bankKeyImage;

    void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        InitializeItemsList();
    }

    void Update()
    {
        HandleInputs();
    }

    public void AddItem(string item)
    {
        int blankPosition = FindBlankPosition();
        if (blankPosition == -1) return;
        switch (item)
        {
            case "CyanKey":
                InsertAt(blankPosition, cyanKeyImage);
                break;
            case "GreenKey":
                InsertAt(blankPosition, greenKeyImage);
                break;
            default:
                print("ERROR: unknown item called - " + item);
                break;
        }
        RefreshPanel();
    }

    public void RemoveItem(int i)
    {
        if (items[i] == null) return;
        eventManager.onDropPickup.Invoke(items[i].tag);
        items[i] = null;
        RefreshPanel();
    }

    public bool CanPickUp()
    {
        int numBlanks = items.FindAll(item => item == null).Count;
        return numBlanks > 0;
    }

    private void InitializeItemsList()
    {
        items = new List<GameObject>(new GameObject[numItems]);
    }

    private int FindBlankPosition()
    {
        return items.IndexOf(null);
    }

    private void InsertAt(int i, GameObject go)
    {
        items[i] = go;
    }

    private void RefreshPanel()
    {
        RemoveAllChildren();
        foreach (GameObject go in items)
        {
            Instantiate(go == null ? bankKeyImage : go, transform);
        }
    }

    private void RemoveAllChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddItem("CyanKey");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintItemsList();
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            RemoveItem(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            RemoveItem(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            RemoveItem(2);
        }
    }

    private void PrintItemsList()
    {
        Debug.Log("Print list " + items.Count);
        for (int i = 0; i < numItems; i++)
        {
            Debug.Log(i + " item = " + items[i]);
        }
    }
}
