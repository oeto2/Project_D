using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkWarriorSkill : EnemySkillBase
{
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
        //사용할 스킬 데이터
        EnemySkillData skillData = _skillData.skill01_Data;
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
            IDamagable idamagable = GameManager.Instance.player.GetComponent<IDamagable>();

            //레이캐스트 사용
            Ray ray = new Ray(transform.position, (GameManager.Instance.player.transform.position - transform.position).normalized);
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
        UsingSkill = false;
        //추적상태 진입
        enemySateMachine.ChangeState(enemySateMachine.ChasingState);

        //스킬 쿨타임 적용
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill01Ready = true;
    }

    private IEnumerator StartDubleAttack()
    {
        //사용할 스킬 데이터
        EnemySkillData skillData = _skillData.skill02_Data;
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        yield return new WaitForSeconds(skillData.SkillDurationTime);

        //추적상태 진입
        enemySateMachine.ChangeState(enemySateMachine.ChasingState);
        UsingSkill = false;

        //스킬 쿨타임 적용
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill02Ready = true;
    }
}
