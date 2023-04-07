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

    void SpawnPlayer()
    {
        if (playerT != null && playerSpawnT != null)
        {
            playerGO = Instantiate(playerT.gameObject, playerSpawnT.position, playerSpawnT.rotation);
            FindAndSetupPlayerCamera();
        }
        else
        {
            Debug.LogError("Player or Player Spawn Point is not assigned.");
        }
    }

    void FindAndSetupPlayerCamera()
    {
        if (playerGO != null)
        {
            playerCamera = playerGO.GetComponentInChildren<Camera>();
            if (playerCamera != null)
            {
                // Set the resolution to 480x480
                Screen.SetResolution(480, 480, false);
                // Set the camera to only render the top-left quadrant of the screen
                playerCamera.rect = new Rect(0, 0, 1f, 1f);
            }
            else
            {
                Debug.LogError("Cannot find camera in player game object.");
            }
        }
        else
        {
            Debug.LogError("No Player instantiated.");
        }
    }
}
