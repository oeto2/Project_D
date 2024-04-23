using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanserSkill : EnemySkillBase
{
    public GameObject explosionparticle;
    public override void UseSkill(int skillNum_)
    {
        switch (skillNum_)
        {
            case 1:
                if (Skill01Ready)
                {
                    UsingSkill = true;
                    Skill01Ready = false;

                    StartCoroutine(StartSummons());
                }
                break;
            case 2:
                if (Skill02Ready)
                {
                    UsingSkill = true;
                    Skill02Ready = false;

                    StartCoroutine(StartExplosion());
                }
                break;
        }
    }

    private IEnumerator StartSummons()
    {
        //사용할 스킬 데이터
        EnemySkillData skillData = _skillData.skill01_Data;
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        yield return new WaitForSeconds(skillData.SkillDurationTime);

        //추적상태 진입
        enemySateMachine.ChangeState(enemySateMachine.ChasingState);
        UsingSkill = false;

        //스킬 쿨타임 적용
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill01Ready = true;
    }
    private IEnumerator StartExplosion()
    {
        //사용할 스킬 데이터
        EnemySkillData skillData = _skillData.skill02_Data;
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        explosionparticle.SetActive(true);
        yield return new WaitForSeconds(skillData.SkillDurationTime);

        //추적상태 진입
        enemySateMachine.ChangeState(enemySateMachine.ChasingState);
        explosionparticle.SetActive(false);
        UsingSkill = false;

        //스킬 쿨타임 적용
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill02Ready = true;
    }
}
