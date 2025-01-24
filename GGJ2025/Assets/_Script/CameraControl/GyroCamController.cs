using System.Collections;
using UnityEngine;

public class GyroCamController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion initialGyroRotation;
    private Quaternion initialCameraRotation;

    // Smoothing factor for smoother motion
    public float smoothing = 0.1f;

    void Start()
    {
        StartCoroutine(DelayEnableGyro());
    }

    IEnumerator DelayEnableGyro()
    {
        yield return new WaitForSeconds(2);

        gyroEnabled = EnableGyro();
        if (gyroEnabled)
        {
            // Capture the initial rotation of the camera
            initialCameraRotation = transform.rotation;
            // Capture the initial gyroscope reading
            initialGyroRotation = GetGyroRotation();
        }
    }

    bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // Get the current gyroscope rotation
            Quaternion currentGyroRotation = GetGyroRotation();
            // Calculate the offset from the initial gyroscope reading
            Quaternion gyroOffset = Quaternion.Inverse(initialGyroRotation) * currentGyroRotation;

            // Apply the offset to the initial camera rotation
            Quaternion targetRotation = initialCameraRotation * gyroOffset;

            // Apply smoothing
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothing);

            // Debug log for tracking gyro rotation
            Debug.Log("Gyro Rotation: " + gyroOffset.eulerAngles);
        }
    }

    Quaternion GetGyroRotation()
    {
        // Convert the device's rotation to Unity's coordinate system
        Quaternion deviceRotation = gyro.attitude;
        return new Quaternion(deviceRotation.x, deviceRotation.y, -deviceRotation.z, -deviceRotation.w);
    }
}
