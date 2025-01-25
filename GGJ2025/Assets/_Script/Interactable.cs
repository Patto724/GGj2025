using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void DoInteract()
    {
        
    }

    [ContextMenu("Test Interact")]
    public void TestInteract()
    {
        DoInteract();
    }
}
