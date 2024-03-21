using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool IsDead {get; private set; }

    public event Action<int> OnDamage;
    public event Action OnDie;

    private void Awake()
    {
        OnDamage += TakeDamage;
    }

    public void InitHealth(float amount)
    {
        maxHealth = amount;
        health = maxHealth;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        OnDamage(damageAmount);
    }

    private void TakeDamage(int damageAmount)
    {
        health = Math.Max(health - damageAmount, 0);
        if (health <= 0)
        {
            OnDie?.Invoke();
            IsDead = true;
        }
    }
}
