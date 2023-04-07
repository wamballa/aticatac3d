using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZXPalette : ScriptableObject
{
    public Color[] colors = new Color[] {
        //new Color(0.0f, 0.0f, 0.8f, 1.0f),         // Blue
        //new Color(0.8f, 0.0f, 0.0f, 1.0f),         // Red
        //new Color(0.8f, 0.0f, 0.8f, 1.0f),         // Magenta
        //new Color(0.0f, 0.8f, 0.0f, 1.0f),         // Green
        //new Color(0.0f, 0.8f, 0.8f, 1.0f),         // Cyan
        //new Color(0.8f, 0.8f, 0.0f, 1.0f),         // Yellow
        //new Color(0.8f, 0.8f, 0.8f, 1.0f),         // White
        new Color(0.0f, 0.0f, 1.0f, 1.0f),         // Bright Blue
        new Color(1.0f, 0.0f, 0.0f, 1.0f),         // Bright Red
        new Color(1.0f, 0.0f, 1.0f, 1.0f),         // Bright Magenta
        new Color(0.0f, 1.0f, 0.0f, 1.0f),         // Bright Green
        new Color(0.0f, 1.0f, 1.0f, 1.0f),         // Bright Cyan
        new Color(1.0f, 1.0f, 0.0f, 1.0f),         // Bright Yellow
        new Color(1.0f, 1.0f, 1.0f, 1.0f),         // Bright White
    };


    public Color GetZXColor()
    {
        int numColors = colors.Length;
        int randColor = Random.Range(0, numColors - 1);
        return colors[randColor];
    }
}
