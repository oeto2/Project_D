using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class Player : MonoBehaviour, IDamagable
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    //public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputs Input { get; private set; }
    public PlayerController Controller { get; private set; }
    //public ForceReceiver ForceReceiver { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public PlayerController PlayerController { get; private set; }

    // [field: SerializeField] public Weapon Weapon { get; private set; }

    //public Health Health { get; private set; }

    public PlayerStateMachine stateMachine;
    public Transform playerTransform;

    //�÷��̾ ������ �޾��� ��
    public event Action TakeDamageEvent;

    //�÷��̾ �׾��� ��
    public event Action PlayerDieEvent;

    [Header("PlayerState")]
    [SerializeField] private float _playerMaxHp;
    [SerializeField] private float _playerCurHp;
    [SerializeField] private float _playerMaxMp;
    [SerializeField] private float _playerCurMp;

    public float PlayerMaxHp
    {
        get { return _playerMaxHp; }
        set { _playerMaxHp = value;}
    }

    public float PlayerCurHp
    {
        get { return _playerCurHp; }
        set { _playerCurHp = value; }
    }

    public float PlayerMaxMp
    {
        get { return _playerMaxMp; }
        set { _playerMaxMp = value; }
    }

    public float PlayerCurMp
    {
        get { return _playerCurMp; }
        set { _playerCurMp = value; }
    }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInputs>();
        //Rigidbody = GetComponent<Rigidbody>();  
        //ForceReceiver = GetComponent<ForceReceiver>();
        Controller = GetComponent<PlayerController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        //Health = GetComponent<Health>();
        PlayerController = GetComponent<PlayerController>();
        playerTransform = GetComponent<Transform>();

        stateMachine = new PlayerStateMachine(this);
        PlayerMaxHp = Data.Health;
        PlayerCurHp = Data.Health;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);

        //Health.OnDie += OnDie;
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

    void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }

    private void CallTakeDamageEvent() => TakeDamageEvent?.Invoke();
    private void CallPlayerDieEvent() => PlayerDieEvent?.Invoke();

    public void TakePhysicalDamage(int damageAmount)
    {
        PlayerCurHp -= damageAmount;
        Debug.Log($"���� �÷��̾� ü�� : {PlayerCurHp}");
        if (PlayerCurHp <= 0)
        {
            // �׾��� �� �̺�Ʈ �׼����� ���߿� �ٲٱ�
            stateMachine.ChangeState(stateMachine.DieState);
            Animator.SetTrigger(stateMachine.Player.AnimationData.DieParameterHash);
            CallPlayerDieEvent();
        }
        CallTakeDamageEvent();
    }
}