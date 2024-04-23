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
        //����� ��ų ������
        EnemySkillData skillData = _skillData.skill01_Data;
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        yield return new WaitForSeconds(skillData.SkillDurationTime);

        //�������� ����
        enemySateMachine.ChangeState(enemySateMachine.ChasingState);
        UsingSkill = false;

        //��ų ��Ÿ�� ����
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill01Ready = true;
    }
    private IEnumerator StartExplosion()
    {
        //����� ��ų ������
        EnemySkillData skillData = _skillData.skill02_Data;
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        explosionparticle.SetActive(true);
        yield return new WaitForSeconds(skillData.SkillDurationTime);

        //�������� ����
        enemySateMachine.ChangeState(enemySateMachine.ChasingState);
        explosionparticle.SetActive(false);
        UsingSkill = false;

        //��ų ��Ÿ�� ����
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill02Ready = true;
    }
}
