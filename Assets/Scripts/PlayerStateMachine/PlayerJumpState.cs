using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    private float gravity;
    private Transform trans;
    private Vector3 velocity;
    private Vector3 firstPos;
    private bool isJump;

    public override void Enter()
    {
        stateMachine.JumpForce = stateMachine.Player.Data.AirData.JumpForce;
        //stateMachine.Player.ForceReceiver.Jump(stateMachine.JumpForce);
        Debug.Log("점프엔터");
        Jump();
        base.Enter();

        //StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        //StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (isJump)
        {
            velocity.y += gravity * Time.deltaTime;
            trans.position = velocity;
            Debug.Log(velocity);
            gravity -= 0.5f;

            if (velocity.y < firstPos.y)
            {
                velocity.y = firstPos.y;
                gravity = 0f;
                trans.position = velocity;
                isJump = false;
                stateMachine.ChangeState(stateMachine.IdleState);
                //stateMachine.Player.NavMeshAgent.enabled = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    private void Jump()
    {
        Debug.Log("점프입력");
        gravity = 5f;
        isJump = true;

        trans = stateMachine.Player.transform;
        firstPos = velocity = trans.position;
        
    }
}