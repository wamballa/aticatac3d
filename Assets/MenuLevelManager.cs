using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLevelManager : MonoBehaviour
{
    public TMP_Text[] textToFlash;
    public Image[] backgroundImages;

    private Color cyan;
    private bool isToggled;

    void Start()
    {
        cyan = new Color(0, 1, 1);
        StartCoroutine(FlashText());
    }

    private void Update()
    {
        if (Input.anyKey) SceneManager.LoadScene(1);
    }

    IEnumerator FlashText()
    {
        yield return new WaitForSeconds(0.25f);
        foreach(TMP_Text t in textToFlash)
        {

            if (isToggled) { t.color = cyan; } else { t.color = Color.black; }

        }
        foreach (Image i in backgroundImages)
        {

            if (isToggled) { i.color = Color.black; } else { i.color = cyan; }

        }
        isToggled = !isToggled;
        StartCoroutine(FlashText());
    }



}
