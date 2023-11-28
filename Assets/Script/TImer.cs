using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60; // Set the timer for one minute
    public TMP_Text timeText; // Reference to the TextMeshPro text component
    public GameObject gameOverPanel; // Reference to the Game Over panel
    public AudioSource endSoundEffect;
    private bool timerIsRunning = false;

    void Start()
    {
        timerIsRunning = true;
        gameOverPanel.SetActive(false); // Hide the Game Over panel at start
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                Debug.Log("Time's Up! Game Over!");
                timeRemaining = 0;
                timerIsRunning = false;
                gameOverPanel.SetActive(true); // Show the Game Over panel
                endSoundEffect.Play();
                Time.timeScale = 0; // This pauses the game
            }
        }
    }

    private void UpdateTimerDisplay(float currentTime)
    {
        currentTime = Mathf.Max(0, currentTime); // Ensure the time doesn't go negative
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
