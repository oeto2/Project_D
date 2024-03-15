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
        Jump();
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Update()
    {
        Look();
        if (isJump)
        {
            velocity.y += gravity * Time.deltaTime;
            // 레이캐스트로 점프 진행 방향에 벽 등이 없을 때만 이동되도록
            RaycastHit ray;
            if (!Physics.Raycast(trans.position, GetMovementDirection(), out ray, 1f))
            {
                velocity += GetMovementDirection() * Time.deltaTime * 3.5f;
            }
            trans.position = velocity;
            gravity -= 0.5f;

            if (velocity.y <= firstPos.y)
            {
                velocity.y = firstPos.y;
                gravity = 0f;
                trans.position = velocity;
                isJump = false;
                stateMachine.Player.NavMeshAgent.enabled = true;
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    private void Jump()
    {
        gravity = 3f;
        isJump = true;

        trans = stateMachine.Player.transform;
        firstPos = velocity = trans.position;
        stateMachine.Player.NavMeshAgent.ResetPath();
        stateMachine.Player.NavMeshAgent.enabled = false;
    }
}