using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkWarriorSkill : EnemySkillBase
{
    //오크 무기 머테이얼
    [SerializeField] private Material _weaponMaterials;

    //변경할 무기 색
    [SerializeField] private Color32 _warringColor;
    //기본 색
    private Color32 _defaultColor = new Color32(255, 255, 255, 255);

    private void OnDisable()
    {
        _weaponMaterials.color = new Color32(255, 255, 255, 255);
    }

    private void Start()
    {
        _enemy.Health.OnDie += OnDie;
    }


    public override void UseSkill(int skillNum_)
    {
        switch(skillNum_)
        {
            case 1:
                if (Skill01Ready)
                {
                    UsingSkill = true;
                    Skill01Ready = false;

                    StartCoroutine(StartRotateAttack());
                }
                break;

            case 2:
                if (Skill02Ready)
                {
                    UsingSkill = true;
                    Skill02Ready = false;

                    StartCoroutine(StartDubleAttack());
                }
                break;
        }
    }

    private IEnumerator StartRotateAttack()
    {
        //무기 색 변경
        _weaponMaterials.color = _warringColor;

        //사용할 스킬 데이터
        EnemySkillData skillData = _skillData.skill_Data[0];
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;
        //스킬 진행시간
        float skillTime = 0f;
        //데미지 입히는 주기
        WaitForSeconds skillDamageCycle = new WaitForSeconds(skillData.SkillDamageCycle);

        //회전베기 공격 로직
        while(true)
        {
            //타겟의 위치
            Vector3 targetVec = GameManager.Instance.player.transform.position;

            //레이캐스트 사용
            Ray ray = new Ray(transform.position, (targetVec - transform.position).normalized);
            RaycastHit hitData;
            Physics.Raycast(ray, out hitData, skillData.SkillRange);

            if (hitData.transform?.tag == "Player")
            {
                hitData.transform?.GetComponent<IDamagable>().TakePhysicalDamage(skillData.SkillDamage);
            }

            //스킬 지속시간이 끝나면 탈출
            if (skillTime >= skillData.SkillDurationTime)
                break;

            yield return skillDamageCycle;
            skillTime += skillData.SkillDamageCycle;
        }
        //무기 색 변경
        _weaponMaterials.color = _defaultColor;

        UsingSkill = false;
        //추적상태 진입
        enemySateMachine.ChangeState(enemySateMachine.ChasingState);

        //스킬 쿨타임 적용
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill01Ready = true;

        //무기 색 변경
        _weaponMaterials.color = _warringColor;
    }

    private IEnumerator StartDubleAttack()
    {
        //사용할 스킬 데이터
        EnemySkillData skillData = _skillData.skill_Data[1];
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        yield return new WaitForSeconds(skillData.SkillDurationTime);
        UsingSkill = false;

        //스킬 쿨타임 적용
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill02Ready = true;
    }

    private void OnDie()
    {
        StopCoroutine(StartRotateAttack());
    }
}
