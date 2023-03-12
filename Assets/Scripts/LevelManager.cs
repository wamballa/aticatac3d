using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public Transform playerT;
    public Transform playerSpawnT;
    private Camera playerCamera;
    private GameObject playerGO;
    private void Awake()
    {
        SpawnPlayer();
        //SetupCamera();


    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // METHODS 

    void SpawnPlayer()
    {
        playerGO = Instantiate(playerT.gameObject);
        playerT.position = playerSpawnT.position;
        playerT.rotation = playerSpawnT.rotation;
    }

    void SetupCamera()
    {
        if (playerGO)
        {
            playerT.position = playerSpawnT.position;
            playerT.rotation = playerSpawnT.rotation;

            foreach (Transform t in playerT)
            {
                if (t.CompareTag("MainCamera"))
                {
                    playerCamera = t.GetComponent<Camera>();
                    break;
                }
            }
            if (playerCamera == null) print("ERROR: cannot find camera");

            // Set the resolution to 480x480
            Screen.SetResolution(480, 480, false);
            // Set the camera to only render the top-left quadrant of the screen
            playerCamera.rect = new Rect(0, 0, 1f, 1f);
        }
        else
        {
            print("ERROR: No Player Instantiated");
        }
    }

}
