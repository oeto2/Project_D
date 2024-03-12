using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        //StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!isGround() && stateMachine.Player.Rigidbody.velocity.y < 0)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }

    protected override void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // 입력이 안 들어왔다면 리턴
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.IdleState);

        base.OnMoveCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (isGround())
            stateMachine.ChangeState(stateMachine.JumpState);
    }

    

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    protected virtual void OnAttack()
    {

    }
}