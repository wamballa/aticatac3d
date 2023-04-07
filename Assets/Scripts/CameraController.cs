using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        SetupCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupCamera()
    {
        Camera playerCamera = transform.GetComponent<Camera>();
        if (playerCamera == null) print("ERROR; cannot find camera");

        // Set the resolution to 480x480
        // Set the camera to only render the top-left quadrant of the screen
        //playerCamera.rect = new Rect(0, 0, 0.72f, 1f);
        playerCamera.rect = new Rect(0, 0, 1f, 1f);

    }

}
