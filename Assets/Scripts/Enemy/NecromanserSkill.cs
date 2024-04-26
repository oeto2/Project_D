using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class NecromanserSkill : EnemySkillBase
{
    public GameObject explosionparticle;

    //���͸� ������ų ��ǥ��
    [SerializeField] List<Vector3> spawnPos = new List<Vector3>();
    
    //�ӽ÷� ��ǥ�� ������ ����
    private Vector3 tempVec = Vector3.zero;

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
        EnemySkillData skillData = _skillData.skill_Data[0];
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        //���� ����
        enemySateMachine.MovementSpeedModifier = 0f;

        //���� ��ǥ�� ���� ��ȯ �غ�
        for (int i = 0; i < 3; i++)
        {
            //�׺�޽� �ȿ� �̵������� ���� ��ǥ
            RandomPoint(transform.position, skillData.SkillRange, out tempVec);
            spawnPos.Add(tempVec);
            tempVec.y = 0;

            //���� ��ǥ���� ��ȯ�� �ߵ�
            PoolManager.Instance.SpawnFromPool("MonsterSpawnEffect").transform.position = spawnPos[i];
        }

        yield return new WaitForSeconds(skillData.SkillDurationTime);

        //���� ��ȯ
        for (int i = 0; i < spawnPos.Count; i++)
        {
            //���� ��ǥ���� ���̷��� ��ȯ
            ResourceManager.Instance.Instantiate("Monster/SKELETON2").transform.position = spawnPos[i];
        }
        spawnPos.Clear();
        UsingSkill = false;

        //��ų ��Ÿ�� ����
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill01Ready = true;
    }


    private IEnumerator StartExplosion()
    {
        //����� ��ų ������
        EnemySkillData skillData = _skillData.skill_Data[1];
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        //���� ����
        enemySateMachine.MovementSpeedModifier = 0f;

        explosionparticle.SetActive(true);
        yield return new WaitForSeconds(skillData.SkillDurationTime);

        Collider[] colider = Physics.OverlapSphere(transform.position, 10.5f);
        foreach(Collider col in colider)
        {
            if(col.gameObject.layer == 7)
            {
                col.GetComponent<IDamagable>().TakePhysicalDamage(skillData.SkillDamage);
            }
        }
        explosionparticle.SetActive(false);
        UsingSkill = false;

        //��ų ��Ÿ�� ����
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill02Ready = true;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    private void OnDie()
    {
        StopCoroutine(StartSummons());
        StopCoroutine(StartExplosion());
    }
}
