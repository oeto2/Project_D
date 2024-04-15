using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    [field: SerializeField] public GameObject DefenseObj { get; private set; }

    public CharacterStats Stats { get; private set; }
    public PlayerSkills PlayerSkills { get; private set; }

    public ClassData Datas { get; private set; }

    public PlayerStateMachine stateMachine;
    public Transform playerTransform;
    public InteractionSystem InteractionSystem;

    public Vector3 beforeTrans;


    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInputs>();
        Controller = GetComponent<PlayerController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Stats = GetComponent<CharacterStats>();
        PlayerSkills = GetComponentInChildren<PlayerSkills>();
        PlayerController = GetComponent<PlayerController>();
        playerTransform = GetComponent<Transform>();

        stateMachine = new PlayerStateMachine(this);
        InteractionSystem = GetComponent<InteractionSystem>();

        Stats.Init(Data);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);

        Stats.OnDie += OnDie;
        Stats.OnDie += OnDieCameraView;
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
        stateMachine.ChangeState(stateMachine.DieState);
        Animator.SetTrigger(stateMachine.Player.AnimationData.DieParameterHash);
    }

    void OnDieCameraView()
    {
        Camera cam = Camera.main;
        cam.cullingMask = -1;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        Stats.TakePhysicalDamage(damageAmount);
    }
}