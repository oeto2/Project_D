using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkWarriorSkill : EnemySkillBase
{
    //��ũ ���� �����̾�
    [SerializeField] private Material _weaponMaterials;

    //������ ���� ��
    [SerializeField] private Color32 _warringColor;
    //�⺻ ��
    private Color32 _defaultColor = new Color32(255, 255, 255, 255);

    private void OnDisable()
    {
        _weaponMaterials.color = new Color32(255, 255, 255, 255);
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
        //���� �� ����
        _weaponMaterials.color = _warringColor;

        //����� ��ų ������
        EnemySkillData skillData = _skillData.skill01_Data;
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;
        //��ų ����ð�
        float skillTime = 0f;
        //������ ������ �ֱ�
        WaitForSeconds skillDamageCycle = new WaitForSeconds(skillData.SkillDamageCycle);

        //ȸ������ ���� ����
        while(true)
        {
            //Ÿ���� ��ġ
            Vector3 targetVec = GameManager.Instance.player.transform.position;
            IDamagable idamagable = GameManager.Instance.player.GetComponent<IDamagable>();

            //����ĳ��Ʈ ���
            Ray ray = new Ray(transform.position, (GameManager.Instance.player.transform.position - transform.position).normalized);
            RaycastHit hitData;
            Physics.Raycast(ray, out hitData, skillData.SkillRange);

            if (hitData.transform?.tag == "Player")
            {
                hitData.transform?.GetComponent<IDamagable>().TakePhysicalDamage(skillData.SkillDamage);
            }

            //��ų ���ӽð��� ������ Ż��
            if (skillTime >= skillData.SkillDurationTime)
                break;

            yield return skillDamageCycle;
            skillTime += skillData.SkillDamageCycle;
        }
        //���� �� ����
        _weaponMaterials.color = _defaultColor;

        UsingSkill = false;
        //�������� ����
        enemySateMachine.ChangeState(enemySateMachine.ChasingState);

        //��ų ��Ÿ�� ����
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill01Ready = true;

        //���� �� ����
        _weaponMaterials.color = _warringColor;
    }

    private IEnumerator StartDubleAttack()
    {
        //����� ��ų ������
        EnemySkillData skillData = _skillData.skill02_Data;
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        yield return new WaitForSeconds(skillData.SkillDurationTime);

        //�������� ����
        enemySateMachine.ChangeState(enemySateMachine.ChasingState);
        UsingSkill = false;

        //��ų ��Ÿ�� ����
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill02Ready = true;
    }
}