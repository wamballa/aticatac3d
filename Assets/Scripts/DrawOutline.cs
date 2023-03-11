using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOutline : MonoBehaviour
{
    GUIStyle style = new GUIStyle();
    public Texture texture;

    // Start is called before the first frame update
    void Start()
    {
        style.alignment = TextAnchor.MiddleCenter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        //top left point of rectangle
        Vector3 boxPosHiLeftWorld = new Vector3(0.5f, 12, 0);
        //bottom right point of rectangle
        Vector3 boxPosLowRightWorld = new Vector3(1.5f, 0, 0);

        Vector3 boxPosHiLeftCamera = Camera.main.WorldToScreenPoint(boxPosHiLeftWorld);
        Vector3 boxPosLowRightCamera = Camera.main.WorldToScreenPoint(boxPosLowRightWorld);

        float width = boxPosHiLeftCamera.x - boxPosLowRightCamera.x;
        float height = boxPosHiLeftCamera.y - boxPosLowRightCamera.y;


        GUI.Box(new Rect(boxPosHiLeftCamera.x, Screen.height - boxPosHiLeftCamera.y, width, height), "", style);
    }


}
