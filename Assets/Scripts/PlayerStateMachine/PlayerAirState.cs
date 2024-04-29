using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }


    public override void Enter()
    {
        base.Enter();
        if (!stateMachine.Player.PlayerController.isJump)
            Jump();
        StartAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    public override void Update()
    {
        Look();

        stateMachine.Player.PlayerController.velocity.y += stateMachine.Player.PlayerController.gravity * Time.deltaTime;
        // 레이캐스트로 점프 진행 방향에 벽 등이 없을 때만 이동되도록
        RaycastHit ray;
        if (!Physics.Raycast(stateMachine.Player.playerTransform.position, GetMovementDirection(), out ray, 1f))
        {
            stateMachine.Player.PlayerController.velocity += GetMovementDirection() * Time.deltaTime * 3.5f;
        }
        stateMachine.Player.playerTransform.position = stateMachine.Player.PlayerController.velocity;
        stateMachine.Player.PlayerController.gravity = Mathf.Max(-15f, stateMachine.Player.PlayerController.gravity - 10f * Time.deltaTime) ;

        
    }

    protected void Jump()
    {
        stateMachine.Player.beforeTrans = stateMachine.Player.playerTransform.position;
        stateMachine.Player.PlayerController.isJump = true;
        stateMachine.Player.PlayerController.gravity = stateMachine.JumpForce;
        stateMachine.Player.PlayerController.velocity = stateMachine.Player.playerTransform.position;
        if (stateMachine.Player.NavMeshAgent.enabled)
            stateMachine.Player.NavMeshAgent.ResetPath();
        stateMachine.Player.NavMeshAgent.enabled = false;
    }
}