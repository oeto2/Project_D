using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class NecromanserSkill : EnemySkillBase
{
    public GameObject explosionparticle;

    //몬스터를 생성시킬 좌표들
    [SerializeField] List<Vector3> spawnPos = new List<Vector3>();
    
    //임시로 좌표를 저장할 변수
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
        //사용할 스킬 데이터
        EnemySkillData skillData = _skillData.skill_Data[0];
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        //몬스터 정지
        enemySateMachine.MovementSpeedModifier = 0f;

        //랜덤 좌표에 몬스터 소환 준비
        for (int i = 0; i < 3; i++)
        {
            //네브메쉬 안에 이동가능한 랜덤 좌표
            RandomPoint(transform.position, skillData.SkillRange, out tempVec);
            spawnPos.Add(tempVec);
            tempVec.y = 0;

            //랜덤 좌표값에 소환진 발동
            PoolManager.Instance.SpawnFromPool("MonsterSpawnEffect").transform.position = spawnPos[i];
        }

        yield return new WaitForSeconds(skillData.SkillDurationTime);

        //몬스터 소환
        for (int i = 0; i < spawnPos.Count; i++)
        {
            //랜덤 좌표값에 스켈레톤 소환
            ResourceManager.Instance.Instantiate("Monster/SKELETON2").transform.position = spawnPos[i];
        }
        spawnPos.Clear();
        UsingSkill = false;

        //스킬 쿨타임 적용
        yield return new WaitForSeconds(skillData.SkillCollTime);
        Skill01Ready = true;
    }


    private IEnumerator StartExplosion()
    {
        //사용할 스킬 데이터
        EnemySkillData skillData = _skillData.skill_Data[1];
        EnemyStateMachine enemySateMachine = _enemy.stateMachine;

        //몬스터 정지
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

        //스킬 쿨타임 적용
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
