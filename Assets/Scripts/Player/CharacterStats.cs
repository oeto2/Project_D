using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float maxMana;
    public float mana;
    public float maxStamina;
    public float stamina;

    public float attack;
    public float defense;

    public float curDefense;

    public bool IsDead {get; private set; }

    public event Action<int> OnDamage;
    public event Action OnDie;
    public event Action<float> OnMana;
    public event Action<float> OnStamina;

    private void Awake()
    {
        OnDamage += TakeDamage;
        OnMana += ChangeMana;
        OnStamina += ChangeStamina;
    }

    public void Init(PlayerSO playerSO)
    {
        InitHealth(playerSO.Health);
        InitMana(playerSO.Mana);
        InitStamina(playerSO.Stamina);
        InitBaseStats(playerSO);

        foreach (var item in InformationManager.Instance.saveLoadData.equipmentItems)
        {
            if (item.Value != 0)
            {
                EquipItem(Database.Item.Get(item.Value));
            }
        }
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

    public void InitBaseStats(PlayerSO playerSO)
    {
        attack = playerSO.Attack;
        defense = playerSO.Defense;
        curDefense = defense;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        OnDamage((int)MathF.Max((damageAmount - (int)curDefense), 1));
    }

    private void TakeDamage(int damageAmount)
    {
        if (damageAmount > 0)
            SoundManager.Instance.PlayHitSound(0);

        health = Math.Max(health - damageAmount, 0);
        if (health <= 0)
        {
            OnDie?.Invoke();
            IsDead = true;
        }
    }

    public void ChangeManaAction(float amount)
    {
        OnMana(amount);
    }

    private void ChangeMana(float amount)
    {
        if (amount >= 0)
            mana = MathF.Min(mana + amount, maxMana);
        else
        {
            mana = MathF.Max(mana + amount, 0);
            if (mana <= 0)
            {
                // 마나 0 됐을 때 할 일
            }
        }
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

    public void UsePotion(ItemData potion)
    {
        OnDamage(-potion.itemHpRecover);
        OnMana(potion.itemMpRecover);
    }

    public void EquipItem(ItemData item)
    {
        if (item == null)
            return;
        attack += item.itemAtk;
        curDefense += defense * item.itemDef / 100;
    }

    public void UnEquipItem(ItemData item)
    {
        if (item == null)
            return;
        attack -= item.itemAtk;
        curDefense -= defense * item.itemDef / 100;
    }
}
