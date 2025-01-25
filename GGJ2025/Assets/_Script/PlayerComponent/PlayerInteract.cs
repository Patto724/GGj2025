using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float raycastRange = 10.0f; // Adjustable range for the raycast
    public float spellRange = 10.0f;
    public LayerMask interactableLayer; // Layer mask for the "Interactable" layer
    public LayerMask spellLayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformRaycast();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            PerformSpell();
        }
    }

    public void PerformRaycast()
    {
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

    public void PerformSpell()
    {
        GameManager.instance.PlayVoice(1);

        // Define the ray starting point and direction
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, spellRange, spellLayer))
        {
            if (hit.collider.TryGetComponent(out ParticleSystem bubbleParticle))
            {
                bubbleParticle.Stop();
                UIManager.instance.spellGroup.SetActive(false);
                GameManager.instance.OnDisableBubble();
                GameManager.instance.isStopBubbleInTime = true;
            }
        }

        UIManager.instance.UseSpell();

        // Optional: Visualize the ray in the editor
        Debug.DrawRay(rayOrigin, rayDirection * spellRange, Color.green);
    }
}
