using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Health
{
   public class DamageScript : MonoBehaviour
   {
      public HealthScript healthScript;

      public enum HitType
      {
         Body,
         Head
      };

      public HitType hitType;
      [Range(1, 100)]
      public float damageAmount;

      private void OnEnable()
      {
         healthScript = GetComponentInParent<HealthScript>();
         healthScript.enemy.OnDead += Dead;
         
      }
      private void OnDisable()
      {
         healthScript.enemy.OnDead -= Dead;
      }

      public void GetHit()
      {
         healthScript.OnGetHit?.Invoke(damageAmount,hitType==HitType.Head);
      }

      private void Dead()
      {
         if(TryGetComponent<Collider>(out Collider collider))
         {
            //collider.enabled = false;
         }

         transform.tag = "Untagged";

         this.enabled = false;

      }
   }
}
