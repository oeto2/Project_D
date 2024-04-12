using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDefenseState : PlayerGroundState
{
    public PlayerDefenseState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ฟฃลอ");
        stateMachine.Player.NavMeshAgent.speed = groundData.WalkSpeedModifier * groundData.BaseSpeed;
        stateMachine.Player.DefenseObj.SetActive(true);
        StartAnimation(stateMachine.Player.AnimationData.DefenseParameterHash);
    }

    public override void Exit()
    {
        stateMachine.Player.DefenseObj.SetActive(false);
        stateMachine.IsDefensing = false;
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.DefenseParameterHash);
    }

    public override void Update()
    {
        base.Update();

        stateMachine.Player.Stats.ChangeStaminaAction(-5 * Time.deltaTime);

        if (!stateMachine.IsDefensing || stateMachine.Player.Stats.stamina <= 0)
            stateMachine.ChangeState(stateMachine.IdleState);
    }

    protected override void OnDefenseCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.Player.Input.playerActions.Defense.ReadValue<Vector2>() == Vector2.zero)
            stateMachine.IsDefensing = false;
    }
}
