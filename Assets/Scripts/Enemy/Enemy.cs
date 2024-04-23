using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Constants;
using System;

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

    public int EnemyPatrolLocation_number = 0;

    public EnemyStateMachine stateMachine;

    //���� Ÿ��
    public MonsterType MonsterType = MonsterType.Normal;

    //���� �̵� Ÿ�� (���� or ����)
    [field: SerializeField] public MonsterMoveType MonsterMoveType { get; private set; }
    public List<Transform> MonsterWanderDestination;

    //���� �̵��� ������ ��ǥ�� �ε���
    private int _curWanderDestination_index = 0;
    public GameObject enemyInteration_Object;
    public CharacterStats Health { get; private set; }
    //���� ��� Ʈ������
    [field: SerializeField] public Transform Target { get; private set; }
    [HideInInspector] public CharacterStats _targetHealth;
    //������ UI �ߴ� ��
    public Transform HitUIPos;

    //���� �Ҽ� �ִ��� 
    [SerializeField] private bool enableStiff = true;

    [field: Header("BossSkill")]
    public EnemySkillBase EnemySkill;

    #region �׼� �̺�Ʈ
    public event Action<float> TakeDamageEvent;
    #endregion

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
        Health = GetComponent<CharacterStats>();
        //���� ���
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

        //���߿��� �����ϱ�
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

    //������ �ޱ�
    public void TakePhysicalDamage(int damageAmount)
    {
        if (this.enabled == false)
            return;

        Health.TakePhysicalDamage(damageAmount);
        //Debug.Log(Health.health);
        
        //������ �ؽ�Ʈ ����
        GameObject hitTextObject = PoolManager.Instance.SpawnFromPool(nameof(HitDamageText));
        HitDamageText hitDamageText = hitTextObject.GetComponent<HitDamageText>();
        hitDamageText.SetHitDamageText(damageAmount);

        int randx = UnityEngine.Random.Range(-2, 3);

        hitTextObject.transform.position = HitUIPos.position + new Vector3(0, 1, 0.1f * randx);
        CallTakeDamageEvent(Health.health);

        if (enableStiff && Health.health > 0)
        {
            //�Ϲ� ������ ���
            if(MonsterType == MonsterType.Normal)
            {
                stateMachine.ChangeState(stateMachine.StiffState);
                StartCoroutine(StiffStateDelay());
            }
            
            //���� ������ ��� ��ų�� ��������� ���� ���� ����
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

    //�������� ������
    public IEnumerator StiffStateDelay()
    {
        enableStiff = false;

        //�ٽ� ���� ���·� �� �� �ִ� �ð�
        yield return new WaitForSeconds(3f);
        enableStiff = true;
    }

    //���� �ǰ� �̺�Ʈ ȣ��
    public void CallTakeDamageEvent(float curHp_)
    {
        TakeDamageEvent?.Invoke(curHp_);
    }
}
