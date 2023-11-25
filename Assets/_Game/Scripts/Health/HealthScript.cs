using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Enemy;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy enemy;
    public float health;
   
  
    public event Action <bool,float> OnHealthChange;
    public Action <float,bool> OnGetHit;
    

    private void Start()
    {
        ResetHealth();
    }

    private void OnEnable()
    {
        OnGetHit += SetHealth;
    }

    private void OnDisable()
    {
        OnGetHit -= SetHealth;
    }

    private void ResetHealth()
    {
        health = 100;
    }


    private void SetHealth(float damageAmount,bool isHeadShoot=false)
    {
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, 100);
        if (health <= 0)
        {
            enemy.OnDeadObject();
        }
        OnHealthChange?.Invoke(isHeadShoot,health);
        
    }
    
   
}
