using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkAssasinSkill : EnemySkillBase
{
    [SerializeField] private GameObject _buffParticle;
    private Animator _animator;

    private int bleedStack;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponentInChildren<Animator>();
    }

    public override void UseSkill(int skillNum_)
    {
        switch (skillNum_)
        {
            case 1:
                if (Skill01Ready)
                {
                    UsingSkill = true;
                    Skill01Ready = false;

                    StartCoroutine(AssasinBuffSkill());
                }
                break;

            case 2:
                if (Skill02Ready)
                {
                    UsingSkill = true;
                    Skill02Ready = false;

                    StartCoroutine(AssasinBleedSkill());
                }
                break;
        }
    }

    IEnumerator AssasinBuffSkill()
    {
        yield return new WaitForSeconds(0.2f);
        EnemySkillData skillData = _skillData.skill_Data[0];

        Vector3 targetVec = GameManager.Instance.player.transform.position;
        IDamagable idamagable = GameManager.Instance.player.GetComponent<IDamagable>();

        //레이캐스트 사용
        Ray ray = new Ray(transform.position, (GameManager.Instance.player.transform.position - transform.position).normalized);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData, skillData.SkillRange);

        if (hitData.transform?.tag == "Player")
        {
            hitData.transform?.GetComponent<IDamagable>().TakePhysicalDamage((int)(skillData.SkillDamage * _enemy.Data.monsterAtk / 100));

            if (hitData.transform?.GetComponent<CharacterStats>() != null)
            {
                hitData.transform?.GetComponent<CharacterStats>().ChangeManaAction(-20);
                StartCoroutine(AssasinOnBuff(skillData.SkillDurationTime));
            }
        }
        UsingSkill = false; 

        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill01Ready = true;
    }

    IEnumerator AssasinOnBuff(float duration)
    {
        _buffParticle.SetActive(true);
        _animator.speed += 0.5f;

        yield return new WaitForSeconds(duration);

        _buffParticle.SetActive(false);
        _animator.speed -= 0.5f;
    }

    IEnumerator AssasinBleedSkill()
    {
        yield return new WaitForSeconds(0.2f);
        EnemySkillData skillData = _skillData.skill_Data[1];

        Vector3 targetVec = GameManager.Instance.player.transform.position;
        IDamagable idamagable = GameManager.Instance.player.GetComponent<IDamagable>();

        //레이캐스트 사용
        Ray ray = new Ray(transform.position, (GameManager.Instance.player.transform.position - transform.position).normalized);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData, skillData.SkillRange);

        if (hitData.transform?.tag == "Player")
        {
            hitData.transform?.GetComponent<IDamagable>().TakePhysicalDamage((int)(skillData.SkillDamage * _enemy.Data.monsterAtk / 100));

            if (hitData.transform?.GetComponent<CharacterStats>() != null)
            {
                CharacterStats player = hitData.transform?.GetComponent<CharacterStats>();
                StopCoroutine(OnBleed(player, skillData.SkillDurationTime));
                StartCoroutine(OnBleed(player, skillData.SkillDurationTime));
            }
        }
        UsingSkill = false;

        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill02Ready = true;
    }

    IEnumerator OnBleed(CharacterStats player, float duration)
    {
        player.OnBleeding(true);
        bleedStack += 9;
        float time = 0;

        while (duration > 0)
        {
            time += Time.deltaTime;
            if (time > 1)
            {
                player.TakePhysicalDamage(bleedStack, true);
                duration -= 1;
                time = 0;
            }
        }

        yield return null;
        bleedStack = 0;
        player.OnBleeding(false);
    }
}
