using UnityEngine;

public class AutoRockingMotion : MonoBehaviour
{
    public float floatAmplitude = 0.5f;          // The height the object moves up and down
    public float floatFrequency = 1.0f;          // Speed of the up and down movement
    public float rotationAmplitude = 5.0f;       // The angle the object rotates around
    public float rotationFrequency = 1.0f;       // Speed of the rotation movement

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        // Store the initial position and rotation
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Calculate new position for subtle floating effect
        Vector3 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        // Calculate new rotation for subtle rocking effect
        Quaternion newRotation = startRotation;
        float rotationAngle = Mathf.Sin(Time.time * rotationFrequency) * rotationAmplitude;
        newRotation *= Quaternion.Euler(new Vector3(rotationAngle, 0, rotationAngle));

        // Apply the transformations to the object
        transform.position = newPosition;
        transform.rotation = newRotation;
    }
}