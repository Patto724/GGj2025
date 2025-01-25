using UnityEngine;

public class HowToPaperScript : Interactable
{
    public override void DoInteract()
    {
        base.DoInteract();

        UIManager.instance.howToPaper.SetActive(true);
    }
}
