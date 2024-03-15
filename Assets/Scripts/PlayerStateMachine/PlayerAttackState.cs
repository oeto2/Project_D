using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool alreadyAppliedForce;
    private bool alreadyAppliedDealing;

    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }


    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Player.Data.AttackData.GetAttackInfo(stateMachine.ComboIndex).ForceTransitionTime)
            {
                TryApplyForce();
                Debug.Log($"{stateMachine.ComboIndex}번 공격");
            }
        
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Player.Data.AttackData.GetAttackInfo(stateMachine.ComboIndex).Dealing_Start_TransitionTime)
            {
                //stateMachine.Player.Weapon.SetAttack(stateMachine.Player.Data.AttackData.GetAttackInfo(stateMachine.ComboIndex).Damage,
                //    stateMachine.Player.Data.AttackData.GetAttackInfo(stateMachine.ComboIndex).Force);
                //stateMachine.Player.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }
            
            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Player.Data.AttackData.GetAttackInfo(stateMachine.ComboIndex).Dealing_End_TransitionTime)
            {
                //stateMachine.Player.Weapon.gameObject.SetActive(false);
            }
        
        }
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;
        
        //stateMachine.Player.ForceReceiver.Reset();
        //
        //stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward *
        //    stateMachine.Player.Data.AttackData.GetAttackInfo(stateMachine.ComboIndex).Force);

    }

    public override void Enter()
    {
        alreadyAppliedForce = false;
        alreadyAppliedDealing = false;

        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        Debug.Log("어택");
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }
}