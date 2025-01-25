using UnityEngine;

public class Locker : Interactable
{
    [SerializeField] private PickableScripableObject crowbarSO;
    private bool itsPicked = false;
    public override void DoInteract()
    {
        if (!itsPicked)
        {
            GameManager.instance.AddPickableObject(crowbarSO);
            itsPicked = true;
        }
        
    }
}
