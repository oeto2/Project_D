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
    public ForceReceiver ForceReceiver { get; private set; }
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
        ForceReceiver = GetComponent<ForceReceiver>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        //���� ���
        SetPatrolLocation(EnemyPatrolLocation_number);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
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
        Data.EnemyHealth -= damageAmount;

        if(Data.EnemyHealth <= 0)
        {
            Debug.Log("���� ���");
            stateMachine.ChangeState(stateMachine.DeadState);
            enemyInteration_Object.SetActive(true);
        }
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
