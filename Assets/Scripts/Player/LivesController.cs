using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LivesController : MonoBehaviour
{

    public List<GameObject> healthBars;
    public GameObject tombStonePF;
    public GameObject livesPrefab;

    public GameObject livesPanelPrefab;

    private int lives = 3;
    private int maxLives = 3;
    private int currentHealth = 4;
    private int maxHealth = 5;
    private EventManager eventManager;

    private void Start()
    {
        InitialiseComponents();
        InitialiseEventListeners();
    }



    private void Update()
    {
        // Debug
        if (Input.GetKeyUp(KeyCode.H))
        {
            ReduceLives();
        }
    }

    private void InitialiseComponents()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        healthBars = new List<GameObject>();
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject hBar = GameObject.Find(i.ToString());
            if (hBar == null) print("ERROR: finding health bar  " + i);
            healthBars.Add(hBar);
        }
        livesPanelPrefab = GameObject.Find("Lives Panel");
        UpdateLivesUI();
    }

    private void InitialiseEventListeners()
    {
        eventManager.onPickup.AddListener(Pickup);
    }

    private void Pickup(int arg0)
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void ReduceHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            print("Health = " + currentHealth);
            UpdateHealthUI();
        }
        else
        {
            ReduceLives();
        }

    }


    private void UpdateHealthUI()
    {
        for (int i=1; i< maxHealth; i++)
        {
            if (i > currentHealth)
            {
                SetVisible(healthBars[i], false);
            }
            else
            {
                SetVisible(healthBars[i], true);
            }
        }

    }

    private void SetVisible(GameObject healthBar, bool isVisible)
    {
        healthBar.SetActive(isVisible);
    }



    private void ReduceLives()
    {
        if (lives > 0)
        {
            ResetChickenUI();
            lives--;
            currentHealth = maxHealth;
            UpdateLivesUI();
            Instantiate(tombStonePF, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
            gameObject.GetComponent<PlayerController>().SpawnPlayer();
            print("Lives =  " + lives);
        }
        else
        {
            SceneManager.LoadScene(2);
        }

    }

    private void UpdateLivesUI()
    {
        foreach(Transform child in livesPanelPrefab.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < lives; i++)
        {
            Instantiate(livesPrefab, livesPanelPrefab.transform);
        }
    }

    private void ResetChickenUI()
    {
        foreach (GameObject g in healthBars)
        {
            g.SetActive(true);
        }
    }
}
