using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravityMultiplier = 2.0f; // This is our custom gravity scale.
    public Transform camera; // Reference to the camera transform
    private Rigidbody rb;
    private Vector3 movement;
    private bool isMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
}