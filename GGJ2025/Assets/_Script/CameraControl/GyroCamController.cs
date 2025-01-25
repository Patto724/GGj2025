using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;

public class GyroCamController : MonoBehaviour
{
    private Quaternion initialCameraRotation;
    private Quaternion initialGyroRotation;
    public float smoothing = 0.1f;

    [HideInInspector] public float rotationX;
    [HideInInspector] public float rotationY;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void GetDeviceOrientation();
#endif

    void Start()
    {
        initialCameraRotation = transform.rotation;
        StartCoroutine(InitializeGyro()); // Delay to allow orientation data to initialize
    }

    IEnumerator InitializeGyro()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        initialGyroRotation = GetGyroRotation();
#else
        while (!Input.gyro.enabled)
        {
            if (SystemInfo.supportsGyroscope)
            {
                Input.gyro.enabled = true;
                initialGyroRotation = GetGyroRotation();
            }
            else
            {
                Debug.LogError("Gyroscope not supported on this device.");
            }

            yield return new WaitForSeconds(1);
        }
#endif
    }

    void Update()
    {
        if(!Input.gyro.enabled)
            return;

        Input.gyro.enabled = true;

        Quaternion currentGyroRotation = GetGyroRotation();
        Quaternion gyroOffset = Quaternion.Inverse(initialGyroRotation) * currentGyroRotation;
        Quaternion targetRotation = initialCameraRotation * gyroOffset;

        // Extract pitch and roll for the camera
        Vector3 eulerRotation = targetRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(eulerRotation.x, 0, eulerRotation.z);

        // Store yaw separately
        rotationX = eulerRotation.x;
        rotationY = eulerRotation.y; // If you need to store pitch as well
    }

    Quaternion GetGyroRotation()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        GetDeviceOrientation();
        float alpha = 0; // Replace with actual JS-to-Unity communication
        float beta = 0;  // Replace with actual JS-to-Unity communication
        float gamma = 0; // Replace with actual JS-to-Unity communication

        // Convert the orientation angles to a quaternion
        return Quaternion.Euler(beta, -alpha, -gamma);
#else
        Quaternion deviceRotation = Input.gyro.attitude;
        // Convert the device's rotation to Unity's coordinate system
        return new Quaternion(deviceRotation.x, deviceRotation.y, -deviceRotation.z, -deviceRotation.w);
#endif
    }
}
