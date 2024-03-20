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

    public Health Health { get; private set; }

    public PlayerStateMachine stateMachine;
    public Transform playerTransform;

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInputs>();
        //Rigidbody = GetComponent<Rigidbody>();  
        //ForceReceiver = GetComponent<ForceReceiver>();
        Controller = GetComponent<PlayerController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Health = GetComponent<Health>();
        PlayerController = GetComponent<PlayerController>();
        playerTransform = GetComponent<Transform>();

        stateMachine = new PlayerStateMachine(this);
        Health.maxHealth = Data.Health;
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

    public void TakePhysicalDamage(int damageAmount)
    {
        Health.TakePhysicalDamage(damageAmount);
    }
}