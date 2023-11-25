using System.Collections;
using _Game.Scripts.Enemy;
using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
 
   public float speed;
   private Enemy enemy;
   private Transform target;

   private void Awake()
   {
      enemy = GetComponent<Enemy>();
   }

   private void OnEnable()
   {
      enemy.OnSetTarget += MoveMe;
   }

   private void OnDisable()
   {
      enemy.OnSetTarget -= MoveMe;
   }
   private void MoveMe(Transform target)
   {
      this.target = target;
      StopAllCoroutines();
      StartCoroutine(MovePlayerToTarget());
      // Debug.Log("aaaa");
     
   }

   IEnumerator MovePlayerToTarget()
   {
      var currentPos = transform.position;
      while (new Vector3(currentPos.x,target.position.y,currentPos.z)!=target.position)
      {
         // Debug.Log("ccc");
         yield return new WaitForFixedUpdate();
         
         var position = target.position;
          currentPos = transform.position;
         transform.position=  Vector3.MoveTowards(currentPos, new Vector3(position.x,currentPos.y,position.z), speed);
      }
      enemy.TargetReached();
      
   }
}
