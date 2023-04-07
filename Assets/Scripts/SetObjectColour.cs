using System.Collections.Generic;
using System;
using UnityEngine;

public class SetObjectColour : MonoBehaviour
{
    private ZXPalette palette;
    private Color targetColor;

    // Start is called before the first frame update
    void Start()
    {

        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>( true);

        palette = new ZXPalette();
        palette = ZXPalette.CreateInstance<ZXPalette>();
        targetColor = palette.GetZXColor();

        foreach (MeshRenderer m in meshRenderers)
        {
            m.material.color = targetColor;
        }

    }

}
