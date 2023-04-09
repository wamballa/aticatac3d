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
        palette = ScriptableObject.CreateInstance<ZXPalette>();
        palette = ZXPalette.CreateInstance<ZXPalette>();
        targetColor = palette.GetZXColor();

        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>( true);
        SpriteRenderer[] spriteRenders = GetComponentsInChildren<SpriteRenderer>(true);

        if (meshRenderers.Length == 0)
        {
            foreach (SpriteRenderer m in spriteRenders)
            {
                m.material.color = targetColor;
            }
        }
        else
        {
            foreach (MeshRenderer m in meshRenderers)
            {
                m.material.color = targetColor;
            }
        }
    }
}
