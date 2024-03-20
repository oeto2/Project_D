using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    private int _health;

    public event Action<int> OnDamage;
    public event Action OnDie;

    private void Start()
    {
        OnDamage += TakeDamage;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        OnDamage(damageAmount);
    }

    private void TakeDamage(int damageAmount)
    {
        _health = Math.Max(_health - damageAmount, 0);
        if (_health <= 0)
        {
            OnDie();
        }
    }
}
