using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefenseState : PlayerGroundState
{
    public PlayerDefenseState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.NavMeshAgent.speed = groundData.WalkSpeedModifier * groundData.BaseSpeed;
        //StartAnimation();
    }

    public override void Exit()
    {
        base.Exit();

        //StopAnimation();
    }
}
