using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }


    public override void Enter()
    {
        base.Enter();
        //StartAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        //StopAnimation(stateMachine.Player.AnimationData.AirParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.Player.Rigidbody.velocity.y < 0)
        {
            stateMachine.ChangeState(stateMachine.FallState);
        }

        stateMachine.Player.Rigidbody.velocity += new Vector3(0, Physics.gravity.y, 0);
    }
}