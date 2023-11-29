using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnCoinScript : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI LeaderboardText;

    private int coin = 0;
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
}