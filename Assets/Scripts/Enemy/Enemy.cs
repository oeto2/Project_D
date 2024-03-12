using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Constants;

public class Enemy : MonoBehaviour
{
    [field: Header("References")]

    //���Ϳ� ���� ������
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    private EnemyStateMachine stateMachine;

    //���� �̵� Ÿ�� (���� or ����)
    [field: SerializeField] public MonsterMoveType MonsterMoveType { get; private set; }
    public List<Vector3> MonsterWanderDestination;

    //���� �̵��� ������ ��ǥ�� �ε���
    private int _curWanderDestination_index = 0;

    public int CurWanderDestination_index
    {
        get
        {
            //���õ� ������ ��ǥ���� �̵��ߴٸ�
            if (_curWanderDestination_index == (int)MonsterMoveType.Count)
            {
                //�ٽ� ó�� ���������� �ʱ�ȭ
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
        //�ִϸ��̼� ������ �Ҵ�
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
