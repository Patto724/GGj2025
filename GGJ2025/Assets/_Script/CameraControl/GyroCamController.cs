using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;

public class GyroCamController : MonoBehaviour
{
    private Quaternion initialCameraRotation;
    private Quaternion initialGyroRotation;
    bool isSetInitialGyroRotation = false;
    public float smoothing = 0.1f;

    [HideInInspector] public float rotationX;
    [HideInInspector] public float rotationY;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void GetDeviceOrientation();
    [DllImport("__Internal")]
    private static extern float GetAlpha();
    [DllImport("__Internal")]
    private static extern float GetBeta();
    [DllImport("__Internal")]
    private static extern float GetGamma();
#endif

    void Start()
    {
        initialCameraRotation = transform.rotation;
        StartCoroutine(InitializeGyro());
    }

    IEnumerator InitializeGyro()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        GetDeviceOrientation();
        yield return new WaitForSeconds(1); // Allow time for orientation data to initialize
        initialGyroRotation = GetGyroRotation();
        isSetInitialGyroRotation = true;
#else
        yield return new WaitForSeconds(1);

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            initialGyroRotation = GetGyroRotation();
            isSetInitialGyroRotation = true;
            Debug.Log("Gyro initialized successfully.");
        }
        else
        {
            Debug.LogError("Gyroscope not supported on this device.");
        }
#endif
    }

    void Update()
    {
        Input.gyro.enabled = true;//need this here for mobile to work for some reason (remote test works without this line)
#if UNITY_WEBGL && !UNITY_EDITOR
        if (!isSetInitialGyroRotation)
        {
            return;
        }
#endif

        Quaternion currentGyroRotation = GetGyroRotation();
        Quaternion gyroOffset = Quaternion.Inverse(initialGyroRotation) * currentGyroRotation;
        Quaternion targetRotation = initialCameraRotation * gyroOffset;

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothing);

        Vector3 eulerRotation = targetRotation.eulerAngles;
        rotationX = eulerRotation.x;
        rotationY = eulerRotation.y;
    }

    Quaternion GetGyroRotation()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        float alpha = GetAlpha();
        float beta = GetBeta();
        float gamma = GetGamma();

        return Quaternion.Euler(gamma, -alpha, alpha);

        //1st axis is for up and down motion
        //2nd axis is for left and right motion
        //3rd axis is for roll left and right motion
        //GetAlpha data is from turn left and right
        //GetBeta data is from roll left and right
        //GetGamma data is from turn up and down
#else
        Quaternion deviceRotation = Input.gyro.attitude;
        return new Quaternion(deviceRotation.x, deviceRotation.y, -deviceRotation.z, -deviceRotation.w);
#endif
    }
}