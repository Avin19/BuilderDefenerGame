using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDamaged;
     public event EventHandler OnHeal;
    public event EventHandler OnDie;
    private int health;
    [SerializeField] private int maxAmountHealth;


    private void Awake() {
        
        health = maxAmountHealth;
    }
    public void Damage(int damageAmount)
    { 
        
        health -= damageAmount;
        health = Mathf.Clamp(health,0,maxAmountHealth);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if(IsDead())
        {
            OnDie?.Invoke(this, EventArgs.Empty); 
        }

    }
    public void Heal(int healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health,0,maxAmountHealth);
        OnHeal?.Invoke(this,EventArgs.Empty);
    }
    public void HealFull()
    {
        health = maxAmountHealth;;
        OnHeal?.Invoke(this,EventArgs.Empty);


    }
    public bool IsFullHealth()
    {
        return health == maxAmountHealth;
    }
    public bool IsDead()
    {
        return health == 0;

    }
    public int GetHealthInfo()
    {
        return health;
    }
     public int GetHealthMaxInfo()
    {
        return maxAmountHealth;
    }

    public float GetHealthAmountNormalized()
    {
        return (float) health/maxAmountHealth;
    }

    public void SetMaxHealth(int maxBuildingHealth , bool UpdateInfo)
    {
        
        maxAmountHealth = maxBuildingHealth;
        if(UpdateInfo)
        {
            health = maxBuildingHealth;
        }
    }

}
