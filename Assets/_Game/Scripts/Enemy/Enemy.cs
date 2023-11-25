using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        
        private EnemyPatternSpawn patterEnemySpawner;
        private Transform target;
        private int id;
        private int targetListNo;
        
        [SerializeField] public Transform shootingPoint;
       
        public GameObject rayTracer;
        public event Action <Transform> OnSetTarget;
        public event Action OnTargetReached;
        public event Action OnRunning;
        public event Action OnIdle;
        public event Action OnShooting;
        public event Action OnDead;
        public int shootingTime;
        public int shootRate;
        public int noOfBulletsToShoot=4;

        public enum EnemyState
        {
            Idle,
            Running,
            Shooting,
            Dead
        };

        public EnemyState enemyState;
    
   

        private void OnEnable()
        {
            OnTargetReached += GetNewTarget;
        }

        private void OnDisable()
        {
            OnTargetReached += GetNewTarget;
        }
        
        public void SetTarget(Transform target,int id,EnemyPatternSpawn patterEnemySpawner )
        {
            enemyState = EnemyState.Running;
            var position = target.position;
            transform.LookAt(new Vector3(position.x,transform.position.y,position.z));
            this.patterEnemySpawner = patterEnemySpawner;
            this.id = id;
            this.target = target;
            OnSetTarget?.Invoke( this.target);
            OnRunningObject();
        }

        public void TargetReached()
        {
            OnTargetReached?.Invoke();
        }

        private void GetNewTarget()
        {
            targetListNo++;
            if (patterEnemySpawner.GetStandingPositionCount() == targetListNo)
            {
                StartAiming();
                return;
            }
            target = patterEnemySpawner.OnGetTarget(id, targetListNo);
            SetTarget(target, id,patterEnemySpawner);
        }

        private void StartAiming()
        {
          
            Debug.Log("reached");
            OnIdleObject();
            var position = GameManager.instance.GetPlayer().position;
            transform.LookAt(new Vector3(position.x,transform.position.y,position.z));
        }
        IEnumerator StartShootingRoutine()
        {
            enemyState = EnemyState.Shooting;
            while (enemyState == EnemyState.Shooting)
            {
                yield return  new WaitForSeconds(shootingTime);
                OnShootingObject();
                int bullets= 0;
                while (bullets<noOfBulletsToShoot)
                {
                    var position = shootingPoint.position;
                    GameObject go = Instantiate(rayTracer, position, Quaternion.identity);
                    go.SetActive(true);
                    Vector3 target = GameManager.instance.GetPlayer().position+Vector3.up*2 + Random.insideUnitSphere * 2;
                    go.transform.position = position;
                    go.transform.rotation = Quaternion.LookRotation( target- position);
                    yield return new WaitForSeconds(shootRate);
                    bullets++;
                }
                OnIdleObject();
            }
        }


        private void OnRunningObject()
        {
            OnRunning?.Invoke();
        }

        private void OnIdleObject()
        {
            enemyState = EnemyState.Idle;
            OnIdle?.Invoke();
            StopAllCoroutines();
            StartCoroutine(StartShootingRoutine());
        }

        private void OnShootingObject()
        {
            OnShooting?.Invoke();
        }
        public void OnDeadObject()
        {
            enemyState = EnemyState.Dead;
            StopAllCoroutines();
            OnDead?.Invoke();
        }
    }
}