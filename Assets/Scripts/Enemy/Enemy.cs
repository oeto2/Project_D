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

    [SerializeField] private int _monsterID;

    //���Ϳ� ���� ������
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

    //���� �̵� Ÿ�� (���� or ����)
    [field: SerializeField] public MonsterMoveType MonsterMoveType { get; private set; }
    public List<Transform> MonsterWanderDestination;

    //���� �̵��� ������ ��ǥ�� �ε���
    private int _curWanderDestination_index = 0;

    public GameObject enemyInteration_Object;
    public Health Health { get; private set; }
    [HideInInspector] public Health _targetHealth;

    //���� ��� Ʈ������
    [field: SerializeField] public Transform Target { get; private set; }

    //���� �Ҽ� �ִ��� 
    [SerializeField] private bool enableStiff = true;

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
        Data = Database.Monster.Get(_monsterID);
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
        Health.InitHealth(Data.monsterHp);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);

        Health.OnDie += OnDie;

        //���߿��� �����ϱ�
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

    //������ �ޱ�
    public void TakePhysicalDamage(int damageAmount)
    {
        Health.TakePhysicalDamage(damageAmount);

        //�ǰݽ� �������� ����
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

    //���Ͱ� ������ ��ǥ ����
    public void SetPatrolLocation(int index)
    {
        switch (index)
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

    //�������� ������
    public IEnumerator StiffStateDelay()
    {
        enableStiff = false;

        //�ٽ� ���� ���·� �� �� �ִ� �ð�
        yield return new WaitForSeconds(3f);
        enableStiff = true;
    }
}
