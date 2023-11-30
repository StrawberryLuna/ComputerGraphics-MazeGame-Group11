/*using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnCoinScript : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI LeaderboardText;

    private int coin = 0; //INITIALIZE  VARIBLES
    private int highScore = 0;
    private const string HighScoreKey = "HighScore";

    private List<int> leaderboardScores = new List<int>();
    private const int MaxLeaderboardSize = 5;
    private bool gameStartedFresh = false; // Initialization added here
    private bool quitting = false;



    private void Start()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateHighScoreText();
        LoadLeaderboard();
        UpdateLeaderboardText();

        // Check if the game is starting fresh or restarting
        if (!gameStartedFresh)
        {
            // If it's a fresh start, reset the scoreboard
            InitializeScoreboard();
            gameStartedFresh = true;
        }
        else
        {
            // If it's a game restart, initialize the scoreboard without resetting
            InitializeScoreboard();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CoinTag"))
        {
            coin++;
            CoinText.text = "Points: " + coin.ToString();

            if (coin > highScore)
            {
                highScore = coin;
                PlayerPrefs.SetInt(HighScoreKey, highScore);
                UpdateHighScoreText();
            }

            AddToLeaderboard(coin);
            UpdateLeaderboardText();

            AudioSource coinAudioSource = other.GetComponent<AudioSource>();
            if (coinAudioSource != null)
            {
                coinAudioSource.Play();
                DisableCoinComponents(other.gameObject);
                Destroy(other.gameObject, coinAudioSource.clip.length);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void DisableCoinComponents(GameObject coin)
    {
        Collider coinCollider = coin.GetComponent<Collider>();
        if (coinCollider != null)
            coinCollider.enabled = false;

        Renderer coinRenderer = coin.GetComponent<Renderer>();
        if (coinRenderer != null)
            coinRenderer.enabled = false;
    }
    public void EndGame()
    {
        if (coin > highScore)
        {
            highScore = coin;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            UpdateHighScoreText();
        }

        AddToLeaderboard(coin);
        UpdateLeaderboardText();
    }
    private void OnApplicationQuit()
    {
        quitting = true;
        // Reset the scoreboard and save data when quitting
        ResetScoreboard();
    }

     public void ResetScoreboard()
    {
        // Only reset when quitting the game
        if (quitting)
        {
            highScore = 0;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            UpdateHighScoreText();

            leaderboardScores.Clear();
            for (int i = 0; i < MaxLeaderboardSize; i++)
            {
                PlayerPrefs.SetInt("LeaderboardScore" + i, 0);
            }
            PlayerPrefs.Save();

            coin = 0;
            CoinText.text = "Points: " + coin.ToString();
        }
    }

    private void InitializeScoreboard()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateHighScoreText();
        LoadLeaderboard();
        UpdateLeaderboardText();
    }

    private void UpdateHighScoreText()
    {
        HighScoreText.text = "High Score: " + highScore.ToString();
    }

    private void LoadLeaderboard()
    {
        leaderboardScores.Clear();
        for (int i = 0; i < MaxLeaderboardSize; i++)
        {
            int savedScore = PlayerPrefs.GetInt("LeaderboardScore" + i, 0);
            leaderboardScores.Add(savedScore);
        }
    }

    private void SaveLeaderboard()
    {
        for (int i = 0; i < MaxLeaderboardSize; i++)
        {
            PlayerPrefs.SetInt("LeaderboardScore" + i, leaderboardScores[i]);
        }
        PlayerPrefs.Save();
    }

    private void AddToLeaderboard(int score)
    {
        leaderboardScores.Add(score);
        leaderboardScores.Sort((a, b) => b.CompareTo(a));

        if (leaderboardScores.Count > MaxLeaderboardSize)
        {
            leaderboardScores.RemoveAt(MaxLeaderboardSize);
        }

        SaveLeaderboard();
    }

    private void UpdateLeaderboardText()
    {
        LeaderboardText.text = "Leaderboard:\n";
        for (int i = 0; i < leaderboardScores.Count; i++)
        {
            LeaderboardText.text += (i + 1) + ". " + leaderboardScores[i] + "\n";
        }
    }
}*/

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnCoinScript : MonoBehaviour
{
    // References to TextMeshProUGUI objects in the Unity Editor
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI LeaderboardText;

    // Variables to hold game data
    private int coin = 0; // Player's current coin count
    private int highScore = 0; // Player's highest score
    private const string HighScoreKey = "HighScore"; // Key for PlayerPrefs to store high score

    // Leaderboard-related variables
    private List<int> leaderboardScores = new List<int>(); // List to hold leaderboard scores
    private const int MaxLeaderboardSize = 5; // Maximum number of scores on the leaderboard
    private bool gameStartedFresh = false; // Flag to determine if the game started fresh or restarted
    private bool quitting = false; // Flag to determine if the application is quitting

    // Runs when the script is started
    private void Start()
    {
        InitializeGame(); // Calls method to initialize the game
    }

    // Initializes the game by setting up the scoreboard and UI elements
    private void InitializeGame()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0); // Retrieves high score from PlayerPrefs
        coin = 0; // Resets the player's current coin count

        UpdateHighScoreText(); // Updates the high score text in the UI
        UpdateCoinText(); // Updates the coin count text in the UI

        LoadLeaderboard(); // Loads leaderboard data from PlayerPrefs
        UpdateLeaderboardText(); // Updates the leaderboard UI
    }

   // Triggered when the object enters a collider trigger
    private void OnTriggerEnter(Collider other)
    {
    // Check if the collider belongs to a game object tagged as "CoinTag"
    if (other.CompareTag("CoinTag"))
    {
        // Call method to handle collision with a coin object
        HandleCoinCollision(other); // Handles collision with a coin object
    }
    }

    // Handles collision with a coin object
    private void HandleCoinCollision(Collider coinCollider)
    {
        coin++; // Increments the player's coin count
        CoinText.text = "Points: " + coin.ToString(); // Updates coin count text in the UI

        // Checks if the current coin count surpasses the high score
        if (coin > highScore)
        {
            highScore = coin; // Updates the high score
            UpdateHighScore(); // Saves and updates the high score in PlayerPrefs and UI
        }

        AddToLeaderboard(coin); // Adds current score to the leaderboard
        UpdateLeaderboardText(); // Updates the leaderboard UI

        // Handles coin audio and disables/destroys coin object
        AudioSource coinAudioSource = coinCollider.GetComponent<AudioSource>();
        if (coinAudioSource != null)
        {
            coinAudioSource.Play();
            DisableCoinComponents(coinCollider.gameObject);
            Destroy(coinCollider.gameObject, coinAudioSource.clip.length);
        }
        else
        {
            Destroy(coinCollider.gameObject);
        }
    }

    // Updates the high score in PlayerPrefs and UI
    private void UpdateHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, highScore); // Saves high score in PlayerPrefs
        UpdateHighScoreText(); // Updates high score text in the UI
    }

    // Triggered when the game ends
    public void EndGame()
    {
        UpdateHighScore(); // Updates the high score
        AddToLeaderboard(coin); // Adds final score to the leaderboard
        UpdateLeaderboardText(); // Updates the leaderboard UI
    }

    // Triggered when the application is quitting
    private void OnApplicationQuit()
    {
        quitting = true; // Sets the quitting flag to true
        ResetScoreboard(); // Resets the scoreboard and saves data when quitting
    }

    // Resets the scoreboard data
    public void ResetScoreboard()
    {
        if (quitting) // Only reset when quitting the game
        {
            highScore = 0; // Resets the high score
            PlayerPrefs.SetInt(HighScoreKey, highScore); // Saves the reset high score in PlayerPrefs
            UpdateHighScoreText(); // Updates high score text in the UI

            leaderboardScores.Clear(); // Clears the leaderboard scores list
            for (int i = 0; i < MaxLeaderboardSize; i++)
            {
                PlayerPrefs.SetInt("LeaderboardScore" + i, 0); // Resets individual leaderboard scores in PlayerPrefs
            }
            PlayerPrefs.Save(); // Saves changes in PlayerPrefs

            coin = 0; // Resets the player's coin count
            UpdateCoinText(); // Updates coin count text in the UI
        }
    }

    // Updates the high score text in the UI
    private void UpdateHighScoreText()
    {
        HighScoreText.text = "High Score: " + highScore.ToString();
    }

    // Updates the coin count text in the UI
    private void UpdateCoinText()
    {
        CoinText.text = "Points: " + coin.ToString();
    }

    // Loads leaderboard data from PlayerPrefs
    private void LoadLeaderboard()
    {
        leaderboardScores.Clear(); // Clears the leaderboard scores list
        for (int i = 0; i < MaxLeaderboardSize; i++)
        {
            int savedScore = PlayerPrefs.GetInt("LeaderboardScore" + i, 0); // Retrieves individual leaderboard scores from PlayerPrefs
            leaderboardScores.Add(savedScore); // Adds the retrieved scores to the leaderboard list
        }
    }

    // Saves leaderboard data to PlayerPrefs
    private void SaveLeaderboard()
    {
        for (int i = 0; i < MaxLeaderboardSize; i++)
        {
            PlayerPrefs.SetInt("LeaderboardScore" + i, leaderboardScores[i]); // Saves individual leaderboard scores in PlayerPrefs
        }
        PlayerPrefs.Save(); // Saves changes in PlayerPrefs
    }

    // Adds a score to the leaderboard, sorts it, and updates PlayerPrefs
    private void AddToLeaderboard(int score)
    {
        leaderboardScores.Add(score); // Adds the current score to the leaderboard
        leaderboardScores.Sort((a, b) => b.CompareTo(a)); // Sorts the leaderboard scores in descending order

        if (leaderboardScores.Count > MaxLeaderboardSize)
        {
            leaderboardScores.RemoveAt(MaxLeaderboardSize); // Removes scores exceeding the maximum leaderboard size
        }

        SaveLeaderboard(); // Saves the updated leaderboard in PlayerPrefs
    }

    // Updates the leaderboard text in the UI
    private void UpdateLeaderboardText()
    {
        LeaderboardText.text = "Leaderboard:\n";
        for (int i = 0; i < leaderboardScores.Count; i++)
        {
            LeaderboardText.text += (i + 1) + ". " + leaderboardScores[i] + "\n"; // Formats and displays leaderboard scores in the UI
        }
    }

    // Disables coin components (Collider and Renderer) after collection
    private void DisableCoinComponents(GameObject coin)
    {
        Collider coinCollider = coin.GetComponent<Collider>();
        if (coinCollider != null)
            coinCollider.enabled = false;

        Renderer coinRenderer = coin.GetComponent<Renderer>();
        if (coinRenderer != null)
            coinRenderer.enabled = false;
    }
}
