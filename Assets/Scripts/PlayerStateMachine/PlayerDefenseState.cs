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
        stateMachine.Player.NavMeshAgent.speed = groundData.WalkSpeedModifier * groundData.BaseSpeed;
        stateMachine.Player.DefenseObj.SetActive(true);
        Debug.Log("방어상태");
        //StartAnimation();
    }

    public override void Exit()
    {
        stateMachine.Player.DefenseObj.SetActive(false);
        stateMachine.IsDefensing = false;
        Debug.Log("방어상태해제");
        base.Exit();

        //StopAnimation();
    }

    public override void Update()
    {
        base.Update();

        if (!stateMachine.IsDefensing)
            stateMachine.ChangeState(stateMachine.IdleState);
    }

    protected override void OnDefenseCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.Player.Input.playerActions.Defense.ReadValue<Vector2>() == Vector2.zero)
            stateMachine.IsDefensing = false;
    }
}
