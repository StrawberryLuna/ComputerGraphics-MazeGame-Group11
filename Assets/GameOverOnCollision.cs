using UnityEngine;

public class GameOverOnCollision : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign this in the Inspector
    public AudioSource deathSoundEffect;

    private void Start()
    {
        // Ensure the game over panel is not active when the game starts
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag 'Enemy'
        if (collision.gameObject.CompareTag("Enemy"))
        {
            deathSoundEffect.Play();
            GameOver();
        }
    }

    private void GameOver()
    {
        // Activate the game over panel to show it

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Optional: Pause the game
        Time.timeScale = 0;
    }
}
