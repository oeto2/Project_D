using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Constants;
using System;

//데미지를 입게하는 인터페이스
public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class Enemy : MonoBehaviour, IDamagable
{
    [field: Header("References")]

    [SerializeField] private int _monsterID;

    //몬스터에 대한 데이터
    [field: SerializeField] public MonsterDBSheet MonstersDbSheet { get; private set; }

    [field: SerializeField] public MonsterData Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public EnemyForceReceiver ForceReceiver { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    public int EnemyPatrolLocation_number = 0;

    public EnemyStateMachine stateMachine;

    //몬스터 타입
    public MonsterType MonsterType = MonsterType.Normal;

    //몬스터 이동 타입 (고정 or 정찰)
    [field: SerializeField] public MonsterMoveType MonsterMoveType { get; private set; }
    public List<Transform> MonsterWanderDestination;

    //현재 이동할 목적지 좌표의 인덱스
    private int _curWanderDestination_index = 0;
    public GameObject enemyInteration_Object;
    public CharacterStats Health { get; private set; }
    //공격 대상 트랜스폼
    [field: SerializeField] public Transform Target { get; private set; }
    [HideInInspector] public CharacterStats _targetHealth;
    //데미지 UI 뜨는 곳
    public Transform HitUIPos;

    //스턴 할수 있는지 
    [SerializeField] private bool enableStiff = true;

    [field: Header("BossSkill")]
    public EnemySkillBase EnemySkill;

    #region 액션 이벤트
    public event Action<float> TakeDamageEvent;
    #endregion

    public int CurWanderDestination_index
    {
        get
        {
            //세팅된 마지막 좌표까지 이동했다면
            if (_curWanderDestination_index == (int)MonsterMoveType.Count)
            {
                //다시 처음 설정값으로 초기화
                _curWanderDestination_index = 0;
            }
            return _curWanderDestination_index;
        }
        set
        {
            _curWanderDestination_index = value;
        }
    }

    void Awake()
    {
        Data = Database.Monster.Get(_monsterID);
        stateMachine = new EnemyStateMachine(this);
        //애니메이션 데이터 할당
        AnimationData.Initialize();
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<EnemyForceReceiver>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Health = GetComponent<CharacterStats>();
        //순찰 장소
        //SetPatrolLocation(EnemyPatrolLocation_number);
        Health.InitHealth(Data.monsterHp);
        
        if(MonsterType == MonsterType.Boss)
            EnemySkill = GetComponent<EnemySkillBase>();
    }

    private void Start()
    {
        NavMeshAgent.speed = Data.monsterWalk;

        stateMachine.ChangeState(stateMachine.IdlingState);

        Health.OnDie += OnDie;

        //나중에는 수정하기
        Target = GameManager.Instance.playerObject.transform;
        _targetHealth = Target.GetComponent<CharacterStats>();
    }

    private void Update()
    {
        stateMachine.HandleInput();

        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    //데미지 받기
    public void TakePhysicalDamage(int damageAmount)
    {
        if (this.enabled == false)
            return;

        Health.TakePhysicalDamage(damageAmount);
        //Debug.Log(Health.health);
        
        //데미지 텍스트 띄우기
        GameObject hitTextObject = PoolManager.Instance.SpawnFromPool(nameof(HitDamageText));
        HitDamageText hitDamageText = hitTextObject.GetComponent<HitDamageText>();
        hitDamageText.SetHitDamageText(damageAmount);

        int randx = UnityEngine.Random.Range(-2, 3);

        hitTextObject.transform.position = HitUIPos.position + new Vector3(0, 1, 0.1f * randx);
        CallTakeDamageEvent(Health.health);

        if (enableStiff && Health.health > 0)
        {
            //일반 몬스터의 경우
            if(MonsterType == MonsterType.Normal)
            {
                stateMachine.ChangeState(stateMachine.StiffState);
                StartCoroutine(StiffStateDelay());
            }
            
            //보스 몬스터의 경우 스킬을 사용중이지 않을 때만 동작
            if(MonsterType == MonsterType.Boss && !EnemySkill.UsingSkill)
            {
                stateMachine.ChangeState(stateMachine.StiffState);
                StartCoroutine(StiffStateDelay());
            }
        }
    }

    void OnDie()
    {
        stateMachine.ChangeState(stateMachine.DeadState);
        Animator.SetTrigger(stateMachine.Enemy.AnimationData.DeadParameterHash);
        Controller.enabled = false;
        NavMeshAgent.enabled = false;
        ForceReceiver.enabled = false;
        enabled = false;
    }

    //경직상태 딜레이
    public IEnumerator StiffStateDelay()
    {
        enableStiff = false;

        //다시 경직 상태로 들어갈 수 있는 시간
        yield return new WaitForSeconds(3f);
        enableStiff = true;
    }

    //몬스터 피격 이벤트 호출
    public void CallTakeDamageEvent(float curHp_)
    {
        TakeDamageEvent?.Invoke(curHp_);
    }
}
