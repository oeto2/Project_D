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

        //if (!stateMachine.Player.Controller.isGrounded
        //&& stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        //{
        //    stateMachine.ChangeState(stateMachine.FallState);
        //    return;
        //}
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

    private bool isGround()
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

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    protected virtual void OnAttack()
    {

    }
}