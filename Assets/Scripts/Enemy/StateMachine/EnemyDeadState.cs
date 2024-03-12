using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {

    }
    public override void Enter()
    {
        Debug.Log("���� ��� ���� ����");

        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        SetTriggerAnimation(stateMachine.Enemy.AnimationData.DeadParameterHash);
    }
}
