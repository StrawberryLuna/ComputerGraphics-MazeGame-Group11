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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            BallController playerController = playerObject?.GetComponent<BallController>();
            EnemyAI enemyAI = collision.gameObject.GetComponent<EnemyAI>();

            if (playerController != null)
            {
                if (playerController.IsShieldActive() && enemyAI != null)
                {
                    Vector3 bounceDirection = collision.transform.position - playerObject.transform.position;
                    enemyAI.BounceOff(bounceDirection);
                    return; // If shielded, bounce off the enemy and don't trigger Game Over
                }
                else if (playerController.IsShieldActive() == false)
                {
                    GameOver();
                    deathSoundEffect.Play();
                }
            }
            else
            {
                Debug.LogError("BallController script not found on the player object.");
            }
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
