using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60; // Set your desired time here
    public bool timeIsRunning = true;

    public TMP_Text timeText;

    void Start()
    {
        timeIsRunning = true;
    }

    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                updateTimer(timeRemaining);
            }
            else
            {
                Debug.Log("Game Over!");
                timeRemaining = 0;
                timeIsRunning = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float min = Mathf.FloorToInt(currentTime / 60);
        float sec = Mathf.FloorToInt(currentTime % 60);

        timeText.text = string.Format("{0:00} : {1:00}", min, sec);
    }
}