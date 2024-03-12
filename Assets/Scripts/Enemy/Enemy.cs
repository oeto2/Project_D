using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Constants;

public class Enemy : MonoBehaviour
{
    [field: Header("References")]

    //몬스터에 대한 데이터
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    private EnemyStateMachine stateMachine;

    //몬스터 이동 타입 (고정 or 정찰)
    [field: SerializeField] public MonsterMoveType MonsterMoveType { get; private set; }
    public List<Vector3> MonsterWanderDestination;

    //현재 이동할 목적지 좌표의 인덱스
    private int _curWanderDestination_index = 0;

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
        //애니메이션 데이터 할당
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
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
}
