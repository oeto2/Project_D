using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.fallParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.fallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //NavMeshHit hit;
        //if (NavMesh.SamplePosition(trans.position, out hit, 0.1f, NavMesh.AllAreas) && gravity <= 0)
        //{
        //    gravity = 0f;
        //    trans.position = hit.position;
        //    isJump = false;
        //    stateMachine.Player.NavMeshAgent.enabled = true;
        //    stateMachine.ChangeState(stateMachine.IdleState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}