using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerAttackState
{
    // ���� ��ų
    public PlayerSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //StartAnimation();
    }

    public override void Exit() 
    { 
        base.Exit();
        //StopAnimation();
    }

    public override void Update()
    {
        base.Update();
    }
}
