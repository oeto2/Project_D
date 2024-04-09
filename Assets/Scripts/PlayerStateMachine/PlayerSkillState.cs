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

        stateMachine.Player.Animator.SetInteger("Skill", skillIndex);
        stateMachine.Player.Stats.ChangeManaAction(-skillInfoData.ManaCost);
        stateMachine.Player.PlayerSkills.coolDowns[skillIndex - 1] = stateMachine.Player.Data.SkillData.GetSkillInfo(skillIndex).CoolDown;

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
