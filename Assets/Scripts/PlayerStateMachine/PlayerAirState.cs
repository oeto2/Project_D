using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    protected float gravity;
    protected Transform trans;
    protected Vector3 velocity;
    protected bool isJump;

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    public override void Update()
    {
        base.Update();
        //Look();
        //
        //velocity.y += gravity * Time.deltaTime;
        //// 레이캐스트로 점프 진행 방향에 벽 등이 없을 때만 이동되도록
        //RaycastHit ray;
        //if (!Physics.Raycast(trans.position, GetMovementDirection(), out ray, 1f))
        //{
        //    velocity += GetMovementDirection() * Time.deltaTime * 3.5f;
        //}
        //trans.position = velocity;
        //gravity -= 0.2f;

        
    }

    protected void Jump()
    {
        gravity = stateMachine.JumpForce;
        isJump = true;

        trans = stateMachine.Player.gameObject.transform;
        velocity = trans.position;
        stateMachine.Player.NavMeshAgent.ResetPath();
        stateMachine.Player.NavMeshAgent.enabled = false;
    }
}