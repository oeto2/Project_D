using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Constants;

//�������� �԰��ϴ� �������̽�
public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class Enemy : MonoBehaviour, IDamagable
{
    [field: Header("References")]

    //���Ϳ� ���� ������
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public EnemyForceReceiver ForceReceiver { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    [field : SerializeField] private int EnemyPatrolLocation_number;

    private EnemyStateMachine stateMachine;

    //���� �̵� Ÿ�� (���� or ����)
    [field: SerializeField] public MonsterMoveType MonsterMoveType { get; private set; }
    public List<Transform> MonsterWanderDestination;

    //���� �̵��� ������ ��ǥ�� �ε���
    private int _curWanderDestination_index = 0;

    [SerializeField] private GameObject enemyInteration_Object;
    public Health Health { get; private set; }
    [HideInInspector] public Health _targetHealth;

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
        stateMachine = new EnemyStateMachine(this);
        //�ִϸ��̼� ������ �Ҵ�
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<EnemyForceReceiver>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Health = GetComponent<Health>();
        //���� ���
        SetPatrolLocation(EnemyPatrolLocation_number);
        Health.InitHealth(Data.Health);
        _targetHealth = stateMachine.Target.GetComponent<Health>();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);

        Health.OnDie += OnDie;
    }

    private void Update()
    {
        stateMachine.HandleInput();

        stateMachine.Update();

        if(Input.GetKeyDown(KeyCode.Z))
        {
            TakePhysicalDamage(100);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    //������ �ޱ�
    public void TakePhysicalDamage(int damageAmount)
    {
        Health.TakePhysicalDamage(damageAmount);
    }

    void OnDie()
    {
        stateMachine.ChangeState(stateMachine.DeadState);
        Animator.SetTrigger(stateMachine.Enemy.AnimationData.DeadParameterHash);
    }

    //���Ͱ� ������ ��ǥ ����
    public void SetPatrolLocation(int index)
    {
        switch(index)
        {
            case 1:
                //���� ���� ��ġ �����Ҵ�
                MonsterWanderDestination.Add(ResourceManager.Instance.Instantiate("Map/WayPoint0").transform);
                MonsterWanderDestination.Add(ResourceManager.Instance.Instantiate("Map/WayPoint1").transform);
                break;

            case 2:
                //���� ���� ��ġ �����Ҵ�
                MonsterWanderDestination.Add(ResourceManager.Instance.Instantiate("Map/WayPoint2").transform);
                MonsterWanderDestination.Add(ResourceManager.Instance.Instantiate("Map/WayPoint3").transform);
                break;
        }
    }
}
