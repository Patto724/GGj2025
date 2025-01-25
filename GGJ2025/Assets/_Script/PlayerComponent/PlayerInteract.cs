using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float raycastRange = 10.0f; // Adjustable range for the raycast
    public LayerMask interactableLayer; // Layer mask for the "Interactable" layer

    public void PerformRaycast()
    {
        // Define the ray starting point and direction
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, raycastRange, interactableLayer))
        {
            // Check if the hit object is on the "Interactable" layer
            Debug.Log("Hit an interactable object: " + hit.collider.gameObject.name);
        }

        // Optional: Visualize the ray in the editor
        Debug.DrawRay(rayOrigin, rayDirection * raycastRange, Color.green);
    }
}
