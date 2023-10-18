using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;  // The target object to follow
    public float distance = 30.0f;  // Distance from the target object
    public float xSpeed = 5.0f;  // Sensitivity to mouse movement on the X axis
    public float ySpeed = 5.0f;  // Sensitivity to mouse movement on the Y axis

    public float yMinLimit = -20f;  // Minimum Y rotation limit
    public float yMaxLimit = 80f;   // Maximum Y rotation limit

    private float x = 0.0f;  // Stored X rotation value
    private float y = 0.0f;  // Stored Y rotation value

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation (if the camera has a Rigidbody component)
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    // LateUpdate is called once per frame, after Update methods have run
    void LateUpdate()
    {
        if (target)
        {
            // Get input from mouse movement
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            // Clamp the vertical angle within the min and max limits
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            // Set the rotation of the camera
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // Set the position of the camera
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            // Update the transform of the camera
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    // Clamp angle utility function
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}