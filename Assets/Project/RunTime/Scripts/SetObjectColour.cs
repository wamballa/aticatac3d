using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectColour : MonoBehaviour
{
    ZXPalette palette;
    Color targetColor;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>(includeInactive: true);

        //palette = new ZXPalette();
        palette = ZXPalette.CreateInstance<ZXPalette>();
        targetColor = palette.GetZXColor();

        foreach (MeshRenderer m in meshRenderers)
        {
            m.material.color = targetColor;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
