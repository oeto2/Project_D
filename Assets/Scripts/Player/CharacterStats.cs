using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float maxMana;
    public float mana;
    public float maxStamina;
    public float stamina;

    public bool IsDead {get; private set; }

    public event Action<int> OnDamage;
    public event Action OnDie;
    public event Action<float> OnMana;
    public event Action<float> OnStamina;

    private void Awake()
    {
        OnDamage += TakeDamage;
        OnStamina += ChangeStamina;
    }

    public void InitHealth(float amount)
    {
        maxHealth = amount;
        health = maxHealth;
    }
    public void InitMana(float amount)
    {
        maxMana = amount;
        mana = maxMana;
    }
    public void InitStamina(float amount)
    {
        maxStamina = amount;
        stamina = maxStamina;
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

    public void ChangeManaAction(float amount)
    {

    }

    public void ChangeStaminaAction(float amount)
    {   
        OnStamina(amount);
    }

    private void ChangeStamina(float amount)
    {
        if (amount >= 0)
            stamina = MathF.Min(stamina + amount, maxStamina);
        else
        {
            stamina = MathF.Max(stamina + amount, 0);
            if (stamina <= 0)
            {
                // 스태미나 0 됐을 때 할 일
            }
        }
    }
}
