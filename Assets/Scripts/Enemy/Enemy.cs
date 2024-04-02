using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Constants;

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

    [field: SerializeField] private int EnemyPatrolLocation_number;

    private EnemyStateMachine stateMachine;

    //몬스터 이동 타입 (고정 or 정찰)
    [field: SerializeField] public MonsterMoveType MonsterMoveType { get; private set; }
    public List<Transform> MonsterWanderDestination;

    //현재 이동할 목적지 좌표의 인덱스
    private int _curWanderDestination_index = 0;

    public GameObject enemyInteration_Object;
    public Health Health { get; private set; }
    [HideInInspector] public Health _targetHealth;

    //공격 대상 트랜스폼
    [field: SerializeField] public Transform Target { get; private set; }

    //스턴 할수 있는지 
    [SerializeField] private bool enableStiff = true;

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
        Health = GetComponent<Health>();
        //순찰 장소
        SetPatrolLocation(EnemyPatrolLocation_number);
        Health.InitHealth(Data.monsterHp);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);

        Health.OnDie += OnDie;

        //나중에는 수정하기
        Target = GameManager.Instance.playerObject.transform;
        _targetHealth = Target.GetComponent<Health>();
    }

    private void Update()
    {
        stateMachine.HandleInput();

        stateMachine.Update();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakePhysicalDamage(100);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    //데미지 받기
    public void TakePhysicalDamage(int damageAmount)
    {
        Health.TakePhysicalDamage(damageAmount);

        //피격시 경직상태 진입
        if (enableStiff)
        {
            stateMachine.ChangeState(stateMachine.StiffState);
            StartCoroutine(StiffStateDelay());
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

    //몬스터가 순찰할 좌표 설정
    public void SetPatrolLocation(int index)
    {
        switch (index)
        {
            case 1:
                //몬스터 순찰 위치 동적할당
                MonsterWanderDestination.Add(ResourceManager.Instance.Instantiate("Map/WayPoint0").transform);
                MonsterWanderDestination.Add(ResourceManager.Instance.Instantiate("Map/WayPoint1").transform);
                break;

            case 2:
                //몬스터 순찰 위치 동적할당
                MonsterWanderDestination.Add(ResourceManager.Instance.Instantiate("Map/WayPoint2").transform);
                MonsterWanderDestination.Add(ResourceManager.Instance.Instantiate("Map/WayPoint3").transform);
                break;
        }
    }

    //경직상태 딜레이
    public IEnumerator StiffStateDelay()
    {
        enableStiff = false;

        //다시 경직 상태로 들어갈 수 있는 시간
        yield return new WaitForSeconds(3f);
        enableStiff = true;
    }
}
