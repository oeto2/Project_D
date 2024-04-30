using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Constants;
using Unity.VisualScripting;

public class NecromanserSkill : EnemySkillBase
{
    public GameObject explosionparticle;

    //몬스터를 생성시킬 좌표들
    [SerializeField] List<Vector3> spawnPos = new List<Vector3>();

    //임시로 좌표를 저장할 변수
    private Vector3 tempVec = Vector3.zero;

    //스킬 공격 코루틴
    private Coroutine _startSummons_Coroutine;
    private Coroutine _startExplosion_Coroutine;

    //폭발 공격 콜라이더
    Collider[] _explosionColider = new Collider[150];
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

                    _startSummons_Coroutine = StartCoroutine(StartSummons());
                }
                break;
            case 2:
                if (Skill02Ready)
                {
                    UsingSkill = true;
                    Skill02Ready = false;

                    _startExplosion_Coroutine = StartCoroutine(StartExplosion());
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

        //폭발 이펙트 보이기
        explosionparticle.SetActive(true);

        //스킬 시전시간 적용
        yield return new WaitForSeconds(skillData.SkillDurationTime);

        //충돌체크
        int numColiders = Physics.OverlapSphereNonAlloc(transform.position, skillData.SkillRange, _explosionColider);
        if (_explosionColider != null)
        {
            IDamagable iDamagable;
            for (int i=0; i< numColiders; i++)
            {
                bool isHave = _explosionColider[i].TryGetComponent(out iDamagable);
                if (isHave) { iDamagable.TakePhysicalDamage(skillData.SkillDamage); }
            }
        }
        else Debug.LogError("네크로맨서 폭발 공격 오류 : 콜라이더 감지 안됨");

        //스킬 이펙트 숨기기
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
        if (explosionparticle != null)
            explosionparticle.SetActive(false);

        if (_startExplosion_Coroutine != null)
            StopCoroutine(_startSummons_Coroutine);

        if (_startExplosion_Coroutine != null)
            StopCoroutine(_startExplosion_Coroutine);
    }
}
