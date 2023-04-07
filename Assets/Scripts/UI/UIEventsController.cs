using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEventsController : MonoBehaviour
{
    private EventManager eventManager;
    public TMP_Text scoreText;

    private void OnEnable()
    {
        InitializeEventManager();
        SetupListeners();
    }

    private void SetupListeners()
    {
        eventManager.onAddScore.AddListener(AddScore);
    }

    private void InitializeEventManager()
    {
        if (eventManager == null)
        {
            eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
            if (eventManager == null) print("ERROR");
            
        }
    }


    void AddScore(int i)
    {
        scoreText.text = i.ToString("D6");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
