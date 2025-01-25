using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float raycastRange = 10.0f; // Adjustable range for the raycast
    public LayerMask interactableLayer; // Layer mask for the "Interactable" layer

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformRaycast();
        }
    }

    public void PerformRaycast()
    {
        Debug.Log("Performing raycast...");

        // Define the ray starting point and direction
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, raycastRange, interactableLayer))
        {
            if(hit.collider.TryGetComponent(out Interactable interactable))
            {
                interactable.DoInteract();
            }
        }

        // Optional: Visualize the ray in the editor
        Debug.DrawRay(rayOrigin, rayDirection * raycastRange, Color.green);
    }
}
