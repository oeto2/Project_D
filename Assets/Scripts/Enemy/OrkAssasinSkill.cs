using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkAssasinSkill : EnemySkillBase
{
    [SerializeField] private GameObject _buffParticle;
    private Animator _animator;

    private int bleedStack;
    private bool accel;

    WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _enemy.Health.OnDie += OnDie;
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
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;
        if (accel)
            yield return new WaitForSeconds(0.1f);
        else
            yield return new WaitForSeconds(0.05f);
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
        accel = true;
        _animator.speed += 0.5f;

        yield return new WaitForSeconds(duration);

        _buffParticle.SetActive(false);
        _animator.speed -= 0.5f;
        accel = false;
    }

    IEnumerator AssasinBleedSkill()
    {
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;
        if (accel)
            yield return new WaitForSeconds(0.1f);
        else
            yield return new WaitForSeconds(0.05f);
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

        while (duration > 0)
        {
            if (player.health > 0)
            {
                player.TakePhysicalDamage(bleedStack, true);
                duration -= 1;
                yield return _waitForSeconds;
            }
        }

        yield return null;
        bleedStack = 0;
        player.OnBleeding(false);
    }

    private void OnDie()
    {
        _buffParticle.SetActive(false);
    }
}
