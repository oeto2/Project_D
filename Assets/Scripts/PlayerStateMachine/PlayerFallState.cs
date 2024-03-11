using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("fall");
        //StartAnimation(stateMachine.Player.AnimationData.fallParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        //StopAnimation(stateMachine.Player.AnimationData.fallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (isGround())
        {
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}