using UnityEngine;

public class PowerUpAnimation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 100f, 0f); // Rotation per second around each axis
    public float floatAmplitude = 0.5f; // Height of the floating effect
    public float floatFrequency = 1f; // Speed of the floating effect

    private Vector3 startPos;

    void Start()
    {
        // Store the starting position of the object
        startPos = transform.position;
    }

    void Update()
    {
        // Rotation
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // Floating up and down
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.time * Mathf.PI * floatFrequency) * floatAmplitude;
        transform.position = tempPos;
    }
}
