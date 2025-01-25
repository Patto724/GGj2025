using UnityEngine;

public class Plank : Interactable
{
    public Rigidbody plankBody;
    private int delaytime;
    private int disabletime = 10;

    [Space(10)]
    [SerializeField] AudioSource audioSource;

    public override void DoInteract()
    {
        if (GameManager.instance.HaveThisItem("Crowbar"))
        {
            plankBody.useGravity = true;
            plankBody.isKinematic = false;
            Invoke(nameof(DelayDisable), disabletime);
            audioSource.Play();
            GameManager.instance.PlayVoice(2);
            UIManager.instance.SetDialogText("i got it");
        }
        else
        {
            UIManager.instance.SetDialogText("need some tool");
        }
    }
    
    public void DelayDisable()
    {
        plankBody.useGravity = false;
        plankBody.isKinematic = true;
    }
}
