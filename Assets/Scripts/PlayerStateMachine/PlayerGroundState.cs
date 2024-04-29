using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.GetCurrentState() == stateMachine.WalkState || stateMachine.GetCurrentState() == stateMachine.RunState)
            OnMove();

        if (stateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
        if (stateMachine.IsDefensing && stateMachine.GetCurrentState() != stateMachine.DefenseState) 
        {
            OnDefense(); 
            return;
        }
    }

    protected override void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // 입력이 안 들어왔다면 리턴
        if (stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }
    
        stateMachine.ChangeState(stateMachine.IdleState);
    
        base.OnMoveCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (true)
            stateMachine.ChangeState(stateMachine.JumpState);
    }

    protected virtual void OnMove()
    {
        if (!stateMachine.Player.IsRun)
            stateMachine.ChangeState(stateMachine.WalkState);
        else
            stateMachine.ChangeState(stateMachine.RunState);
    }   

    protected virtual void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.ComboAttackState);
    }

    protected virtual void OnDefense()
    {
        stateMachine.ChangeState(stateMachine.DefenseState);
    }

    protected override void OnSkill1Performed(InputAction.CallbackContext context)
    {
        if (CanSkillActive(1))
        {
            stateMachine.SkillIndex = 1;
            stateMachine.ChangeState(stateMachine.SkillState);
        }
        else
        {
            stateMachine.Player.Stats.noManaText(stateMachine.Player.Data.SkillData.GetSkillInfo(1).ManaCost);
        }
    }

    protected override void OnSkill2Performed(InputAction.CallbackContext context)
    {
        if (CanSkillActive(2))
        {
            stateMachine.SkillIndex = 2;
            stateMachine.ChangeState(stateMachine.SkillState);
        }
        else
        {
            stateMachine.Player.Stats.noManaText(stateMachine.Player.Data.SkillData.GetSkillInfo(2).ManaCost);
        }
    }

    protected override void OnSkill3Performed(InputAction.CallbackContext context)
    {
        if (CanSkillActive(3))
        {
            stateMachine.SkillIndex = 3;
            stateMachine.ChangeState(stateMachine.SkillState);
        }
        else if (stateMachine.Player.PlayerSkills.coolDowns[2] > 0)
        {
            stateMachine.Player.Stats.coolDownText(stateMachine.Player.PlayerSkills.coolDowns[2]);
        }
        else
        {
            stateMachine.Player.Stats.noManaText(stateMachine.Player.Data.SkillData.GetSkillInfo(3).ManaCost);
        }
    }

    private bool CanSkillActive(int index)
    {
        if (stateMachine.Player.Stats.mana < stateMachine.Player.Data.SkillData.GetSkillInfo(index).ManaCost || stateMachine.Player.PlayerSkills.coolDowns[index - 1] > 0)
        {
            return false;
        }
        return true;
    }
}