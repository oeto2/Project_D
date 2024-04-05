using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

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
        if (Cursor.lockState != CursorLockMode.None)
        {
            Move();
            Look();
        }
        if (stateMachine.SkillIndex == 0)
        {
            stateMachine.Player.Stats.ChangeManaAction(0.1f * Time.deltaTime);
        }
    }

    protected virtual void AddInputActionsCallback()
    {
        PlayerInputs input = stateMachine.Player.Input;
        input.playerActions.Move.canceled += OnMoveCanceled;
        input.playerActions.Run.started += OnRunStarted;
        input.playerActions.Look.canceled += OnLookCanceled;

        input.playerActions.Jump.started += OnJumpStarted;

        input.playerActions.Attack.performed += OnAttackPerformed;
        input.playerActions.Attack.canceled += OnAttackCanceled;

        input.playerActions.Interaction.started += OnInteractionStarted;
        input.playerActions.Interaction.canceled += OnInteractionCanceled;

        input.playerActions.Inventory.started += OnInventoryStarted;
        input.playerActions.Equip.started += OnEquipStarted;

        input.playerActions.Defense.performed += OnDefensePerformed;
        input.playerActions.Defense.canceled += OnDefenseCanceled;

        input.playerActions.Skill1.performed += OnSkill1Performed;
        input.playerActions.Skill2.performed += OnSkill2Performed;
    }


    protected virtual void RemoveInputActionsCallback()
    {
        PlayerInputs input = stateMachine.Player.Input;
        input.playerActions.Move.canceled -= OnMoveCanceled;
        input.playerActions.Run.started -= OnRunStarted;

        input.playerActions.Jump.started -= OnJumpStarted;

        input.playerActions.Attack.performed -= OnAttackPerformed;
        input.playerActions.Attack.canceled -= OnAttackCanceled;

        input.playerActions.Interaction.started -= OnInteractionStarted;
        input.playerActions.Interaction.canceled -= OnInteractionCanceled;

        input.playerActions.Inventory.started -= OnInventoryStarted;
        input.playerActions.Equip.started -= OnEquipStarted;

        input.playerActions.Defense.performed -= OnDefensePerformed;
        input.playerActions.Defense.canceled -= OnDefenseCanceled;

        input.playerActions.Skill1.performed -= OnSkill1Performed;
        input.playerActions.Skill2.performed -= OnSkill2Performed;
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
        if (Cursor.lockState != CursorLockMode.None && stateMachine.Player.Stats.stamina >= 5f)
            stateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;
    }

    private void OnInteractionStarted(InputAction.CallbackContext context)
    {
        stateMachine.Player.InteractionSystem.isInteract = true;
    }

    private void OnInteractionCanceled(InputAction.CallbackContext context)
    {
        stateMachine.Player.InteractionSystem.isInteract = false;
        stateMachine.Player.InteractionSystem.curInteractable?.CancelInteract();
    }

    private void OnInventoryStarted(InputAction.CallbackContext context)
    {
        GameObject inventoryPopup = UIManager.Instance.GetPopupObject(nameof(InventoryPopup));

        if(inventoryPopup != null)
        {
            if (inventoryPopup.activeSelf)
            {
                inventoryPopup.SetActive(false);
                return;
            }
        }

        Cursor.lockState = CursorLockMode.None;
        UIManager.Instance.ShowPopup<InventoryPopup>();
    }

    private void OnEquipStarted(InputAction.CallbackContext context)
    {
        GameObject equipmentPopup = UIManager.Instance.GetPopupObject(nameof(EquipmentPopup));

        //장비창 토글
        if (equipmentPopup != null)
        {
            if (equipmentPopup.activeSelf)
            {
                equipmentPopup.SetActive(false);
                return;
            }
        }

        Cursor.lockState = CursorLockMode.None;
        UIManager.Instance.ShowPopup<EquipmentPopup>();
    }

    private void OnDefensePerformed(InputAction.CallbackContext context)
    {
        if (Cursor.lockState != CursorLockMode.None)
            stateMachine.IsDefensing = true;
    }

    protected virtual void OnDefenseCanceled(InputAction.CallbackContext context)
    {
    }

    private void OnSkill1Performed(InputAction.CallbackContext context)
    {
        stateMachine.SkillIndex = 1;
        stateMachine.ChangeState(stateMachine.SkillState);
    }

    private void OnSkill2Performed(InputAction.CallbackContext context)
    {
        stateMachine.SkillIndex = 2;
        stateMachine.ChangeState(stateMachine.SkillState);
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
        Vector3 destination = stateMachine.Player.transform.position + GetMovementDirection();
        stateMachine.Player.NavMeshAgent.destination = destination;
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

    protected Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.Player.transform.forward;
        Vector3 right = stateMachine.Player.transform.right;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
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
}