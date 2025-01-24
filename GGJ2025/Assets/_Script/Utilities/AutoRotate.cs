using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private Vector3 axis = Vector3.up;

    private void Update()
    {
        transform.Rotate(axis, speed * Time.deltaTime);
    }
}
