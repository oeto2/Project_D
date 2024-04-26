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

        NavMeshHit hit;
        if (NavMesh.SamplePosition(stateMachine.Player.playerTransform.position, out hit, 0.1f, NavMesh.AllAreas) && stateMachine.Player.PlayerController.gravity <= 0)
        {
            stateMachine.Player.playerTransform.position = hit.position;
            OnGround();
        }

        if (stateMachine.Player.PlayerController.gravity <= -40f)
        {
            stateMachine.Player.playerTransform.position = stateMachine.Player.beforeTrans;
            OnGround();
        }
    }

    private void OnGround()
    {
        stateMachine.Player.PlayerController.gravity = 0f;
        stateMachine.Player.PlayerController.isJump = false;
        stateMachine.Player.NavMeshAgent.enabled = true;
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}