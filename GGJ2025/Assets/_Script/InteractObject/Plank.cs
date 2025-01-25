using UnityEngine;

public class Plank : Interactable
{
    public Rigidbody plankBody;
    private int delaytime;
    private int disabletime = 10;

    public override void DoInteract()
    {
        plankBody.useGravity = true;
        plankBody.isKinematic = false;
        Invoke(nameof(DelayDisable), disabletime);
        UIManager.instance.SetDialogText("i got it");
    }
    
    public void DelayDisable()
    {
        plankBody.useGravity = false;
        plankBody.isKinematic = true;
    }
}
