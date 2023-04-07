using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    public TMP_Text timerText;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);

        timerText.text = string.Format("{0:000}:{1:00}", minutes, seconds);
        //timerText.text = seconds.ToString();
        //Canvas.ForceUpdateCanvases();
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
    }
}
