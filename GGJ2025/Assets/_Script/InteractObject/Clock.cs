using UnityEngine;

public class Clock : Interactable
{
   public Rigidbody clockBody;
   private int delaytime = 10;
  
   public override void DoInteract()
   {
      clockBody.useGravity = true;
      clockBody.isKinematic = false;
      Invoke(nameof(DelayDisable), delaytime);
   }

   public void DelayDisable()
   {
      clockBody.useGravity = false;
      clockBody.isKinematic = true;
   }
}
