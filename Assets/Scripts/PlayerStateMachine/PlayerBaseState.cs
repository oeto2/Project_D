using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundedData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallback();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallback();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
        ReadLookInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
        Look();
        //Debug.Log(stateMachine.GetCurrentState());
    }

    protected virtual void AddInputActionsCallback()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.playerActions.Move.canceled += OnMoveCanceled;
        input.playerActions.Run.started += OnRunStarted;
        input.playerActions.Look.canceled += OnLookCanceled;

        input.playerActions.Jump.started += OnJumpStarted;

        input.playerActions.Attack.performed += OnAttackPerformed;
        input.playerActions.Attack.canceled += OnAttackCanceled;
        
        //stateMachine.Player.Input.playerActions.Potion.started += OnPotionStarted;
    }

    

    protected virtual void RemoveInputActionsCallback()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.playerActions.Move.canceled -= OnMoveCanceled;
        input.playerActions.Run.started -= OnRunStarted;

        input.playerActions.Jump.started -= OnJumpStarted;

        input.playerActions.Attack.performed -= OnAttackPerformed;
        input.playerActions.Attack.canceled -= OnAttackCanceled;
        
        //stateMachine.Player.Input.playerActions.Potion.started -= OnPotionStarted;
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {

    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;
    }

    private void OnPotionStarted(InputAction.CallbackContext context)
    {
        //GameManager.Instance._player.GetComponent<Items>().UsePotion();
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.playerActions.Move.ReadValue<Vector2>();
    }

    private void ReadLookInput()
    {
        stateMachine.LookInput = stateMachine.Player.Input.playerActions.Look.ReadValue<Vector2>();
    }

    private void Move()
    {
        //Vector3 movementDirection = GetMovementDirection() * GetMovementSpeed();
        //if (OnSlope())
        //{
        //    stateMachine.Player.Rigidbody.velocity = (GetSlopeMoveDirection(movementDirection) + stateMachine.Player.ForceReceiver.Movement);
        //}
        //else
        //{
        //    stateMachine.Player.Rigidbody.velocity = (movementDirection + stateMachine.Player.ForceReceiver.Movement);
        //}
        Vector3 destination = stateMachine.Player.transform.position + GetMovementDirection();
        stateMachine.Player.NavMeshAgent.destination = destination;
        //stateMachine.Player.NavMeshAgent.SetDestination(destination);
    }

    protected void Look()
    {
        var controller = stateMachine.Player.Controller;
        controller.camCurXRot += stateMachine.LookInput.y * controller.lookSensitivity;
        controller.camCurXRot = Mathf.Clamp(controller.camCurXRot, controller.minXLook, controller.maxXLook);
        stateMachine.MainCameraTransform.localEulerAngles = new Vector3(-controller.camCurXRot, 0, 0);

        Transform playerTransform = stateMachine.Player.transform;
        playerTransform.eulerAngles += new Vector3(0, stateMachine.LookInput.x * controller.lookSensitivity, 0) ;
    }

    protected void ForceMove()
    {
       //stateMachine.Player.(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }

    protected Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.Player.transform.forward;
        Vector3 right = stateMachine.Player.transform.right;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
    // Áö¿ì±â
    protected bool isGround()
    {
        var transform = stateMachine.Player.transform;

        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f) , Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+ (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, stateMachine.Player.Controller.groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
}