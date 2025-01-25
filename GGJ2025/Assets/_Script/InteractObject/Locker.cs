using UnityEngine;

public class Locker : Interactable
{
    public GameObject safeLock;
    [SerializeField] private PickableScripableObject crowbarSO;
    private bool itsPicked = false;
    
    public override void DoInteract()
    {
        if (!itsPicked)
        {
            safeLock.SetActive(true);
        }

    }

    public void GetCrowbar()
    {
        GameManager.instance.AddPickableObject(crowbarSO);
        itsPicked = true;
    }
}
