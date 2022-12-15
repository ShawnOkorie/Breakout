using UnityEngine;

public class BallEffects : MonoBehaviour
{
   private Ball myBall;
   private ParticleSystem particleSystem;
   public int particleCount;
   
   private void Awake()
   {
      myBall = GetComponentInParent<Ball>();
      particleSystem = GetComponent<ParticleSystem>();
   }

   private void OnEnable()
   { 
      myBall.PowerupActive += CheckPowerupState;
   }

   private void OnDisable()
   { 
      myBall.PowerupActive -= CheckPowerupState;
   }

   private void CheckPowerupState(bool isActive)
   {
      if (isActive == true)
      {
         particleSystem.Play();
      }

      if (isActive == false)
      {
         particleSystem.Stop();
      }
   }

      
}