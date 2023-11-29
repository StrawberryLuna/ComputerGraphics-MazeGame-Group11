using System.Collections;
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
            coin++;
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
