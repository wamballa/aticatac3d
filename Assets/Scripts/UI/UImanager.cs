using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    private EventManager eventManager;
    private List<GameObject> items;
    private const int numItems = 3;
    ZXPalette palette;

    [Header("Key panel")]
    public GameObject cyanKeyImage;
    public GameObject greenKeyImage;
    public GameObject redKeyImage;
    public GameObject blueKeyImage;
    public GameObject crucifixImage;
    public GameObject leafImage;
    public GameObject blankImage;
    public Transform keyPanel;
    [Header("Lives panel")]
    public GameObject livesPanel;
    [Header("UI colours")]
    public Image borderImage;
    public Image roseImage;

    void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        InitializeItemsList();
        InitializeUIColours();
        ChangeUIColours();
    }

    public void ChangeUIColours()
    {
        borderImage.color = palette.GetZXColor();
        roseImage.color = palette.GetZXColor();
    }

    private void InitializeUIColours()
    {
        palette = ZXPalette.CreateInstance<ZXPalette>();
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
            case "RedKey":
                InsertAt(blankPosition, redKeyImage);
                break;
            case "Crucifix":
                InsertAt(blankPosition, crucifixImage);
                break;
            case "Leaf":
                InsertAt(blankPosition, leafImage);
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
        print(items[i].tag);
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
            Instantiate(go == null ? blankImage : go, keyPanel);
        }
    }

    private void RemoveAllChildren()
    {
        //print("REMOVE ALL CHLDREN # " + (keyPanel.childCount - 1));
        for (int i = keyPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(keyPanel.GetChild(i).gameObject);
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
        if (Input.GetKeyUp(KeyCode.C))
        {
            SetObjectColour[] foundScript = FindObjectsOfType<SetObjectColour>();
            print("EnemyC # " + foundScript.Length);
            foreach (SetObjectColour s in foundScript)
            {
                print("== " + s.gameObject.name);
                print("==== " + s.gameObject.transform.parent.name);
            }
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
