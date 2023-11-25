using DG.Tweening;
using UnityEngine;
namespace _Game.Scripts
{
    public class EnemyAnimationController : MonoBehaviour
    {
        public Animator anim;
        public Enemy.Enemy enemy;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void OnEnable()
        {
            enemy.OnIdle += Idle;
            enemy.OnRunning += Running;
            enemy.OnShooting += Shooting;
            enemy.OnDead += DeadAnimation;
        }

        private void OnDisable()
        {
            enemy.OnIdle -= Idle;
            enemy.OnRunning -= Running;
            enemy.OnShooting -= Shooting;
            enemy.OnDead -= DeadAnimation;
        }

        private void Running()
        {
            SetBlendValue(0);
          
        }
        private void Idle()
        {
            SetBlendValue(1);

        }
        private void Shooting()
        {
            SetBlendValue(3);
        }

        private void SetBlendValue(float val)
        {
            float blendValue=anim.GetFloat(Speed);
            DOTween.To(() => blendValue, x => blendValue = x, val, 0.4f).OnUpdate(() =>
            {
                anim.SetFloat(Speed,blendValue);
            });
        }

        private void DeadAnimation()
        {
           // anim.Play("Death");
           anim.enabled = false;
        }
    }
}