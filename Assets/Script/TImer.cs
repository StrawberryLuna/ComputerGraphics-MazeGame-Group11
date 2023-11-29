using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float totalTime = 60; // Set the total time for the countdown
    public float timeRemaining; // Current time remaining
    public TMP_Text timeText; // Reference to the TextMeshPro text component
    public GameObject gameOverPanel; // Reference to the Game Over panel
    public AudioSource mainMusicAudioSource; // Assign the main music audio source
    public AudioSource finalThirdMusicAudioSource; // Assign the final third audio source
    public AudioSource endSoundEffect;

    private bool timerIsRunning = false;
    private bool finalThirdTriggered = false;

    void Start()
    {
        timeRemaining = totalTime; // Initialize timeRemaining with totalTime
        timerIsRunning = true;
        finalThirdTriggered = false;
        gameOverPanel.SetActive(false); // Hide the Game Over panel at start
        mainMusicAudioSource.Play(); // Start the main music
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);

                // Check if the time has reached the final third and final third music hasn't been triggered yet
                if (!finalThirdTriggered && timeRemaining <= totalTime / 3)
                {
                    // Switch to the final third music
                    mainMusicAudioSource.Stop(); // Stop the main music
                    finalThirdMusicAudioSource.Play(); // Start the final third music
                    finalThirdTriggered = true; // Ensure this only happens once
                }
            }
            else
            {
                if (!finalThirdTriggered)
                {
                    // If the timer reaches 0 and the final third music hasn't been triggered,
                    // ensure that the main music stops playing.
                    mainMusicAudioSource.Stop();
                }

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
