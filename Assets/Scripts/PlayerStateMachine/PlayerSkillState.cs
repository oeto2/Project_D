using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerAttackState
{
    SkillInfoData skillInfoData;

    public PlayerSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //StartAnimation();

        int skillIndex = stateMachine.SkillIndex;
        skillInfoData = stateMachine.Player.Data.SkillData.GetSkillInfo(skillIndex);
        stateMachine.Player.Animator.SetInteger("Skill", skillIndex);
        stateMachine.Player.Stats.ChangeManaAction(-skillInfoData.ManaCost);
        
    }

    public override void Exit() 
    { 
        base.Exit();
        //StopAnimation();
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
    }


}
