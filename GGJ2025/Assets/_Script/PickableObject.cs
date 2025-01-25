using UnityEngine;

public class PickableObject : Interactable
{
    public PickableScripableObject pickSO;

    public override void DoInteract()
    {
        base.DoInteract();

        GameManager.instance.AddPickableObject(pickSO);

        Destroy(gameObject);
    }
}
