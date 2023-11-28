using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float speedMultiplier = 2.0f; // The factor by which the player's speed is increased
    public float duration = 5.0f; // How long the speed boost lasts

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has a "Player" tag
        {
            BallController playerMovement = other.GetComponent<BallController>();

            if (playerMovement != null)
            {
                playerMovement.ActivateSpeedBoost(speedMultiplier, duration);
            }
            // Optionally, deactivate or destroy the power-up object
            gameObject.SetActive(false);
        }
    }
}
