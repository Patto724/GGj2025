using UnityEngine;

public class Clock : Interactable
{
   public Rigidbody clockBody;
   private int disabletime = 10;
  
   public override void DoInteract()
   {
      clockBody.useGravity = true;
      clockBody.isKinematic = false;
      Invoke(nameof(DelayDisable), disabletime);
   }

   public void DelayDisable()
   {
      clockBody.useGravity = false;
      clockBody.isKinematic = true;
   }
}
