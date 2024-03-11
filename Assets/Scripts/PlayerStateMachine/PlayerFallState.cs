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
        Debug.Log("폴스테이트");
        if (isGround())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        //stateMachine.Player.Rigidbody.velocity += new Vector3(0, Physics.gravity.y, 0);
    }
}