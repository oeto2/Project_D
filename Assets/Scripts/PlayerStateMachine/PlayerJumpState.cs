using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    

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

        velocity.y += gravity * Time.deltaTime;
        // 레이캐스트로 점프 진행 방향에 벽 등이 없을 때만 이동되도록
        RaycastHit ray;
        if (!Physics.Raycast(trans.position, GetMovementDirection(), out ray, 1f))
        {
            velocity += GetMovementDirection() * Time.deltaTime * 3.5f;
        }
        trans.position = velocity;
        gravity -= 0.2f;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(trans.position, out hit, 0.1f, NavMesh.AllAreas) && gravity <= 0)
        {
            gravity = 0f;
            trans.position = hit.position;
            isJump = false;
            stateMachine.Player.NavMeshAgent.enabled = true;
            stateMachine.ChangeState(stateMachine.IdleState);
        }

        //if (gravity <= 0)
        //{
        //    stateMachine.ChangeState(stateMachine.FallState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}