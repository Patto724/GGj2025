using UnityEngine;

public class Door : Interactable
{
    [SerializeField] Plank plank1;
    [SerializeField] Plank plank2;

    [SerializeField] AudioSource audioSource;

    public override void DoInteract()
    {
        if(plank1.isBreak && plank2.isBreak)
        {
            audioSource.Play();
            GameManager.instance.GoodEnd();
        }
        else
        {
            UIManager.instance.SetDialogText("need some tool");
        }
    }
}
