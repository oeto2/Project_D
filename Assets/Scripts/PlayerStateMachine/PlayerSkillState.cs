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

        int skillIndex = stateMachine.SkillIndex;
        skillInfoData = stateMachine.Player.Data.SkillData.GetSkillInfo(skillIndex);

        if (stateMachine.Player.Stats.mana < skillInfoData.ManaCost)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        stateMachine.Player.Animator.SetInteger("Skill", skillIndex);
        stateMachine.Player.Stats.ChangeManaAction(-skillInfoData.ManaCost);

        StartAnimation(stateMachine.Player.AnimationData.BaseSkillParameterHash);
    }

    public override void Exit() 
    { 
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BaseSkillParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Skill");
        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
