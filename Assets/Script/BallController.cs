using UnityEngine;
using System.Collections; // Needed for IEnumerator

public class BallController : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravityMultiplier = 2.0f;
    public Transform camera;
    public AudioSource speedBoostAudioSource; // For speed boost sound
    public AudioSource shieldAudioSource; // For shield sound

    private Rigidbody rb;
    private Vector3 movement;
    private bool isMoving;
    private float originalSpeed;
    private bool isShielded = false; // Flag for shield status
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = speed; // Store the original speed
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Check if there's input from the user
        isMoving = moveHorizontal != 0f || moveVertical != 0f;

        // Convert movement vector to be relative to camera
        Vector3 forward = camera.forward;
        forward.y = 0; // Prevent character from trying to move up/down
        forward.Normalize();
        Vector3 right = camera.right;
        right.y = 0; // Again, prevent up/down movement.
        right.Normalize();
        movement = (forward * moveVertical + right * moveHorizontal).normalized; // Normalized to ensure consistent speed
    }

    void FixedUpdate()
    {
        // Applying custom gravity. The extra gravity is applied every frame to the ball's Rigidbody.
        Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
        rb.AddForce(extraGravityForce, ForceMode.Acceleration);

        if (isMoving)
        {
            // Adding force to the rigidbody for movement
            rb.AddForce(movement * speed);

            // This will add a rolling effect to the ball
            rb.AddTorque(new Vector3(movement.z, 0.0f, -movement.x) * speed);
        }
        else
        {
            // Here we're dampening both the linear and angular velocities
            // to bring the ball to a stop when there's no input
            rb.velocity *= 0.9f;
            rb.angularVelocity *= 0.9f;
        }
    }
    // Call this method to activate the speed boost
    public void ActivateSpeedBoost(float multiplier, float duration)
    {
        if (speed == originalSpeed) // Only boost if not already boosted
        {
            StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
        }
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        speed *= multiplier; // Increase the speed by the multiplier
        speedBoostAudioSource.Play(); // Start playing the speed boost music

        yield return new WaitForSeconds(duration); // Wait for the duration of the boost

        speed = originalSpeed; // Reset the speed back to the original value
        speedBoostAudioSource.Stop(); // Stop playing the speed boost music
    }
    public void ActivateShield(float duration)
    {
        if (!isShielded)
        {
            isShielded = true;
            shieldAudioSource.Play();
            StartCoroutine(ShieldDuration(duration));
        }
    }

    private IEnumerator ShieldDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isShielded = false;
        shieldAudioSource.Stop();
    }

    // Check if the shield is active
    public bool IsShieldActive()
    {
        return isShielded;
    }
}