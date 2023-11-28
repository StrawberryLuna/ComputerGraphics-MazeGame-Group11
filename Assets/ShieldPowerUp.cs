using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float shieldDuration = 5.0f; // Duration of the shield

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BallController playerController = other.GetComponent<BallController>();
            if (playerController != null)
            {
                playerController.ActivateShield(shieldDuration); // Pass the shield duration
                gameObject.SetActive(false); // Optionally deactivate or destroy the power-up object
            }
        }
    }
}
