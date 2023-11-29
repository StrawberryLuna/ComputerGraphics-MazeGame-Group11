/*using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnCoinScript : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI HighScoreText;
    private int coin = 0;
    private int highScore = 0;
    private const string HighScoreKey = "HighScore";


    private void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateHighScoreText();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("CoinTag"))
        {
            coin++; //increment point
            CoinText.text = "Points: " + coin.ToString();
            Debug.Log("Collected coins: " + coin);
            //For highscore based on coin/points
             if (coin > highScore)
            {
                highScore = coin;
                PlayerPrefs.SetInt(HighScoreKey, highScore);
                UpdateHighScoreText();
            }

            // Play the coin's collection sound
            AudioSource coinAudioSource = other.gameObject.GetComponent<AudioSource>();
            if (coinAudioSource != null)
            {
                // Play the sound
                coinAudioSource.Play();

                // Disable the collider and renderer to "hide" the coin while the sound plays
                DisableCoinComponents(other.gameObject);

                // Destroy the coin after the sound finished playing
                Destroy(other.gameObject, coinAudioSource.clip.length);
            }
            else
            {
                // If no AudioSource found, just destroy the coin immediately
                Destroy(other.gameObject);
            }
        }
    }

    private void DisableCoinComponents(GameObject coin)
    {
        // Disable collider and renderer to "hide" the coin
        Collider coinCollider = coin.GetComponent<Collider>();
        if (coinCollider != null)
            coinCollider.enabled = false;

        Renderer coinRenderer = coin.GetComponent<Renderer>();
        if (coinRenderer != null)
            coinRenderer.enabled = false;
    }
    private void UpdateHighScoreText()
    {
        HighScoreText.text = "High Score: " + highScore.ToString();
    }
}
*/
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

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateHighScoreText();
        LoadLeaderboard();
        UpdateLeaderboardText();
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
