using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text scrollTimeText;
    public TMP_Text scrollscoreText;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text percentageCompleteText;

    int score = 0;
    int minutes = 0;
    int seconds=0;
    int percentComplete = 0;

    float timerDuration = 10f;
    float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        GetValues();
        SetValues();
    }



    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        if (timer <= 0) SceneManager.LoadScene(0);

        if (Input.anyKey) SceneManager.LoadScene(0);
    }

    private void SetValues()
    {
        scrollTimeText.text = timeText.text = string.Format("{0:000}:{1:00}", minutes, seconds);
        scrollscoreText.text = score.ToString("D6");
        scoreText.text = score.ToString("D6");
        percentageCompleteText.text = "12";
    }

    private void GetValues()
    {
        timer = timerDuration;
        score = PlayerPrefs.GetInt("Score");
        minutes = PlayerPrefs.GetInt("Minutes");
        seconds = PlayerPrefs.GetInt("Seconds");
        score = PlayerPrefs.GetInt("PercentComplete");
    }


}
